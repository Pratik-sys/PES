using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PESPresentationLayer
{
    /// <summary>
    /// Interaction logic for AdminHomePage.xaml
    /// </summary>
    public partial class AdminHomePage : Page
    {
        public AdminHomePage()
        {
            InitializeComponent();
        }

        private void btn_searchPolicy_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SearchPolicyPage());
        }

        private void btn_endorsementDetails_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EndorsementDetailsPage());
        }

        private void btn_customers_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomersPage());
        }

        private void btn_insuranceProducts_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new InsuranceProductsPage());
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
        }
    }
}
