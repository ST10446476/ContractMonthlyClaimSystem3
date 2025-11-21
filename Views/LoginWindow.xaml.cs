using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            // Set focus to username textbox
            Loaded += (s, e) => UsernameTextBox.Focus();

            // Allow Enter key to login
            UsernameTextBox.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                    PasswordBox.Focus();
            };

            PasswordBox.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                    LoginButton_Click(s, e);
            };
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

                // Find user
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

                // Store user info in application properties
                Application.Current.Properties["CurrentUserId"] = user.UserId;
                Application.Current.Properties["CurrentUserRole"] = user.Role;
                Application.Current.Properties["CurrentUsername"] = user.Username;

                // Open appropriate window based on role
                Window mainWindow = null;

                switch (user.Role)
                {
                    case "Lecturer":
                        if (user.LecturerId.HasValue)
                        {
                            mainWindow = new LecturerView();
                            ((LecturerView)mainWindow).DataContext =
                                new LecturerViewModel(user.LecturerId.Value);
                        }
                        break;

                    case "Coordinator":
                        mainWindow = new CoordinatorView();
                        ((CoordinatorView)mainWindow).DataContext =
                            new CoordinatorViewModel();
                        break;

                    case "HR":
                        mainWindow = new HRView();
                        ((HRView)mainWindow).DataContext =
                            new HRViewModel();
                        break;

                    default:
                        ErrorMessage.Text = "Unknown user role.";
                        return;
                }

                if (mainWindow != null)
                {
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = $"Login error: {ex.Message}";
            }
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

        private class HRViewModel
        {
            public HRViewModel()
            {
            }
        }

        private class LecturerViewModel
        {
            private object value;

            public LecturerViewModel(object value)
            {
                this.value = value;
            }
        }
    }
}
