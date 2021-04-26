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
    /// Interaction logic for SearchPolicyForCustomers.xaml
    /// </summary>
    public partial class SearchPolicyForCustomers : Page
    {
        public string custId { get; set; }
        public SearchPolicyForCustomers(string cId)
        {
            InitializeComponent();
            this.custId = cId;
        }

        private void btn_searchPolicy_Click(object sender, RoutedEventArgs e)
        {
            DataSet searchPolicy = null;
            try
            {
                string policyNo = txt_policyNumber.Text.Trim();
                searchPolicy = PolicyBL.GetAllPolicyByCustomerIdAndPolicyNumberBL(custId,policyNo);
                if (searchPolicy.Tables[0].Rows.Count > 0)
                    grid_policyDetails.ItemsSource = searchPolicy.Tables[0].DefaultView;
                else
                    throw new PolicyExceptions("No records found at present");
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet searchPolicy = null;
            try
            {
                searchPolicy = PolicyBL.SearchPolicyBL(custId);
                if (searchPolicy.Tables[0].Rows.Count > 0)
                    grid_policyDetails.ItemsSource = searchPolicy.Tables[0].DefaultView;
                else
                    throw new PolicyExceptions("No records found at present");
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            if(this.custId != null)
            {
                this.custId = null;
            }
            this.NavigationService.GoBack();
        }

        private void grid_policyDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                DataRowView dr = (DataRowView)dgr.Item;

                string policyNo = dr[1].ToString();
                this.NavigationService.Navigate(new ViewAndUpdatePolicyPage(policyNo));
            }
        }

        private void btn_reload_Click(object sender, RoutedEventArgs e)
        {
            if (txt_policyNumber.Text != null)
                txt_policyNumber.Text = "";
            this.NavigationService.Refresh();
        }
    }
}
