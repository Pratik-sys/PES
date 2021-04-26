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
    /// Interaction logic for EndorsementStatusPage.xaml
    /// </summary>
    public partial class EndorsementStatusPage : Page
    {
        public string CustomerId { get; set; }
        public EndorsementStatusPage(string customerId)
        {
            InitializeComponent();
            this.CustomerId = customerId;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (this.CustomerId != null)
            {
                this.CustomerId = null;
            }
            this.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataSet endorsementStatusList = null;
            try
            {
                endorsementStatusList = EndorsementBL.GetAllEndorsementStatus(CustomerId);
                if(endorsementStatusList.Tables[0].Rows.Count > 0)
                {
                    grid_endorsementStatus.ItemsSource = endorsementStatusList.Tables[0].DefaultView;
                }
            }catch(PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
