using Newtonsoft.Json;
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
    [RoutePrefix("api/Data")]
    public class DataController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public DataController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [Route("getPricelist")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetPricelist()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            // User.IsInRole()
            //}

            Pricelist pricelist = unitOfWork.PricelistRepository.GetAll().Where(x => DateTime.Compare(x.From, DateTime.Now) < 0 && DateTime.Compare(x.To, DateTime.Now) > 0).FirstOrDefault();
            List<Item> item = unitOfWork.ItemRepository.GetAll().ToList();
            List<PricelistItem> pricelistItems = unitOfWork.PricelistItemRepository.GetAll().Where(x => x.Pricelist_id == pricelist.Id).ToList();

            string json = "[";
            int c = 0;
            foreach (PricelistItem p in pricelistItems)
            {
                json += "{\"type\": \"" + item.Where(i => i.Id == p.Item_id).FirstOrDefault().TypeOfTicket.ToString() + "\",";
                json += "\"price\": \"" + pricelistItems.Where(i => i.Item_id == p.Item_id).FirstOrDefault().Price + "\"},";
                c++;
            }
            json = json.Remove(json.Length - 1, 1);
            json += "]";


            return Ok(json);
        }

        [Route("getCoefficient")]
        public IHttpActionResult GetCoefficient()
        {
            Coefficients coefficients = unitOfWork.CoefficientRepository.GetAll().FirstOrDefault();
            return Ok(coefficients);
        }

        [Route("buyTicket")]
        public IHttpActionResult BuyTicket(string typeOfUser, string typeOfTicket, int totalPrice)
        {

            return Ok();
        }

        [Route("updateProfile")]
        public IHttpActionResult UpdateProfile(RegisterUser userToRegister)
        {


            return Ok();
        }

        [Route("getProfileData")]
        public IHttpActionResult GetProfileData()
        {
            //Kako da proverim kredencijale korisnika?
            //Da li kopam nekako token ili?

            string s = User.Identity.Name;



            return Ok();
        }
    }
}
