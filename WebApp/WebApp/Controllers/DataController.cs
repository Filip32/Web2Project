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
using System.Globalization;
using WebApp.Helper;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Data")]
    public class DataController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public DataController(IUnitOfWork uw)
        {
           // HelperReader.Reader(uw);
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

        [Route("getTypeOfLoginUser")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetTypeOfLoginUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                string s = User.Identity.GetUserId();
                Passenger passenger = unitOfWork.PassengerRepository.GetAll().Where(x => x.AppUserId == s).FirstOrDefault();
                string message = "{\"TypeOfUser\" : \"" + passenger.PassengerType.ToString()+ "\",";
                message += "\"IsValid\" : \"" + passenger.IsValidated + "\"}";
                return Ok(message);
            }
            return Ok();
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

                    if (t.To.Day != t.From.Day)
                    {
                        string ss = t.From.Date.ToString();
                        string[] niz = ss.Split(' ');
                        niz[1] = "11:59:59 PM";
                        string time = niz[0] + " " + niz[1];
                        t.To = Convert.ToDateTime(time);
                    }
                    t.TypeOfTicket = Enums.TypeOfTicket.TIMED;
                    t.Price = ticket.TotalPrice;
                }
                else if (ticket.TypeOfTicket == "DIALY")
                {
                    t.From = DateTime.Now;
                    t.To = DateTime.Now;
                    t.To = t.To.AddDays(1);
                    if (t.To.Day != t.From.Day)
                    {
                        string ss = t.From.Date.ToString();
                        string[] niz = ss.Split(' ');
                        niz[1] = "11:59:59 PM";
                        string time = niz[0] + " " + niz[1];
                        t.To = Convert.ToDateTime(time);
                    }
                    t.TypeOfTicket = Enums.TypeOfTicket.DIALY;
                    t.Price = ticket.TotalPrice;
                }
                else if (ticket.TypeOfTicket == "MONTHLY")
                {
                    t.From = DateTime.Now;
                    t.To = DateTime.Now;
                    t.To = t.To.AddMonths(1);
                   
                    if (t.To.Month != t.From.Month)
                    {
                        string ss = t.From.ToString();
                        string[] niz = ss.Split(' ');
                        string lastDay = GetLastDay(t.From.Month).ToString();
                        niz[1] = "11:59:59 PM";
                        string[] nizniz = niz[0].Split('-');
                        nizniz[0] = lastDay;
                        //Assemble the whole date
                        string wholeDate = nizniz[0] + "-" + nizniz[1] + "-" + nizniz[2] + " " + niz[1];
                        t.To = Convert.ToDateTime(wholeDate);
                    }
                    t.TypeOfTicket = Enums.TypeOfTicket.MONTHLY;
                    t.Price = ticket.TotalPrice;
                }
                else if (ticket.TypeOfTicket == "YEARLY")
                {
                    t.From = DateTime.Now;
                    t.To = DateTime.Now;
                    t.To = t.To.AddYears(1);
                    if (t.From.Year != t.To.Year)
                    {
                        string ss = t.From.ToString();
                        string[] niz = ss.Split(' ');
                        string[] nizniz = niz[0].Split('-');
                        nizniz[0] = "31";
                        nizniz[1] = "Dec";
                        niz[1] = "11:59:59 PM";
                        string wholeDate = nizniz[0] + "-" + nizniz[1] + "-" + nizniz[2] + " " + niz[1];
                        t.To = Convert.ToDateTime(wholeDate);
                    }
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
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult UpdateProfile(RegisterUser userToUpdate)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(dbContext);
                var userManager = new UserManager<ApplicationUser>(userStore);
                string returnMessage = "";

                string s = User.Identity.GetUserId();
                var user = dbContext.Users.Any(u => u.Id == s);
                ApplicationUser apu = new ApplicationUser();
                apu = userManager.FindByIdAsync(s).Result;

                Passenger p = unitOfWork.PassengerRepository.Find(u => u.AppUserId == s).FirstOrDefault();
                Address a = unitOfWork.AddressRepository.Find(u => u.Id == p.Address_id).FirstOrDefault();

                if (!userManager.CheckPasswordAsync(apu, userToUpdate.OriginalPassword).Result)
                {
                    returnMessage = "You have entered an invalid password";
                    return Ok(returnMessage);
                }

                apu.PasswordHash = ApplicationUser.HashPassword(userToUpdate.Password);
                userManager.Update(apu);

                a.City = userToUpdate.City;
                a.StreetName = userToUpdate.StreetName;
                a.StreetNumber = userToUpdate.StreetNumber;
                unitOfWork.AddressRepository.Update(a);
                unitOfWork.Complete();

                p.Birthday = userToUpdate.Birthday;
                p.LastName = userToUpdate.Lastname;
                p.Name = userToUpdate.Name;
                p.PassengerType = userToUpdate.UserType;
                unitOfWork.PassengerRepository.Update(p);
                unitOfWork.Complete();
                returnMessage = "Profile successfully updated.";

                return Ok(returnMessage);
            }
            return Ok();
        }

        [Route("getProfileData")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetProfileData()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(dbContext);
                var userManager = new UserManager<ApplicationUser>(userStore);

                RegisterUser registerUser = new RegisterUser();
                string s = User.Identity.GetUserId();
                var user = dbContext.Users.Any(u => u.UserName == s);
                Passenger p = unitOfWork.PassengerRepository.Find(u => u.AppUserId == s).FirstOrDefault();
                Address a = unitOfWork.AddressRepository.Find(u => u.Id == p.Address_id).FirstOrDefault();
                registerUser.SendBackBirthday = Convert.ToDateTime(p.Birthday).ToString("yyyy-MM-dd");
                registerUser.City = a.City;
                registerUser.Lastname = p.LastName;
                registerUser.Name = p.Name;
                registerUser.StreetName = a.StreetName;
                registerUser.StreetNumber = a.StreetNumber;
                registerUser.Username = User.Identity.Name;
                registerUser.UserType = p.PassengerType;

                return Ok(registerUser);
            }
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
                List<StationHelp> stations = new List<StationHelp>();

                foreach (RouteStation rs in routeStations)
                {
                    Station station = unitOfWork.StationRepository.Get(rs.Station_id);
                    stations.Add(new StationHelp { X = station.X, Y = station.Y, Name = station.Name, IsStation = station.IsStation, Address = unitOfWork.AddressRepository.Get(station.Address_id) });
                }

                routes.Add(new Routes { RouteNumber = r.RouteNumber, RouteType = r.RouteType.ToString(), Stations = stations });
            }

            return Ok(routes);
        }

        [Route("getTickes")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetTickets()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<TicketHelp> ticketHelps = new List<TicketHelp>();
                int passenger_id = unitOfWork.PassengerRepository.GetAll().Where(x => x.AppUserId == User.Identity.GetUserId()).FirstOrDefault().Id;
                List<Ticket> tickets = unitOfWork.TicketRepository.GetAll().Where(x => x.Passenger_id == passenger_id && !x.IsDeleted).ToList();

                foreach(Ticket t in tickets)
                {
                    if (t.TypeOfTicket == Enums.TypeOfTicket.TIMED)
                    {
                        string s1 = t.From.ToString();
                        string[] niz = s1.Split(' ');
                        string s2 = t.To.ToString();
                        string[] niz1 = s2.Split(' ');
                        string time = niz[1] + " - " + niz1[1];
                        ticketHelps.Add(new TicketHelp { Id = t.Id, Price = t.Price.ToString(), Type = t.TypeOfTicket.ToString(), Date = time });
                    }
                    else
                    {
                        string time = t.From.Date.ToString().Split(' ')[0] + " - " + t.To.Date.ToString().Split(' ')[0];
                        ticketHelps.Add(new TicketHelp { Id = t.Id, Price = t.Price.ToString(), Type = t.TypeOfTicket.ToString(), Date = time });
                    }
                }

                return Ok(ticketHelps);
            }

            return Ok();
        }

        [Route("getTicket")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult GetTicket(int id)
        {
            List<string> messageToReturn = new List<string>();
            if (User.Identity.IsAuthenticated)
            {
                Ticket ticketToReturn = unitOfWork.TicketRepository.Find(t => t.Id == id).FirstOrDefault();
                if (ticketToReturn == null)
                {
                    messageToReturn.Add("The ticket with that id doesn't exist.");
                    return Ok(messageToReturn);
                }

                int result = DateTime.Compare(ticketToReturn.To, DateTime.Now);
                if (result < 0)
                {
                    messageToReturn.Add("This ticket is valid.");
                    messageToReturn.Add("From: " + ticketToReturn.ToString() + " To: " + ticketToReturn.ToString());
                    return Ok(messageToReturn);
                }
                else
                {
                    messageToReturn.Add("This ticket has expired.");
                    return Ok(messageToReturn);
                }
            }

            return Ok();
        }

        [HttpPost,Route("deleteTicket")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult DeleteTicket(TicketHelp ticketHelp)
        {
            if (User.Identity.IsAuthenticated)
            {
                Ticket ticket = unitOfWork.TicketRepository.Get(ticketHelp.Id);
                ticket.IsDeleted = true;
                unitOfWork.TicketRepository.Update(ticket);
                unitOfWork.Complete();
            }
            return Ok();
        }

        [Authorize(Roles = "Controller")]
        [Route("getUsers")]
        public IHttpActionResult GetUsers()
        {

            if (User.Identity.IsAuthenticated)
            {
                PassengerForVerification pas = new PassengerForVerification();
                var userStore = new UserStore<ApplicationUser>(dbContext);
                var userManager = new UserManager<ApplicationUser>(userStore);
                List<PassengerForVerification> passengerForVerifications = new List<PassengerForVerification>();
                List<Passenger> passengers = unitOfWork.PassengerRepository.GetAll().ToList();

                foreach (Passenger p in passengers)
                {
                    string s = User.Identity.GetUserId();
                    var user = dbContext.Users.Any(u => u.Id == s);
                    ApplicationUser apu = new ApplicationUser();
                    apu = userManager.FindByIdAsync(s).Result;
                    Address a = unitOfWork.AddressRepository.Find(aa=>aa.Id == p.Address_id).FirstOrDefault();

                    pas.Birthday = p.Birthday;
                    pas.Email = apu.UserName;
                    pas.Address = a.City + ", " + a.StreetName + " " + a.StreetNumber;
                    pas.IsValidated = p.IsValidated.ToString();
                    pas.LastName = p.LastName;
                    pas.Name = p.Name;
                    pas.PassengerType = p.ToString();
                    //pas.Picture
                    passengerForVerifications.Add(pas);
                }
                return Ok(pas);
            }

            return Ok();
        }

        private int GetLastDay(int month) {
            int res;

            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                res = 31;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                res = 30;
            }
            else {
                int year = DateTime.Now.Year;
                if (year % 4 == 0)
                {
                    res = 29;
                }
                else
                {
                    res = 28;
                }
            }

            return res;
        }

    }
}
