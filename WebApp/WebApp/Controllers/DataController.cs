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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public IHttpActionResult GetPricelist()
        {
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

        [HttpPost,Route("buyTicket")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult BuyTicket(BuyedTicket ticket)
        {
            var userStore = new UserStore<ApplicationUser>(dbContext);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (User.Identity.IsAuthenticated)
            {
                Ticket t = new Ticket();

                if (ticket.TypeOfTicket == "TIMED")
                {
                    t.From = DateTime.Now;
                    t.To = DateTime.Now;
                    t.To = t.To.AddHours(1);
                    t.TypeOfTicket = Enums.TypeOfTicket.TIMED;
                    t.Price = ticket.TotalPrice;
                }
                else if (ticket.TypeOfTicket == "DIALY")
                {
                    t.From = DateTime.Now;
                    t.To = DateTime.Now;
                    t.To = t.To.AddDays(1);
                    t.TypeOfTicket = Enums.TypeOfTicket.DIALY;
                    t.Price = ticket.TotalPrice;
                }
                else if (ticket.TypeOfTicket == "MONTHLY")
                {
                    t.From = DateTime.Now;
                    t.To = DateTime.Now;
                    t.To = t.To.AddMonths(1);
                    t.TypeOfTicket = Enums.TypeOfTicket.MONTHLY;
                    t.Price = ticket.TotalPrice;
                }
                else if (ticket.TypeOfTicket == "YEARLY")
                {
                    t.From = DateTime.Now;
                    t.To = DateTime.Now;
                    t.To = t.To.AddYears(1);
                    t.TypeOfTicket = Enums.TypeOfTicket.YEARLY;
                    t.Price = ticket.TotalPrice;
                }

                string s = User.Identity.GetUserId();
                int passenger = unitOfWork.PassengerRepository.GetAll().Where(x => x.AppUserId == s).FirstOrDefault().Id;
                t.Passenger_id = passenger;

                unitOfWork.TicketRepository.Add(t);
                unitOfWork.Complete();
                return Ok();
            }
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

        [Route("getRoutes")]
        public IHttpActionResult GetRoutes()
        {
            List<Routes> routes = new List<Routes>();

            List<Route> routesDb = unitOfWork.RouteRepository.GetAll().ToList();

            foreach (Route r in routesDb)
            {
                List<RouteStation> routeStations = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Route_id == r.Id).ToList();
                List<Station> stations = new List<Station>();

                foreach (RouteStation rs in routeStations)
                {
                    stations.Add(unitOfWork.StationRepository.Get(rs.Station_id));
                }
                //dobaviti adrese od station mesto samo int za id
                routes.Add(new Routes { RouteNumber = r.RouteNumber, RouteType = r.RouteType.ToString(), Stations = stations });
            }

            return Ok(routes);
        }
    }
}
