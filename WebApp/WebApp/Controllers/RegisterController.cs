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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Register")]
    public class RegisterController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public RegisterController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpPost,Route("registerUser")]
        public IHttpActionResult RegisterUser(RegisterUser userToRegister)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            string returnMessage = "";

            if (context.Users.Any(u => u.UserName == userToRegister.Username))
            {
                returnMessage = "This username is already taken.";
                return Ok(returnMessage);
            }

            var user = new ApplicationUser() { Id = userToRegister.Username, UserName = userToRegister.Username, Email = userToRegister.Username, PasswordHash = ApplicationUser.HashPassword(userToRegister.Password) };
            userManager.Create(user);
            userManager.AddToRole(user.Id, "AppUser");

            Address address = new Address();
            address.City = userToRegister.City;
            address.StreetName = userToRegister.StreetName;
            address.StreetNumber = userToRegister.StreetNumber;
            unitOfWork.AddressRepository.Add(address);
            unitOfWork.Complete();
            
            Passenger passenger = new Passenger();
            List<Address> addresses = unitOfWork.AddressRepository.GetAll().ToList();
            passenger.Address_id = addresses.Where(x=>x.City == userToRegister.City && x.StreetName == userToRegister.StreetName && x.StreetNumber == userToRegister.StreetNumber).FirstOrDefault().Id;
            passenger.AppUserId = user.Id;
            passenger.Birthday = userToRegister.Birthday;
            passenger.IsValidated = Enums.StateType.UNVERIFIED;
            passenger.LastName = userToRegister.Lastname;
            passenger.Name = userToRegister.Name;
            unitOfWork.PassengerRepository.Add(passenger);
            unitOfWork.Complete();
            returnMessage = "Successfully registered.";
            return Ok(returnMessage);
        }

    }
}
