using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using AuthenticationInterface;
namespace ServiceProvider.Models
{
    public class Authentication
    {
        public Authentication(string status, string reason)
        {
            Status = status;
            Reason = reason;
        }
        public string Status { get; set; }
        public string Reason { get; set; }
        /*
            Using Singelton design pattern to ennsure that only one object of authentication
            type object exists.
        */
        private Authentication() { }
        private static Authentication instance = null;
        public static Authentication Instance
        {
            get
            {
                if(instance == null){
                    instance = new Authentication();
                }
                return instance;
            }
        }
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