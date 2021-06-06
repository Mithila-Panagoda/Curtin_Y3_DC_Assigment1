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
using AuthenticationInterface;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for LoginandReg.xaml
    /// </summary>
    public partial class LoginandReg : Page
    {
        AuthenticationServerInterface foob;
        User user;
        public LoginandReg()
        {
            /*
             * Connection to .net remoting server is established.
             */
            InitializeComponent();
            Authentication auth = Authentication.Instance;
            foob = auth.initServerConnection();
            InitializeComponent();
        }

            /*
             * Checks if the login is valid if valid the paged is changed
             * else an error message is shown
             */
        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            string name = txtuname.Text.Trim();
            string pwd = txtpwd.Password;
            if (name.Length == 0 || pwd.Length == 0)
            {
                MessageBox.Show("Name or Password Feilds cannot be empty!!.");
            }
            else
            {
                int result = foob.Login(name, pwd);
                if (result > 0)
                {
                    user = User.Instance;
                    MessageBox.Show("Logged in succesfully");
                    user.setToken(result);
                    user.setName(name);
                    ServicePage servicePage = new ServicePage();
                    this.NavigationService.Navigate(servicePage);
                }
                else
                {
                    lblErrormsg.Content = "Name or password incorrect Please try again.";
                    MessageBox.Show("Dont have an Account!! Enter name and passowrd and click on register.", Title = "Login Error");
                    txtuname.Clear();
                    txtpwd.Clear();
                }
            }
        }
        /*
         * When user Clicks on the register button they will be registered and if registration is successfull
         * they will be logged into the system.
         */
        private void btnreg_Click(object sender, RoutedEventArgs e)
        {
            string name = txtuname.Text;
            string pwd = txtpwd.Password;
            string result = foob.Register(name, pwd);
            if (name.Length == 0 || pwd.Length == 0)
            {
                MessageBox.Show("Name or Password Feilds cannot be empty!!.");
            }
            else
            {
                if (result.Equals("successfully registered"))
                {
                    MessageBox.Show("User Registered Successfully you will now be logged in", Title = "Registration Successfull");
                    int Result = foob.Login(name, pwd);
                    if (Result > 0)
                    {
                        user = User.Instance;
                        user.setName(name);
                        user.setToken(Result);
                        MessageBox.Show("Logged in succesfully");
                        ServicePage servicePage = new ServicePage();
                        this.NavigationService.Navigate(servicePage);
                    }
                    else
                    {
                        lblErrormsg.Content = "Name or password incorrect Please try again.";
                    }
                }
            }
        }
    }
}
