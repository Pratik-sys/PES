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
    /// Interaction logic for SearchPolicyPage.xaml
    /// </summary>
    public partial class SearchPolicyPage : Page
    {
        public SearchPolicyPage()
        {
            InitializeComponent();
            dp_customerDOB.SelectedDate = DateTime.Now;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btn_searchPolicy_Click(object sender, RoutedEventArgs e)
        {
            DataSet searchPolicy = null;
            try
            {
                if(cmb_searchPolicy.Text == "Policy Number")
                {
                    searchPolicy = PolicyBL.SearchPolicyBL(txt_policyNumber.Text.Trim());
                }
                else if(cmb_searchPolicy.Text == "Customer Id")
                {
                    searchPolicy = PolicyBL.SearchPolicyBL(txt_customerId.Text.Trim());
                }
                else 
                {
                    searchPolicy = PolicyBL.SearchPolicyBL(txt_customerName.Text.Trim(), dp_customerDOB.SelectedDate.Value);
                }
                
                if(searchPolicy.Tables[0].Rows.Count > 0)
                {
                    grid_policyDetails.ItemsSource = searchPolicy.Tables[0].DefaultView;
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet policyList = null;
            try
            {
                policyList = PolicyBL.GetAllPolicyBL();
                if (policyList.Tables[0].Rows.Count > 0)
                {
                    grid_policyDetails.ItemsSource = policyList.Tables[0].DefaultView;
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_reload_Click(object sender, RoutedEventArgs e)
        {
            txt_policyNumber.Text = "";
            txt_customerId.Text = "";
            txt_customerName.Text = "";
            dp_customerDOB.SelectedDate = DateTime.Now;
            this.NavigationService.Refresh();
        }
    }
}
