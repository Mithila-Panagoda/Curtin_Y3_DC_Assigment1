using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Registry.Models;
using System.IO;
using AuthenticationInterface;
using Utilities;
namespace Registry.Controllers
{
    public class PublishController : ApiController
    {
        Authentication auth = Authentication.Instance;
        //api/{Publish}/?data=
        /*
         * This function is used to publish services data is sent in as a string where to will Added to text file
         * the return type of the funciton is set to dynamic since it can return one of two objects. How ever at the calling point
         * a genral class is available to deserialize the objects into it
         */
        public dynamic Get(string data,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            if (result.Equals("Valid"))
            {
                string filepath = Util.ServiceDB_FILE_PATH;
                //string filepath = "Servicedb.txt";
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filepath).ToList();
                Services services = JsonConvert.DeserializeObject<Services>(data);
                lines.Add(data);
                File.WriteAllLines(filepath, lines);
                return services;
            }
            else
            {
                Authentication authfail = new Authentication("Denied", "Authentication Error");
                return authfail;
            }
            // dynamic obj = JsonConvert.DeserializeObject<dynamic>(data);
            
        }

    }
}
