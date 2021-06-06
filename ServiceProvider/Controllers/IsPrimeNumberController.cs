using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthenticationInterface;
using ServiceProvider.Models;
namespace ServiceProvider.Controllers
{
    public class IsPrimeNumberController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: IsPrimeNumber
        public dynamic Get(int num,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            bool isPrime = false;
            if (result.Equals("Valid"))
            {
                for(int i = 2; i < num; i++)
                {
                    if (num % i == 0)
                    {
                        isPrime = false;
                        Response response = new Response(isPrime);
                        return response;
                    }
                }
                isPrime = true;
                Response response1 = new Response(isPrime);
                return response1;
            }
            else
            {
                Authentication authFail = new Authentication("Denied", "Authentication Error");
                return authFail;
            }
        }
    }
}
