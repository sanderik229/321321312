using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10Prakticheskaya
{
    internal class user
    {
        public string Login;
        public string Password;
        public string Role;
        public string ID;
        public user(string log, string pass, string role, string id)
        {
            Login = log;
            Password = pass;
            Role = role;
            ID = id;
        }
    }
}