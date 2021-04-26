using PESBusinessLayer;
using PESEntity;
using PESExceptions;
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
    /// Interaction logic for EndorsementApprovalPage.xaml
    /// </summary>
    public partial class EndorsementApprovalPage : Page
    {
        public string PolicyNo { get; set; }
        public string EndorsementId { get; set; }
        public EndorsementApprovalPage(string policyNo, string endorsementId)
        {
            InitializeComponent();
            this.PolicyNo = policyNo;
            this.EndorsementId = endorsementId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Policy policy = null;
            try
            {
                policy = new Policy();
                policy = PolicyBL.ViewPolicyBL(PolicyNo);
                if (policy.C.CustomerId != null)
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
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Approve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string flag = "Approved";
                bool endorsementAdded = EndorsementBL.UpdateEndorsemetBL(new Endorsement { EndorsementId = EndorsementId, PolicyId = PolicyNo }, flag);
                if (endorsementAdded)
                    MessageBox.Show("Endorsement Approved", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

            }catch(PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Reject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string flag = "Rejected";
                bool endorsementAdded = EndorsementBL.UpdateEndorsemetBL(new Endorsement { EndorsementId = EndorsementId, PolicyId = PolicyNo }, flag);
                if (endorsementAdded)
                    MessageBox.Show("Endorsement Rejected", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (this.EndorsementId != null)
            {
                this.EndorsementId = null;
            }
            if(this.PolicyNo != null)
            {
                this.PolicyNo = null;
            }
            this.NavigationService.GoBack();
        }

        private void btn_GoToHome_Click(object sender, RoutedEventArgs e)
        {
            if (this.EndorsementId != null)
            {
                this.EndorsementId = null;
            }
            if (this.PolicyNo != null)
            {
                this.PolicyNo = null;
            }
            this.NavigationService.Navigate(new AdminHomePage());
        }
    }
}
