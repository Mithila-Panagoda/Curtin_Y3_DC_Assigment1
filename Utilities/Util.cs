using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    //This clas is used to store the file locations of the textfiles used in the project. For Easier Managment of the project
    public static class Util
    {
        // default location : C:\Users\MSI\Desktop\SLIIT\CURTIN\Sem1\DC\assement\DC Assigment 01\Authenticator\bin\Debug
        public static readonly string USER_FILE_PATH = @"UserDetails.txt";

        //default location : C:\Users\MSI\Desktop\SLIIT\CURTIN\Sem1\DC\assement\DC Assigment 01\Authenticator\bin\Debug
        public static readonly string TOKEN_FILE_PATH = @"TokenPool.txt";

        //default location : C:\Program Files (x86)\IIS Express
        public static readonly string ServiceDB_FILE_PATH = @"Servicedb.txt";
    }
}
