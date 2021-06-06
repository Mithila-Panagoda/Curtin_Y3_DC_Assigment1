using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using AuthenticationInterface;
namespace Authenticator
{
    
    class Program
    {
        static void Main(string[] args)
        {
           //Program.cs Hosts the server.
            AuthenticationServer auth = new AuthenticationServer();
            //starting server
            Console.WriteLine("Authentication Server.");
            var tcp = new NetTcpBinding();
            var host = new ServiceHost(typeof(AuthenticationServer));
            host.AddServiceEndpoint(typeof(AuthenticationServerInterface), tcp, "net.tcp://0.0.0.0:8100/AuthenticationService");
            host.Open();
            Thread timer = new Thread(auth.clearTokens);
            Console.WriteLine("Server is Running");
            //User inputs taken to set the timmer
            Console.WriteLine("Enter time(mins) To clear Tokens: ");
            int time = Convert.ToInt32(Console.ReadLine());
            auth.setTime(time);//value of timer is set.
            Console.WriteLine("Timer Started.\nTokens will be cleared every "+time+" minutes!");
            timer.IsBackground = true; // The Thread is set as an background thread
            timer.Start(); // Thread Execution is started.
            
            Console.ReadLine();
            host.Close();
        }
    }
}
