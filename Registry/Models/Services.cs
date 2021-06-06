using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class Services
    {
        public Services() { }

        public Services(string aPIEndPoint)
        {
            APIEndPoint = aPIEndPoint;
        }

        public Services(string name, string description, string aPIEndPoint, int noOperands, string operandType)
        {
            Name = name;
            Description = description;
            APIEndPoint = aPIEndPoint;
            NoOperands = noOperands;
            OperandType = operandType;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string APIEndPoint { get; set; }
        public int NoOperands { get; set; }
        public string OperandType { get; set; }

        public string getapiendpoint()
        {
            return APIEndPoint;
        }
    }
}