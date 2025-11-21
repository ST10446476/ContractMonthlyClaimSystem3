using System.Windows;
using ContractMonthlyClaimSystem.Views;

namespace ContractMonthlyClaimSystem
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Start with LoginWindow
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}



