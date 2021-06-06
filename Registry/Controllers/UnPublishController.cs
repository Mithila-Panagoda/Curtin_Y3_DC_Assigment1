using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Registry.Models;
using System.IO;
using Newtonsoft.Json;
using AuthenticationInterface;
using Utilities;
namespace Registry.Controllers
{
    public class UnPublishController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: api/UnPublish
        /*
         * This service will search for a matching endpoint in the text file and delete the line that contains the record
         * which will in return ubpublish the service
         */
        public dynamic Get(string endpoint,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            if (result.Equals("Valid"))
            {
                string filepath = Util.ServiceDB_FILE_PATH;
                //string filepath = "Servicedb.txt";
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filepath).ToList();
                List<Services> data = new List<Services>();
                Services services;
                Services removedService = new Services();
                foreach (string line in lines)
                {
                    services = new Services();
                    services = JsonConvert.DeserializeObject<Services>(line);
                    data.Add(services);
                }

                lines.Clear();
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].getapiendpoint().Equals(endpoint))
                    {
                        removedService = data[i];
                        data.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < data.Count; i++)
                {
                    string jsonstring = JsonConvert.SerializeObject(data[i]);
                    lines.Add(jsonstring);
                }
                File.WriteAllLines(filepath, lines);
                return removedService;
            }
            else
            {
                Authentication authfail = new Authentication("Denied", "Authentication Error");
                return authfail;
            }

        }
    }
}
