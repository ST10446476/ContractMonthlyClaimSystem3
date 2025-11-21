using ContractMonthlyClaimSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClaimSystem.Views
{
    /// <summary>
    /// Interaction logic for CoordinatorView.xaml
    /// </summary>
    public partial class CoordinatorView : Window
    {
        public CoordinatorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Handle status filter radio button changes
            if (DataContext is ContractMonthlyClaimSystem.ViewModels.CoordinatorViewModel viewModel)
            {
                var radioButton = sender as RadioButton;
                if (radioButton != null && radioButton.Tag != null)
                {
                    viewModel.FilterStatus = radioButton.Tag.ToString();
                }
            }
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