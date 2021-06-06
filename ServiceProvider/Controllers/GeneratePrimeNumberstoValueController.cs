using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceProvider.Models;
using AuthenticationInterface;

namespace ServiceProvider.Controllers
{
    public class GeneratePrimeNumberstoValueController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: GeneratePrimeNumberstoValue/num/token
        public dynamic Get(int num,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            if (result.Equals("Valid"))
            {            
                List<int> primeNumbers = new List<int>();
                for (int i = 2; i < num; i++)
                {
                    bool isprime = true;
                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            isprime = false;
                            break;
                        }
                    }
                    if (isprime)
                    {
                        primeNumbers.Add(i);
                    }
                }
                Response response = new Response(primeNumbers);
                return response;
            }
            else
            {
                Authentication response = new Authentication("Denied", "Authentication error");
                return response;
            }
        }

       
    }
}
