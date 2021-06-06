using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthenticationInterface;
using Registry.Models;
using System.IO;
using Newtonsoft.Json;
using Utilities;
namespace Registry.Controllers
{
    public class SearchController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: api/Search
        /*
         * This Function will search the text file for a valid string and if a match is made all matching instance data is sent back
         * Firstly all the data is read froma file and stored in a list<string> object. then the .Contains() method is used to see if there 
         * is a matching substring if so the item will serialized and aded to a nother list<string> which will be returned
         * back to the calling point.
         */
        public List<string> Get(string data,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            if (result.Equals("Valid"))
            {
                string filepath = Util.ServiceDB_FILE_PATH;
                //string filepath = "Servicedb.txt";
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filepath).ToList();
                List<string> returnData = new List<string>();
                for(int i=0; i<lines.Count;i++)
                {
                    Services services = JsonConvert.DeserializeObject<Services>(lines[i]);
                    if (services.Description.Contains(data))
                    {
                        string jsonstr = JsonConvert.SerializeObject(services);
                        returnData.Add(jsonstr);
                    }
                }
                return returnData;
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
