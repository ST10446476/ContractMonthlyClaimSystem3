using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ContractMonthlyClaimSystem.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ClaimDbContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new ClaimDbContext();

            // Set focus to username textbox on load
            Loaded += (s, e) => UsernameTextBox.Focus();

            // Enter key handling
            UsernameTextBox.KeyDown += TextBox_KeyDown;
            PasswordBox.KeyDown += TextBox_KeyDown;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginButton_Click(sender, e);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = string.Empty;

            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage.Text = "Please enter both username and password.";
                return;
            }

            try
            {
                // Hash the password
                string passwordHash = HashPassword(password);

                // Look for user in DB
                var user = _context.Users
                    .FirstOrDefault(u => u.Username == username &&
                                         u.PasswordHash == passwordHash &&
                                         u.IsActive);

                if (user == null)
                {
                    ErrorMessage.Text = "Invalid username or password.";
                    PasswordBox.Clear();
                    return;
                }

                // Update last login date
                user.LastLoginDate = DateTime.Now;
                _context.SaveChanges();

                // Store user session info
                Application.Current.Properties["CurrentUserId"] = user.UserId;
                Application.Current.Properties["CurrentUsername"] = user.Username;
                Application.Current.Properties["CurrentUserRole"] = user.Role;

                // Open the correct window based on role
                Window mainWindow = user.Role switch
                {
                    "Lecturer" when user.LecturerId.HasValue => OpenLecturerWindow(user.LecturerId.Value),
                    "Coordinator" => OpenCoordinatorWindow(),
                    "HR" => OpenHRWindow(),
                    _ => null
                };

                if (mainWindow != null)
                {
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    ErrorMessage.Text = "User role not recognized or missing data.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = $"Login error: {ex.Message}";
            }
        }

        private LecturerView OpenLecturerWindow(int lecturerId)
        {
            var window = new LecturerView
            {
                DataContext = new LecturerViewModel(lecturerId)
            };
            return window;
        }

        private CoordinatorView OpenCoordinatorWindow()
        {
            var window = new CoordinatorView
            {
                DataContext = new CoordinatorViewModel()
            };
            return window;
        }

        private HRView OpenHRWindow()
        {
            var window = new HRView
            {
                DataContext = new HRViewModel()
            };
            return window;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _context?.Dispose();
            base.OnClosed(e);
        }
    }
}
