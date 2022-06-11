using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Mvc.Filters;
using CD9TSchool.Data;
using CD9TSchool.Models;
using Enum = CD9TSchool.Utils.Enum;
using IAuthenticationFilter = System.Web.Http.Filters.IAuthenticationFilter;


namespace CD9TSchool.App_Start
{
    public class MyAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        private readonly Enum.Role[] roles = new Enum.Role[] { };
        private readonly SchoolManager db;
        
        public MyAuthAttribute(Enum.Role[] roles)
        {
            this.roles = roles;
            db = new SchoolManager();
        }

        public MyAuthAttribute(Enum.Role role)
        {
            this.roles = new Enum.Role[]{role};
            db = new SchoolManager();
        }
        
        public MyAuthAttribute()
        {
            db = new SchoolManager();
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var token = context.Request.Headers.GetValues("Authorization").First();
            if (token == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials");
                return;
            }

            token = token.Substring(7);
            
            var user = db.Users.FirstOrDefault(m => m.Token.Equals(token));
            db.Entry<User>(user).Reload();
            if (user == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials");
                return;
            }
            
            var result = DateTime.Compare(DateTime.Now, user.TokenExpireDate ?? DateTime.Now.AddHours(-1));
            if (result > 0)
            {
                user.Token = null;
                user.TokenExpireDate = null;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                db.Entry<User>(user).Reload();
                context.ErrorResult = new AuthenticationFailureResult("Token Expired");
                return;
            }

            if (this.roles.Count() > 0 && !this.roles.Contains(user.Role))
            {
                context.ErrorResult = new AuthorizationFailureResult("Invalid role!");
                return;
            }
            
            var principal = new MyPrincipal();
            principal.Id = user.Id;
            principal.Email = user.Email;
            principal.Token = user.Token;
            principal.Role = user.Role;
            principal.Avatar = user.Avatar;
            context.ActionContext.Request.GetRequestContext().Principal = principal;
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context,
            CancellationToken cancellationToken)
        {
            Debug.WriteLine("done");
        }
    }
}