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
using PESBusinessLayer;
using PESEntity;
using PESExceptions;
using System.Data;

namespace PESPresentationLayer
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            InitializeComponent();
            dp_customerDOB.SelectedDate = DateTime.Now;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet customerList = null;
            try
            {
                customerList = CustomerBL.GetAllCustomersBL();
                if(customerList.Tables[0].Rows.Count > 0)
                {
                    grid_customerDetails.ItemsSource = customerList.Tables[0].DefaultView;
                }
            }
            catch(PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btn_searchCustomer_Click(object sender, RoutedEventArgs e)
        {
            DataSet customerList = null;
            try
            {
                customerList = new DataSet();
                if(cmb_searchCustomer.Text == "Customer Id")
                {
                    customerList = CustomerBL.GetCustomerByIdBL(txt_customerId.Text.Trim());
                }
                else
                {
                    customerList = CustomerBL.SearchCustomerByNameAndDOBBL(txt_customerName.Text.Trim(), dp_customerDOB.SelectedDate.Value);
                }
                if (customerList.Tables[0].Rows.Count > 0)
                {
                    grid_customerDetails.ItemsSource = customerList.Tables[0].DefaultView;
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_reload_Click(object sender, RoutedEventArgs e)
        {
            txt_customerId.Text = "";
            txt_customerName.Text = "";
            dp_customerDOB.SelectedDate = DateTime.Now;
            this.NavigationService.Refresh();
        }
    }
}
