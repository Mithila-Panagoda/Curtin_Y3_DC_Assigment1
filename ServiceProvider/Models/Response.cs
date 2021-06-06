using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceProvider
{
    public class Response
    {
        public Response()
        {
        }

        public Response(int answer)
        {
            Answer = answer;
        }

        public Response(List<int> primeNumberAnswer)
        {
            PrimeNumbers = primeNumberAnswer;
        }

        public Response(bool isPrimeNumber)
        {
            this.isPrimeNumber = isPrimeNumber;
        }

        

        
        public int Answer { get; set; }
        public List<int> PrimeNumbers { get; set; }
        public bool isPrimeNumber { get; set; }
    }
}