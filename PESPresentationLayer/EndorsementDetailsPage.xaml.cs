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
    /// Interaction logic for EndorsementDetailsPage.xaml
    /// </summary>
    public partial class EndorsementDetailsPage : Page
    {
        public EndorsementDetailsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet endorsementList = null;
            try
            {
                endorsementList = EndorsementBL.GetAllEndorsement();
                if(endorsementList.Tables[0].Rows.Count > 0)
                {
                    grid_endorsementDetails.ItemsSource = endorsementList.Tables[0].DefaultView;
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

        private void grid_endorsementDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                DataRowView dr = (DataRowView)dgr.Item;

                string endorsementId = dr[1].ToString();
                string policyNo = dr[2].ToString();
                this.NavigationService.Navigate(new EndorsementApprovalPage(policyNo, endorsementId));
            }
        }
    }
}
