using System.Data.Entity;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using ContractMonthlyClaimSystem.Views;

namespace ContractMonthlyClaimSystem
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize EF or other services here if needed

            // Show the CoordinatorView window
            CoordinatorView coordinatorView = new CoordinatorView();
            coordinatorView.Show();
        }
    }
}



