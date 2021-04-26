using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using PESBusinessLayer;
using PESEntity;
using PESExceptions;

namespace PESPresentationLayer
{
    /// <summary>
    /// Interaction logic for CustomerHomePage.xaml
    /// </summary>
    public partial class CustomerHomePage : Page
    {
        public string custId { get; set; }
        public CustomerHomePage(string cId)
        {
            InitializeComponent();
            this.custId = cId;
        }

        private void btn_applyPolicy_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AppliyPolicyPage(custId));
        }

        private void btn_searchPolicy_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SearchPolicyForCustomers(custId));
        }

        private void btn_endorsementStatus_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EndorsementStatusPage(custId));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet customer = new DataSet();
                customer = CustomerBL.GetCustomerByIdBL(custId);
                if(customer.Tables[0].Rows.Count > 0)
                {
                    var customerDOB = Convert.ToDateTime(customer.Tables[0].Rows[0][6]);
                    txt_customerId.Text = customer.Tables[0].Rows[0][1].ToString();
                    txt_customerName.Text = customer.Tables[0].Rows[0][2].ToString();
                    txt_customerAge.Text = Convert.ToString(new DateTime((DateTime.Now - customerDOB).Ticks).Year);
                    txt_customerAddress.Text = customer.Tables[0].Rows[0][3].ToString();
                    txt_customerTelephone.Text = customer.Tables[0].Rows[0][4].ToString();
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            if(this.custId != null)
            {
                this.custId = null;
            }
            this.NavigationService.Navigate(new LoginPage());
        }
    }
}
