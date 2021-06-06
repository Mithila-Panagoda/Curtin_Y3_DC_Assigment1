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
    public class GeneratePrimeNumbersinRangeController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: GeneratePrimeNumbersinRange/lowerbound/upperbound/token
        public dynamic Get(int num1,int num2,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result=foob.validate(token);
            if (result.Equals("Valid"))
            {
                List<int> primeNumbers = new List<int>();
                for (int i = num1; i <= num2; i++)
                {
                    if (i == 1)
                    {
                        i++;
                    }
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
                Authentication response = new Authentication("Denied", "Authentication Error");
                return response;
            }
        }

    }
}
