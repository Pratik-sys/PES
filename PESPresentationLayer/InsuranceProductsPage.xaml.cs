using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for InsuranceProductsPage.xaml
    /// </summary>
    public partial class InsuranceProductsPage : Page
    {
        public InsuranceProductsPage()
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            InsuranceProducts insuranceProducts = null;
            try
            {
                insuranceProducts = new InsuranceProducts();
                insuranceProducts.ProductId = txt_proctId.Text;
                insuranceProducts = InsuranceProductsBL.SearchInsuranceProductBL(insuranceProducts.ProductId);
                if(insuranceProducts.ProductId != null)
                {
                    txt_productName.Text = insuranceProducts.ProductName;
                    cmb_productLine.Text = insuranceProducts.Line;
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            InsuranceProducts insuranceProducts = null;
            try
            {
                insuranceProducts = new InsuranceProducts();
                insuranceProducts.ProductName = txt_productName.Text;
                insuranceProducts.Line = cmb_productLine.Text;
                bool productAdded = InsuranceProductsBL.AddInsuranceProductBL(insuranceProducts);
                if (productAdded)
                {
                    MessageBoxResult result = MessageBox.Show("Product Added", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    if(result == MessageBoxResult.OK)
                    {
                        this.NavigationService.Refresh();
                    }
                }
            }
            catch(PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            InsuranceProducts insuranceProducts = null;
            try
            {
                insuranceProducts = new InsuranceProducts();
                insuranceProducts.ProductId = txt_proctId.Text;
                insuranceProducts.ProductName = txt_productName.Text;
                insuranceProducts.Line = cmb_productLine.Text;
                bool productUpdated = InsuranceProductsBL.UpdateInsuranceProductBL(insuranceProducts);
                if (productUpdated)
                {
                    MessageBoxResult result = MessageBox.Show("Product Updated", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        this.NavigationService.Refresh();
                    }
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet insuranceProductsList = null;
            try
            {
                insuranceProductsList = InsuranceProductsBL.GetAllInsuranceProductsBL();
                if (insuranceProductsList != null)
                    grid_productDetails.ItemsSource = insuranceProductsList.Tables[0].DefaultView;
                else
                    grid_productDetails.ItemsSource = null;
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            txt_proctId.Text = "";
            txt_productName.Text = "";
        }
    }
}
