using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using CD9TSchool.Models;

namespace CD9TSchool.App_Start
{
    public class MyPrincipal : IPrincipal
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }
        public Utils.Enum.Role Role { get; set; }

        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}