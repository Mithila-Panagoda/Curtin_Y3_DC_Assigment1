using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using AuthenticationInterface;
using Registry.Models;
using Newtonsoft.Json;
using Utilities;
namespace Registry.Controllers
{
    public class AllServicesController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: api/AllServices
        /*
         * this fucntion will be used to return all the published services back to the calling point when the api call is made.
         * data is saved in a text file (ServiceDB) in json string format. data is returned as a List<string> which can later be deserilised
         * into objects.
         */
        public List<string> Get(int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            if (result.Equals("Valid"))
            {
                List<string> AllServices = new List<string>();
                string filepath = Util.ServiceDB_FILE_PATH;
                //  string filepath = "Servicedb.txt";
                AllServices = File.ReadAllLines(filepath).ToList();
                return AllServices;
            }
            else
            {
                Authentication auth = new Authentication("Denied", "Authentication Error");
                string jsonstr = JsonConvert.SerializeObject(auth);
                List<string> authFail = new List<string>();
                authFail.Add(jsonstr);
                return authFail;
            }
        }
    }
}
