using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.WebApi.Service
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {

        }
    }
    
}