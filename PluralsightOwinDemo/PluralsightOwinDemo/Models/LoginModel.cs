using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;

namespace PluralsightOwinDemo.Models
{
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public List<AuthenticationDescription> AuthProviders { get; set; }
    }
}