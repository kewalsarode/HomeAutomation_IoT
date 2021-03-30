using AutoHome.API.Models;
using AutoHome.BL.DbServiceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoHome.API.Filters;

namespace AutoHome.API.Controllers
{
    public class AuthController : ApiController
    {
        public HttpResponseMessage Post(LoginModel loginModel)
        {
            try
            {
                UserManager objManager = new UserManager();
                var user = objManager.ValidateUser(loginModel.Username, loginModel.Password);

                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Incorrect credentials.");

                var authModule = new AuthenticationModule();
                string token = authModule.GenerateTokenForUser(user);

                return Request.CreateResponse(HttpStatusCode.OK, new { Token = token, Expires = DateTime.UtcNow.AddDays(30) });
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
