using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI
{
    /*
     * This Class which is made using the Singelton desing pattern to limit the object instances to one will be used to
     * to obtain the token number which will be set after a sucessfull login.
     */
    class User
    {
        private User() { }
        private static User instance = null;
        public static User Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }

        private string name;
        private int token;

        public void setName(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return this.name;
        }
        public void setToken(int token)
        {
            this.token = token;
        }
        public int getToken()
        {
            return this.token;
        }
    }
}
