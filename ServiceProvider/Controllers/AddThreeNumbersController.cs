using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ServiceModel;
using ServiceProvider.Models;
using AuthenticationInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ServiceProvider.Controllers
{
    public class AddThreeNumbersController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: api/AddThreeNumbers/num1/num2/num3/token
        public dynamic Get(int num1, int num2, int num3,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            if (result.Equals("Valid"))
            {
                int answer = num1 + num2 + num3;
                Response response = new Response(answer);
                return response;
            }
            else
            {
                Authentication response = new Authentication("Denied", "Invalid token");
                return response;
            }
        }
    }
   
}
