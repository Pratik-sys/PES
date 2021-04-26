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

namespace PESPresentationLayer
{
    /// <summary>
    /// Interaction logic for NewUserPage.xaml
    /// </summary>
    public partial class NewUserPage : Page
    {
        public NewUserPage()
        {
            InitializeComponent();
            dp_customerDOB.SelectedDate = DateTime.Now;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = new Customer();
                customer.CustomerName = txt_name.Text;
                customer.Address = txt_Address.Text;
                customer.DOB = dp_customerDOB.SelectedDate.Value;
                customer.Gender = Convert.ToChar(cmb_customerGender.Text.Substring(0,1));
                customer.Telephone = txt_telephone.Text;
                customer.Smoker = (rb_Smoker.IsChecked == true) ? true : false;
                customer.Hobbies = txt_hobbies.Text;

                var result = CustomerBL.AddCustomerBL(customer);
                if (result.Item1)
                {
                    this.NavigationService.Navigate(new PolicyPage(result.Item2));
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            txt_name.Text = "";
            txt_Address.Text = "";
            txt_telephone.Text = "";
            cmb_customerGender.SelectedIndex = 2;
            dp_customerDOB.SelectedDate = DateTime.Now;
            txt_hobbies.Text = "";
        }
    }
}
