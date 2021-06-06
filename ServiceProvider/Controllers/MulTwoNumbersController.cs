﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceProvider.Models;
using AuthenticationInterface;
namespace ServiceProvider.Controllers
{
    public class MulTwoNumbersController : ApiController
    {
        Authentication auth = Authentication.Instance;
        // GET: api/MulTwoNumbers/num1/num2/token
        public dynamic Get(int num1 , int num2,int token)
        {
            AuthenticationServerInterface foob = auth.initServerConnection();
            string result = foob.validate(token);
            if (result.Equals("Valid"))
            {
                int answer = num1 * num2;
                Response response = new Response(answer);
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
