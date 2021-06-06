using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using AuthenticationInterface;
using System.ServiceModel;
namespace ServiceHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = User.Instance;
            int token = -1;
            /*
             * The User is kept inside a continues loop until they perform a successfull refgistration or login
             * if the user chooses to register if the registration is successfull they will be automatically logged into the system.
             */
            while (token <= 0)
            {
                token = InitialMenu();
                if (token > 0)
                {
                    user.setToken(token);
                    break;
                }
                else if (token == -1)
                {
                    Console.WriteLine("User Regitraton has failed please try again");
                    continue;
                }
                else if (token == 0) {
                    Console.WriteLine("Username or password is incorrect please try again!!");
                    continue;
                }
                else if (token == -99)
                {
                    continue;
                }
            }
            int option = 1;
            /*
             * Here the user will once again be kept inside another continues loop until they select the logout option.
             * until the authentication server automatically deletes the the token which will stop the user from requesting 
             * any futher services. The user will have to logout and restart the application in order to publish and unpublish services.
             */
            while(option != 3)
            {
                option = serviceMenue();
                if (option == 1)
                {
                    PublishService();
                }
                else if (option == 2)
                {
                    unblishService();
                }
                else if (option == 3)
                {
                    break;
                }
            }
            Console.ReadLine();
        }

        /*
         * Error Codes:-
         * -1 = User registration has failed
         *  0 = User Login has failed.
         *  IF Registration is successfull User will be logged in as well.
         */

        public static int serviceMenue()
        {
            Console.WriteLine("select one of the Follwing Services.");
            Console.WriteLine("1.Publish a service.\n2.Unpublish a service\n3.logout");
            Console.WriteLine("Select an option(1/2/3):");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }
        //this Menu will intially ask the client to select whether to login or register according to the selection the neccessaru text
        //for inputs will be promted
        public static int InitialMenu()
        {
            Console.WriteLine("Would you like to : \n1.Register\n2.Login");
            Console.WriteLine("Enter Your Selectio(1/2) :");
            int selection = Convert.ToInt32(Console.ReadLine());
            if (selection == 1)
            {
                Console.WriteLine("Please Enter Your Name : ");
                string Name = Console.ReadLine();
                Console.WriteLine("Please Enter your Password: ");
                string pwd = Console.ReadLine();
                AuthenticationServerInterface foob = initServer();
                string result = foob.Register(Name, pwd);
                if (result.Equals("successfully registered"))
                {
                    Console.WriteLine("User Registered");
                    int token = foob.Login(Name, pwd);
                    return token;
                }
                else
                {
                    return -1;
                }               
            }
            else if (selection == 2)
            {
                Console.WriteLine("Please Enter Your Name : ");
                string Name = Console.ReadLine();
                Console.WriteLine("Please Enter your Password: ");
                string pwd = Console.ReadLine();
                AuthenticationServerInterface foob = initServer();
                int token = foob.Login(Name, pwd);
                if (token > 0)
                {
                    Console.WriteLine("User " + Name + "Has Logged in");
                    return token;
                }
                else
                {
                   
                    return 0;
                }

            }
            else
            {
                Console.WriteLine("Invalid Selection Please try again");
                return -99;
            }
        }
        public static void unblishService()
        {
            User user = User.Instance;
            Console.WriteLine("Enter Service endpoint to unpublish the service: ");
            string endpoint = Console.ReadLine();

            //API call is made to unpublish the service which has the matching endpoint.
            string url = @"https://localhost:44388/api/UnPublish/?endpoint=" + endpoint+"&token="+user.getToken();
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);

            Console.WriteLine("Response : " + response.Content.ToString());
            
        }
        public static void PublishService()
        {
            /*
             * Key board inputs are taken from the user to intialise a Services object which will then get serialized and sent via
             * the url to be saved to in the text file as a published service.
             */
            User user = User.Instance;
            Console.WriteLine("Enter Name Service :-");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Service Description : ");
            string desc = Console.ReadLine();
            Console.WriteLine("Enter API end point : ");
            string apiEndPoint = Console.ReadLine();
            Console.WriteLine("Enter Number of Operands : ");
            int noOperands = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Operand type");
            string operandType = Console.ReadLine();
            Services services = new Services(name, desc, apiEndPoint, noOperands, operandType);

            string json = JsonConvert.SerializeObject(services);
            //API call is made to publish the service
            string url = @"https://localhost:44388/api/Publish/?data=" + json+"&token="+user.getToken();

            var clinet = new RestClient(url);
            var request = new RestRequest();
            var response = clinet.Get(request);
            Console.WriteLine("Data Recived: "+response.Content.ToString());
            //return services;
        }
        /*
         * Connection to the .net remoting server is established in the function.
         */
        public static AuthenticationServerInterface initServer()
        {
            AuthenticationServerInterface foob;
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost:8100/AuthenticationService";
            var chanFactory = new ChannelFactory<AuthenticationServerInterface>(tcp, URL);
            foob = chanFactory.CreateChannel();
            return foob;
        }
    }
}
