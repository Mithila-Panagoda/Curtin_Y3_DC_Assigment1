using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace AuthenticationInterface
{
    //Functions that are avilable via the .net remoting server are mentioned here
    [ServiceContract]
    public interface AuthenticationServerInterface
    {
        [OperationContract]
        string Register(string name, string password);

        [OperationContract]
        int Login(string name, string password);

        [OperationContract]
        string validate(int token);

        void clearTokens();
    }
}
