using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using AuthenticationInterface;
using Utilities;
namespace Authenticator
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    class AuthenticationServer : AuthenticationServerInterface
    {
        private Random random;
        List<string> lines;
        private int time=0;
        //Timer is set which will be used to pause the thread execution
        public void setTime(int time)
        {
            this.time = time;
        }
        /*
         * Once The Thread starts and this fuction is invoked it will periodically empty the token text file
         * which will remove all the tokens that are currently saved.
         */
        public void clearTokens()
        {
            while (true)
            {
                Thread.Sleep(time * 60000); // 1min == 60000 milliseconds
                lines = new List<string>();
                lines.Clear();
                File.WriteAllLines(Util.TOKEN_FILE_PATH,lines);
                //File.WriteAllLines("TokenPool.txt", lines);
                Console.WriteLine("Tokens Cleared.");
            }
        }
        /*
         * The Login funcitons acces the userDetails text files and searches for a matching user name and passowrd
         * Once a match has been found a token will be generated with the use of the Random class
         * This token will the be saved to the token text file and the token will be returned to the calling point
         * The token will be used to validate service calls.
         */
        public int Login(string name, string password)
        {
            int flag_found = 0;
            int token = 0;
            string[] data;
            lines = new List<string>();

            lines = File.ReadAllLines(Util.USER_FILE_PATH).ToList();
            //lines = File.ReadAllLines("UserDetails.txt").ToList();
            foreach (string line in lines)
            {
                data = line.Split(',');
                if(data[0] == name && data[1] == password){
                    flag_found = 1;
                    break;
                }
            }
            // flag_found == 1 denotes that a match has been found in the text file.
            if (flag_found==1)
            {
                random = new Random();
                token = random.Next(100000, 999999);//min and max values are given
                lines = new List<string>();
                lines = File.ReadAllLines(Util.TOKEN_FILE_PATH).ToList();
                //lines = File.ReadAllLines("TokenPool.txt").ToList();
                if (lines.Count() == 0)
                {
                    lines.Clear();
                }
                lines.Add(token.ToString());
                File.WriteAllLines(Util.TOKEN_FILE_PATH, lines);
                //File.WriteAllLines("TokenPool.txt", lines);
                Console.WriteLine("User " + name + " Has Logged in. Genarated Token : " + token);
                return token;

            }
            else
            {
                return -1;
            }
        }

        /*
         * This Function will be used to register a new user. name and password is accepted as parameters
         * this two values will then bee written into the UserDetails text file and saved.
         */
        public string Register(string name, string password)
        {
            lines = new List<string>();
            lines = File.ReadAllLines(Util.USER_FILE_PATH).ToList();
            //lines = File.ReadAllLines("UserDetails.txt").ToList();
            lines.Add("" + name + "," + password);
            try
            {
                File.WriteAllLines(Util.USER_FILE_PATH, lines);
                // File.WriteAllLines("UserDetails.txt", lines);
                Console.WriteLine("Registered User : " + name);
                return "successfully registered";
            }
            catch(IOException e)
            {
                Console.WriteLine("Failed to save User : "+e.Message);
                return "Registration Failed";
            }
            
            
        }
        /*
         * This Function is used to validate user tokens
         * This function ensures that only logged in users gain access to services
         * However when the thread clears the tokens user will have to relogin and get a new token.
         */
        public string validate(int token)
        {
            int flag_token_found = 0;
            lines = new List<string>();
            lines = File.ReadAllLines(Util.TOKEN_FILE_PATH).ToList();
            //lines = File.ReadAllLines("TokenPool.txt").ToList();
            foreach (string line in lines)
            {
                if (line.Equals(token.ToString()))
                {
                    flag_token_found = 1;
                    break;
                }
            }
            if (flag_token_found == 1)
            {
                Console.WriteLine("Token :" + token.ToString() + " Has been validated");
                return "Valid";
            }
            else
            {
                Console.WriteLine("Token :" + token.ToString() + " Is invalid");
                return "Invalid";
            }
        }
    }
}
