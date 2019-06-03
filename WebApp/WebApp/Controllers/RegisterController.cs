using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Register")]
    public class RegisterController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public RegisterController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpPost,Route("registerUser")]
        public IHttpActionResult RegisterUser(object json)
        {
            ApplicationUser ap = new ApplicationUser();
            JObject jObject = JObject.Parse(json.ToString());
            JToken jUser = jObject;
            ap.UserName = (string)jUser["username"];
            ap.PasswordHash = (string)jUser["password"];

            Passenger newPassenger = new Passenger(json);
            Address newAddress = new Address(json);

            List<ApplicationUser> apu = unitOfWork..GetAll().ToList();


            return Ok(true);
        }

    }
}
