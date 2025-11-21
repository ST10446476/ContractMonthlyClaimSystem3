using ContractMonthlyClaimSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContractMonthlyClaimSystem.ViewModels
{
    public partial class HRView : Window
    {
        public HRView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            // Return to login
            var login = new LoginWindow();
            login.Show();
        }
    }
}
