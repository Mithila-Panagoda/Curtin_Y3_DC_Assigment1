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
using RestSharp;
using Newtonsoft.Json;
namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            User user = User.Instance;
            lbluserData.Content = "Welcome :" + user.getName() + " Token:" + user.getToken();
        }
        //User Will be sent back to the login page.
        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {
            LoginandReg login = new LoginandReg();
            MessageBox.Show("User logged out SuccessFully");
            User user = User.Instance;
            user.setToken(0);
            this.NavigationService.Navigate(login);
        }
        /*
         * When the button is pressed and api call is made to the Registry where all the registered services
         * will be retrived and displayed in the data grid.
         */
        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            Services services;
            User user = User.Instance;
            //Api Call is made.
            string url = @"https://localhost:44388/api/AllServices/?token=" + user.getToken(); ;
            var clinet = new RestClient(url);
            var request = new RestRequest();
            var response = clinet.Get(request);
            List<string> data = new List<string>();
            data = JsonConvert.DeserializeObject<List<string>>(response.Content.ToString());
            List<Services> gridData = new List<Services>();
            foreach (string line in data)
            {
                services = JsonConvert.DeserializeObject<Services>(line);
                if (services.Status.Equals("Denied"))
                {
                    logout();
                    break;
                }
                gridData.Add(services);
            }
           // Services test = serviceInfo.SelectedItem as Services;
            serviceInfo.ItemsSource = gridData;
        }
        private void serviceInfo_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Services selected = serviceInfo.SelectedItem as Services;
            if (selected.Name == null)
            {
                MessageBox.Show("Selected Feild is Empty",Title="Empty Feild Selected");
            }
            else
            {
                lblName.Content = selected.Name;
                lblDesc.Content = selected.Description;
                lblEndPoint.Content = selected.APIEndPoint;
                lblnoOperands.Content = selected.NoOperands;
            }
            //Implementation of dynamic buttons and test button.
            if (selected.NoOperands==2)
            {
                txtbox1.Visibility = Visibility.Visible;
                txtbox2.Visibility = Visibility.Visible;
                txtbox3.Visibility = Visibility.Hidden;
                btntest.Visibility = Visibility.Visible;
            }
            else if (selected.NoOperands==3)
            {
                txtbox1.Visibility = Visibility.Visible;
                txtbox2.Visibility = Visibility.Visible;
                txtbox3.Visibility = Visibility.Visible;
                btntest.Visibility = Visibility.Visible;
            }
            else if (selected.NoOperands == 1)
            {
                txtbox1.Visibility = Visibility.Visible;
                txtbox2.Visibility = Visibility.Hidden;
                txtbox3.Visibility = Visibility.Hidden;
                btntest.Visibility = Visibility.Visible;
            }
        }
        /*
         * When the user double click data on a item grid data will be loaded onto lables whose visibility is set to hidden.
         * Since the Name proerty determines the function name a series of if and else if statments are used to execute the correct api call
         * the url will be set with the appropiate amount of parameters and the token that will be used for validation and sent
         * The out put will be displayed accordingly
         * 
         * In an event where the token becomes invalid the user will be logged out and a message box will be shown to the user
         * Then the user will be moved to the login/register page once again
         */
        private void btntest_Click(object sender, RoutedEventArgs e)
        {
            User user = User.Instance;
            primeNumList.Children.Clear();
            if (lblName.Content.Equals("AddTwoNumbers"))
            {
                if(txtbox1.Text.Length==0 || txtbox2.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                }
                else
                {
                    string endpoint = lblEndPoint.Content.ToString();
                    string url = endpoint + @"/" + txtbox1.Text + "/" + txtbox2.Text + "/" + user.getToken();
                    var clinet = new RestClient(url);
                    var request = new RestRequest();
                    var response = clinet.Get(request);
                    APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content.ToString());
                    if (aPIResponse.Status.Equals("Denied"))
                    {
                        MessageBox.Show("Invalid Token you will be logged out now!!");
                        LoginandReg loginandReg = new LoginandReg();
                        this.NavigationService.Navigate(loginandReg);
                    }
                    else
                    {
                        MessageBox.Show(""+txtbox1.Text + "+"+txtbox2.Text + "="+aPIResponse.answer, Title = "Answer");
                    }
                }
            }
            else if(lblName.Content.ToString().Equals("AddThreeNumbers")){
                if (txtbox1.Text.Length == 0 || txtbox2.Text.Length == 0 || txtbox3.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                }
                else
                {
                    string endpoint = lblEndPoint.Content.ToString();
                    string url = endpoint + @"/" + txtbox1.Text + "/" + txtbox2.Text + "/"+txtbox3.Text + "/" + user.getToken();
                    var clinet = new RestClient(url);
                    var request = new RestRequest();
                    var response = clinet.Get(request);
                    APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content.ToString());
                    if (aPIResponse.Status.Equals("Denied"))
                    {
                        MessageBox.Show("Token Expiered you will be logged out now!!");
                        LoginandReg loginandReg = new LoginandReg();
                        this.NavigationService.Navigate(loginandReg);
                    }
                    else
                    {
                        MessageBox.Show("" + txtbox1.Text + "+" + txtbox2.Text +"+"+txtbox3.Text+ "=" + aPIResponse.answer, Title = "Answer");
                    }
                }
            }
            else if (lblName.Content.ToString().Equals("MulTwoNumbers"))
            {
                if (txtbox1.Text.Length == 0 || txtbox2.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                }
                else
                {
                    string endpoint = lblEndPoint.Content.ToString();
                    string url = endpoint + @"/" + txtbox1.Text + "/" + txtbox2.Text +  "/" + user.getToken();
                    var clinet = new RestClient(url);
                    var request = new RestRequest();
                    var response = clinet.Get(request);
                    APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content.ToString());
                    if (aPIResponse.Status.Equals("Denied"))
                    {
                        logout();
                    }
                    else
                    {
                        MessageBox.Show("" + txtbox1.Text + "X" + txtbox2.Text +  "=" + aPIResponse.answer, Title = "Answer");
                    }
                }
            }
            else if (lblName.Content.ToString().Equals("MulThreeNumbers"))
            {
                if (txtbox1.Text.Length == 0 || txtbox2.Text.Length == 0 || txtbox3.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                }
                else
                {
                    string endpoint = lblEndPoint.Content.ToString();
                    string url = endpoint + @"/" + txtbox1.Text + "/" + txtbox2.Text + "/" + txtbox3.Text + "/" + user.getToken();
                    var clinet = new RestClient(url);
                    var request = new RestRequest();
                    var response = clinet.Get(request);
                    APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content.ToString());
                    if (aPIResponse.Status.Equals("Denied"))
                    {
                        logout(); //logout procedure was encapsulated inside the login function.
                    }
                    else
                    {
                        MessageBox.Show("" + txtbox1.Text + "X" + txtbox2.Text+"X"+txtbox3.Text+ "=" + aPIResponse.answer, Title = "Answer");
                    }
                }
            }
            else if (lblName.Content.ToString().Equals("GeneratePrimeNumberstoValue"))
            {
                if (txtbox1.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                }
                else
                {
                    string endpoint = lblEndPoint.Content.ToString();
                    string url = endpoint + @"/" + txtbox1.Text + "/"  + user.getToken();
                    var clinet = new RestClient(url);
                    var request = new RestRequest();
                    var response = clinet.Get(request);
                    APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content.ToString());
                    if (aPIResponse.Status.Equals("Denied"))
                    {
                        logout();
                    }
                    else
                    {
                        primeNumList.Children.Clear();
                        Label title = new Label();
                        title.Content = "Prime Numbers until " + txtbox1.Text + ": ";
                        primeNumList.Children.Add(title);
                        foreach(int nums in aPIResponse.PrimeNumbers)
                        {
                            Label data = new Label();
                            data.Content = nums;
                            primeNumList.Children.Add(data);
                        }
                        //MessageBox.Show("prime numbers until " + txtbox1.Text + ": " + aPIResponse.PrimeNumbers.ToArray().ToString()) ;
                    }
                }
            }
            else if (lblName.Content.ToString().Equals("GeneratePrimeNumbersinRange"))
            {
                if (txtbox1.Text.Length == 0 || txtbox2.Text.Length == 0) 
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                }
                else
                {
                    string endpoint = lblEndPoint.Content.ToString();
                    string url = endpoint + @"/" + txtbox1.Text + "/"+txtbox2.Text+"/" + user.getToken();
                    var clinet = new RestClient(url);
                    var request = new RestRequest();
                    var response = clinet.Get(request);
                    APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content.ToString());
                    if (aPIResponse.Status.Equals("Denied"))
                    {
                        logout();
                    }
                    else
                    {
                        Label title = new Label();
                        title.Content = "Prime Numbers until " + txtbox2.Text + ": ";
                        primeNumList.Children.Add(title);
                        foreach (int nums in aPIResponse.PrimeNumbers)
                        {
                            Label data = new Label();
                            data.Content = nums;
                            primeNumList.Children.Add(data);
                        }
                    }
                }
            }
            else if (lblName.Content.ToString().Equals("IsPrimeNumber"))
            {
                if (txtbox1.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                }
                else
                {
                    string endpoint = lblEndPoint.Content.ToString();
                    string url = endpoint + @"/" + txtbox1.Text + "/" + user.getToken();
                    var clinet = new RestClient(url);
                    var request = new RestRequest();
                    var response = clinet.Get(request);
                    APIResponse aPIResponse = JsonConvert.DeserializeObject<APIResponse>(response.Content.ToString());
                    if (aPIResponse.Status.Equals("Denied"))
                    {
                        logout();
                    }
                    else
                    {
                        MessageBox.Show("prime number results : "+aPIResponse.isPrimeNumber.ToString());
                        if (aPIResponse.isPrimeNumber)
                        {
                            MessageBox.Show("" + txtbox1.Text + " Is a PrimeNumber");
                        }
                        else
                        {
                            MessageBox.Show("" + txtbox1.Text + " Is not a PrimeNumber");
                        }
                    }
                }
            }
        }
        /*
         * this Funciton will dispaly token expiered message and move the user back to the login page.
         */
        public void logout()
        {
            MessageBox.Show("Token Expiered you will be logged out now!!");
            LoginandReg loginandReg = new LoginandReg();
            this.NavigationService.Navigate(loginandReg);
        }
        /*
         * This function takes the input from a text box and sends to api where a string search will be carried out 
         * on the Description feild
         * Matching data will be returned.
         */
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtsearch.Text.Length == 0)
            {
                MessageBox.Show("Search Feild Cannot be left Empty!!");
            }
            else
            {
                Services services;
                User user = User.Instance;
                string url = @"https://localhost:44388/api/Search/?data="+txtsearch.Text+"&token=" + user.getToken(); ;
                var clinet = new RestClient(url);
                var request = new RestRequest();
                var response = clinet.Get(request);
                List<string> data = new List<string>();
                data = JsonConvert.DeserializeObject<List<string>>(response.Content.ToString());
                List<Services> gridData = new List<Services>();
                foreach (string line in data)
                {
                    services = JsonConvert.DeserializeObject<Services>(line);
                    if (services.Status.Equals("Denied"))
                    {
                        logout();
                        break;
                    }
                    gridData.Add(services);
                }
                serviceInfo.ItemsSource = gridData;
            }
        }
    }

}
