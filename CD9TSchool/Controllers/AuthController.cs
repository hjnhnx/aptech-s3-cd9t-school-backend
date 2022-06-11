using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models;
using CD9TSchool.Models.Dto;
using CD9TSchool.Validators;
using Google.Apis.Auth;


namespace CD9TSchool.Controllers
{
    public class AuthController : ApiController
    {
        private SchoolManager db;
        public AuthController()
        {
            db = new SchoolManager();
        }
        public async Task<GoogleJsonWebSignature.Payload> ValidateToken(string token)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { "861796971442-ev7jgcr16iksig7344c49nbf5qtq0fvo.apps.googleusercontent.com" }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return (payload);
        }
        
        [Route("api/login")]
        public async Task<IHttpActionResult> Login(LoginRequest request)
        {
            var payload = await ValidateToken(request.Token);
            if (payload == null)
                return BadRequest("Invalid External Authentication."); 
            
            var user = db.Users.Where(m => m.Email == payload.Email).FirstOrDefault();
            if (user == null)
            {
                return BadRequest();
            }

            user.Token = request.Token;
            user.Avatar = payload.Picture ?? null;
            user.TokenExpireDate = DateTime.Now.AddHours(8.0);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(new UserDto (){
                id = user.Id,
                email = user.Email,
                role = user.Role,
                avatar = user.Avatar
            });
        }
        
        [Route("api/logout")]
        public IHttpActionResult Logout(LoginRequest request)
        {
            if (request.Token == null)
                return BadRequest("Invalid External Authentication.");
            var user = db.Users.Where(m => m.Token == request.Token ).FirstOrDefault();
            if (user != null)
            {
                user.Token = null;
                user.TokenExpireDate = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
            }
            
            return Ok();
        }

        [Route("api/profile")]
        [MyAuth]
        public IHttpActionResult GetInformation()
        {
            var dataUser = HttpContext.Current.User as MyPrincipal;
            var info = new UserDto()
            {
                id = dataUser.Id,
                email = dataUser.Email,
                role = dataUser.Role,
                avatar = dataUser.Avatar
            };
            return Ok(info);    
        }
    }
}