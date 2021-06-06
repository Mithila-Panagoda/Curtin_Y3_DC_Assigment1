using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationInterface;
using System.ServiceModel;
namespace ClientGUI
{
    /*
     * This Clas is used to establish a connection to the .net remoting server
     */
    class Authentication
    {
        private Authentication() { }

        public Authentication(string status, string reason)
        {
            Status = status;
            Reason = reason;
        }

        /*
         * Singelton Design pattern is applied in order to limit object instances to 01
         */
        private static Authentication instance = null;
        public static Authentication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Authentication();
                }
                return instance;
            }
        }
        public string Status { get; set; }
        public string Reason { get; set; }

        public AuthenticationServerInterface initServerConnection()
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
