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
    /// Interaction logic for AppliyPolicyPage.xaml
    /// </summary>
    public partial class AppliyPolicyPage : Page
    {
        public string custId { get; set; }
        public AppliyPolicyPage(string cId)
        {
            InitializeComponent();
            this.custId = cId;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if(this.custId != null)
            {
                this.custId = null;
            }
            this.NavigationService.GoBack();
        }

        private void btn_GeneratePolicy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Policy policy = new Policy();
                policy.C = new Customer { CustomerId = custId };
                policy.P = new InsuranceProducts { ProductId = cmb_productName.SelectedValue.ToString() };
                policy.Nominee = txt_nominee.Text;
                policy.Relation = txt_nomineeRelation.Text;
                policy.PremiumPayment = cmb_policyPremiumFrequency.Text;
                policy.FilePath = txt_uploadDocument.Text;

                bool policyAdded = PolicyBL.AddPolicyBL(policy);
                if (policyAdded)
                {
                    MessageBoxResult result = MessageBox.Show("Policy Generated Successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    if(result == MessageBoxResult.OK)
                    {
                        if (this.custId != null)
                            this.custId = null;
                        this.NavigationService.GoBack();
                    }
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message,"Error" ,MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (this.custId != null)
                    this.custId = null;
            }
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

        public void BindComboBox(ComboBox comboBoxName)
        {
            DataSet ds = null;
            try
            {
                ds = InsuranceProductsBL.GetAllInsuranceProductsBL();
                if (ds != null)
                {
                    comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
                    comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["productName"].ToString();
                    comboBoxName.SelectedValuePath = ds.Tables[0].Columns["productId"].ToString();
                }
                else
                {
                    throw new PolicyExceptions("Insurance Products Not Listed");
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    this.NavigationService.Navigate(new LoginPage());
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BindComboBox(cmb_productName);
            cmb_productName.SelectedIndex = 0;
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            txt_nominee.Text = "";
            txt_nomineeRelation.Text = "";
            txt_uploadDocument.Text = "";
        }
    }
}
