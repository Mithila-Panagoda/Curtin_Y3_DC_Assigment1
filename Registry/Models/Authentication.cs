using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthenticationInterface;
using System.ServiceModel;

namespace Registry.Models
{
    public class Authentication
    {
        public Authentication(string status, string reason)
        {
            Status = status;
            Reason = reason;
        }
        private Authentication() { }
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