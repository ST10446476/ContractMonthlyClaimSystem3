using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ContractMonthlyClaimSystem.Views
{
    public partial class CoordinatorView : Window
    {
        private List<Claim> allClaims;

        public CoordinatorView()
        {
            InitializeComponent(); // WPF InitializeComponent
            LoadClaims();
            DisplayClaims(allClaims);
        }

        private void LoadClaims()
        {
            // Sample data
            allClaims = new List<Claim>
            {
                new Claim { ClaimId = 1, Lecturer = "John Doe", Status = "Pending" },
                new Claim { ClaimId = 2, Lecturer = "Jane Smith", Status = "Approved" },
                new Claim { ClaimId = 3, Lecturer = "Alice Brown", Status = "Rejected" },
                new Claim { ClaimId = 4, Lecturer = "Bob White", Status = "Pending" }
            };
        }

        private void DisplayClaims(IEnumerable<Claim> claims)
        {
            ClaimsDataGrid.ItemsSource = claims;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            if (radio != null)
            {
                string status = radio.Tag?.ToString();
                if (status == "All")
                    DisplayClaims(allClaims);
                else
                    DisplayClaims(allClaims.Where(c => c.Status == status));
            }
        }
    }

    public class Claim
    {
        public int ClaimId { get; set; }
        public string Lecturer { get; set; }
        public string Status { get; set; }
    }
}
