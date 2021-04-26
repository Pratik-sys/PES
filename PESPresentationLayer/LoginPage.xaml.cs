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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            this.Loaded += delegate
            {
                Window window = Window.GetWindow(this);
                window.SetBinding(Window.MinHeightProperty, new Binding() { Source = this.MinHeight });
                window.SetBinding(Window.MinWidthProperty, new Binding() { Source = this.MinWidth });
            };
        }

        private void lbl_NewUser_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new NewUserPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Login login = new Login();
                login.UserId = txt_userId.Text;
                login.Password = txt_password.Password.ToString();
                login.LoginType = (rb_Admin.IsChecked == true) ? "A" : "C";
                if(login.LoginType == "A")
                {
                    bool checkAdmin = LoginBL.CheckAdminBL();
                    if (!checkAdmin)
                    {
                        bool adminCreated = LoginBL.CreateAdminBL();
                        if (!adminCreated)
                            MessageBox.Show("Database Not Found");
                    }
                    bool adminLoginVerified = LoginBL.checkLoginCredentialsBL(login);
                    if (adminLoginVerified)
                        this.NavigationService.Navigate(new AdminHomePage());
                    else
                        throw new PolicyExceptions("Login Credentials Not Present");
                }
                else
                {
                    bool loginVerified = LoginBL.checkLoginCredentialsBL(login);
                    if (loginVerified)
                        this.NavigationService.Navigate(new CustomerHomePage(login.Password));
                    else
                        throw new PolicyExceptions("Login Credentials Not Present");
                }
            }
            catch (PolicyExceptions ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
