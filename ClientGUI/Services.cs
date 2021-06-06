using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI
{
    class Services
    {
        public Services(string name, string description, string aPIEndPoint, int noOperands, string operandType)
        {
            Name = name;
            Description = description;
            APIEndPoint = aPIEndPoint;
            NoOperands = noOperands;
            OperandType = operandType;
        }

        public Services(string status, string reason)
        {
            Status = status;
            Reason = reason;
        }
        public Services()
        {
            Name = "";
            Description = "";
            APIEndPoint = "";
            NoOperands = 0;
            OperandType = "";
            Status = "";
            Reason = "";
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string APIEndPoint { get; set; }
        public int NoOperands { get; set; }
        public string OperandType { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        
    }
}
