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
    /// Interaction logic for ViewAndUpdatePolicyPage.xaml
    /// </summary>
    public partial class ViewAndUpdatePolicyPage : Page
    {
        public string PolicyNo { get; set; }
        public string CustomerId { get; set; }
        public ViewAndUpdatePolicyPage(string policyNo)
        {
            InitializeComponent();
            this.PolicyNo = policyNo;
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();
            openFileDlg.Filter = "PDF files (*.pdf)| *.pdf| JPEG files (*.jpg)|*.jpg| All files (*.*)|*.*";
            if (result == true)
            {
                txt_uploadDocument.Text = openFileDlg.FileName;
            }
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.CustomerId != null)
            {
                this.CustomerId = null;
            }
            if (this.PolicyNo != null)
            {
                this.PolicyNo = null;
            }
            this.NavigationService.GoBack();
        }

        private void btn_updatePolicy_Click(object sender, RoutedEventArgs e)
        {
            Policy policy = null;
            try
            {
                policy = new Policy();
                policy.C = new Customer { CustomerId = CustomerId };
                policy.PolicyId = PolicyNo;
                policy.Nominee = txt_nominee.Text;
                policy.Relation = txt_nomineeRelation.Text;
                policy.PremiumPayment = cmb_policyPremiumFrequency.Text;
                policy.FilePath = txt_uploadDocument.Text;
                policy.C.Address = txt_Address.Text;
                policy.C.Telephone = txt_telephone.Text;
                policy.C.Smoker = (rb_Smoker.IsChecked == true) ? true : false;

                bool policyUpdated = PolicyBL.UpdatePolicy(policy);
                if (policyUpdated)
                {
                    MessageBoxResult result = MessageBox.Show("Policy Updated Successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    if(result == MessageBoxResult.OK)
                    {
                        this.NavigationService.Navigate(new CustomerHomePage(CustomerId));
                    }
                }

            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerHomePage(CustomerId));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Policy policy = null;
            try
            {
                policy = new Policy();
                policy = PolicyBL.ViewPolicyBL(PolicyNo);
                if(policy.C.CustomerId != null)
                {
                    txt_name.Text = policy.C.CustomerName;
                    txt_Address.Text = policy.C.Address;
                    dp_customerDOB.SelectedDate = policy.C.DOB;
                    txt_telephone.Text = policy.C.Telephone;
                    txt_productName.Text = policy.P.ProductName;
                    cmb_productLine.Text = policy.P.Line;
                    txt_nominee.Text = policy.Nominee;
                    txt_nomineeRelation.Text = policy.Relation;
                    cmb_policyPremiumFrequency.Text = policy.PremiumPayment;
                    txt_uploadDocument.Text = policy.FilePath;
                    if (policy.C.Smoker == true)
                        rb_Smoker.IsChecked = true;
                    else
                        rb_NonSmoker.IsChecked = true;
                    this.CustomerId = policy.C.CustomerId;
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
