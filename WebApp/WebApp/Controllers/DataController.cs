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
using WebApp.Hubs;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.IO;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Data")]
    public class DataController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IUnitOfWork unitOfWork;

        public DataController(IUnitOfWork uw)
        {
            //HelperReader.Reader(uw, "1A");
            //HelperReader.Reader(uw, "1B");
            //HelperReader.Reader(uw, "4A");
            //HelperReader.Reader(uw, "4B");
            //HelperReader.Reader(uw, "12A");
            //HelperReader.Reader(uw, "12B");
            //HelperReader.Reader(uw, "13A");
            //HelperReader.Reader(uw, "13B");
            //HelperReader.Reader(uw, "16A");
            //HelperReader.Reader(uw, "16B");
            //HelperReader.Reader(uw, "22A");
            //HelperReader.Reader(uw, "22B");
            //HelperReader.Reader(uw, "32B");
            //HelperReader.Reader(uw, "41A");
            //HelperReader.Reader(uw, "41B");
            //HelperReader.Reader(uw, "51B");
            unitOfWork = uw;
        }

        [Route("getPricelist")]
        public IHttpActionResult GetPricelist()
        {
             try
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
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("getCoefficient")]
        public IHttpActionResult GetCoefficient()
        {
            try
            {
                Coefficients coefficients = unitOfWork.CoefficientRepository.GetAll().FirstOrDefault();
                return Ok(coefficients);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("getTypeOfLoginUser")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetTypeOfLoginUser()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string s = User.Identity.GetUserId();
                    Passenger passenger = unitOfWork.PassengerRepository.GetAll().Where(x => x.AppUserId == s).FirstOrDefault();
                    string message = "{\"TypeOfUser\" : \"" + passenger.PassengerType.ToString() + "\",";
                    message += "\"IsValid\" : \"" + passenger.IsValidated + "\"}";
                    return Ok(message);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("buyTicket")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult BuyTicket(BuyedTicket ticket)
        {
            try
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
                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("updateProfile")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult UpdateProfile(RegisterUser userToUpdate)
        {
            try
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
                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("getProfileData")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetProfileData()
        {
            try
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
					registerUser.Photo = p.Picture;

					return Ok(registerUser);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("getRoutes")]
        public IHttpActionResult GetRoutes()
        {
            try
            {
                List<Routes> routes = new List<Routes>();

                List<Route> routesDb = unitOfWork.RouteRepository.GetAll().Where(x => x.DayType == Enums.TypeOfDay.WORKDAY).ToList();

                foreach (Route r in routesDb)
                {
                    routes.Add(new Routes { Id = r.Id, RouteNumber = r.RouteNumber, RouteType = r.RouteType.ToString() });
                }

                return Ok(routes);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getRoute")]
        public IHttpActionResult GetRoute(int id)
        {
            try
            {
                Route route = unitOfWork.RouteRepository.Get(id);

                List<RouteStation> routeStations = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Route_id == route.Id).ToList();
                List<StationHelp> stations = new List<StationHelp>();

                foreach (RouteStation rs in routeStations)
                {
                    Station station = unitOfWork.StationRepository.Get(rs.Station_id);
                    if (station != null)
                        stations.Add(new StationHelp { X = station.X, Y = station.Y, Name = station.Name, IsStation = station.IsStation, Address = unitOfWork.AddressRepository.Get(station.Address_id) });
                }
                Routes routes = new Routes() { Id = route.Id, RouteNumber = route.RouteNumber, RouteType = route.RouteType.ToString() };
                routes.Stations = stations;
                return Ok(routes);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("getTickes")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult GetTickets()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    List<TicketHelp> ticketHelps = new List<TicketHelp>();
                    int passenger_id = unitOfWork.PassengerRepository.GetAll().Where(x => x.AppUserId == User.Identity.GetUserId()).FirstOrDefault().Id;
                    List<Ticket> tickets = unitOfWork.TicketRepository.GetAll().Where(x => x.Passenger_id == passenger_id && !x.IsDeleted).ToList();

                    foreach (Ticket t in tickets)
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

                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getTicket")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult GetTicket(int id)
        {
            try
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
                    if (result > 0)
                    {
                        messageToReturn.Add("This ticket is valid.\n");
                        messageToReturn.Add("From: " + ticketToReturn.From.ToString() + "\nTo: " + ticketToReturn.To.ToString());
                        return Ok(messageToReturn);
                    }
                    else
                    {
                        messageToReturn.Add("This ticket has expired.");
                        return Ok(messageToReturn);
                    }
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("deleteTicket")]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult DeleteTicket(TicketHelp ticketHelp)
        {
            try
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
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("approveUser")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult ApproveUser(PassengerForVerification passengerForVerification)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
					var user = dbContext.Users.Any(u => u.UserName == passengerForVerification.Email);
					Passenger p = unitOfWork.PassengerRepository.Find(u => u.AppUserId == passengerForVerification.Email).FirstOrDefault();
					p.IsValidated = Enums.StateType.ACCEPTED;
					unitOfWork.PassengerRepository.Update(p);
					unitOfWork.Complete();
					SendMail(passengerForVerification.Email,Enums.StateType.ACCEPTED);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("denyUser")]
        [Authorize(Roles = "Controller")]
        public IHttpActionResult DenyUser(PassengerForVerification passengerForVerification)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
					var user = dbContext.Users.Any(u => u.UserName == passengerForVerification.Email);
					Passenger p = unitOfWork.PassengerRepository.Find(u => u.AppUserId == passengerForVerification.Email).FirstOrDefault();
					p.IsValidated = Enums.StateType.DENIED;
					unitOfWork.PassengerRepository.Update(p);
					unitOfWork.Complete();
					SendMail(passengerForVerification.Email, Enums.StateType.DENIED);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize(Roles = "Controller")]
        [HttpGet,Route("getUsers")]
        public IHttpActionResult GetUsers()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    // da li da ubacim uslov za samo ne verifikovane da izbacuje 
                    var userStore = new UserStore<ApplicationUser>(dbContext);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    List<PassengerForVerification> passengerForVerifications = new List<PassengerForVerification>();
                    List<Passenger> passengers = unitOfWork.PassengerRepository.GetAll().ToList();

                    foreach (Passenger p in passengers)
                    {
                        //string s = User.Identity.GetUserId();
                        //var user = dbContext.Users.Any(u => u.Id == p.AppUserId);
                        //ApplicationUser apu = new ApplicationUser();
                        PassengerForVerification pas = new PassengerForVerification();
                        ApplicationUser apu = userManager.FindByIdAsync(p.AppUserId).Result;
                        Address a = unitOfWork.AddressRepository.Find(aa => aa.Id == p.Address_id).FirstOrDefault();

                        pas.Birthday = Convert.ToDateTime(p.Birthday).ToString("yyyy-MM-dd");
                        pas.Email = apu.UserName;
                        pas.Address = a.City + ", " + a.StreetName + " " + a.StreetNumber;
                        pas.IsValidated = p.IsValidated.ToString();
                        pas.LastName = p.LastName;
                        pas.Name = p.Name;
                        pas.PassengerType = p.PassengerType.ToString();
                        //pas.Picture
                        passengerForVerifications.Add(pas);
                    }
                    return Ok(passengerForVerifications);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [ActionName("UploadDishImage")]
        public HttpResponseMessage UploadJsonFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                    Passenger p = unitOfWork.PassengerRepository.Find(u => u.AppUserId == file).FirstOrDefault();
                    p.Picture = filePath;
                    if (p.IsValidated != Enums.StateType.UNVERIFIED)
                    {
                        p.IsValidated = Enums.StateType.UNVERIFIED;
                    }
                    unitOfWork.PassengerRepository.Update(p);
                    unitOfWork.Complete();
                    postedFile.SaveAs(filePath);
                }
            }
            return response;
        }

        [HttpGet, Route("getPhoto")]
        public IHttpActionResult GetPhoto(string id)
        {
            Passenger k = unitOfWork.PassengerRepository.Find(u => u.AppUserId == id).FirstOrDefault();

            if (k.Picture == null)
            {
                return Ok("Nema slike");
            }

            var filePath = k.Picture;

            FileInfo fileInfo = new FileInfo(filePath);
            string type = fileInfo.Extension.Split('.')[1];
            byte[] data = new byte[fileInfo.Length];

            HttpResponseMessage response = new HttpResponseMessage();
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(data);
                response.Content.Headers.ContentLength = data.Length;

            }

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/png");

            return Ok(data);
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

        [HttpGet, Route("getRoutesCityWorkday")]
        public IHttpActionResult GetRoutesCityWorkday()
        {
            try
            {
                List<string> lista = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteType == Enums.TypeOfRoute.TOWN && x.DayType == Enums.TypeOfDay.WORKDAY && !x.Deleted).Select(x => x.RouteNumber).ToList();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getRoutesCitySaturday")]
        public IHttpActionResult GetRoutesCitySaturday()
        {
            try
            {
                List<string> lista = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteType == Enums.TypeOfRoute.TOWN && x.DayType == Enums.TypeOfDay.SATURDAY && !x.Deleted).Select(x => x.RouteNumber).ToList();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getRoutesCitySunday")]
        public IHttpActionResult GetRoutesCitySunday()
        {
            try
            {
                List<string> lista = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteType == Enums.TypeOfRoute.TOWN && x.DayType == Enums.TypeOfDay.SUNDAY && !x.Deleted).Select(x => x.RouteNumber).ToList();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getRoutesSuburbanWorkday")]
        public IHttpActionResult GetRoutesSuburbanWorkday()
        {
            try
            {
                List<string> lista = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteType == Enums.TypeOfRoute.SUBURBAN && x.DayType == Enums.TypeOfDay.WORKDAY && !x.Deleted).Select(x => x.RouteNumber).ToList();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getRoutesSuburbanSaturday")]
        public IHttpActionResult GetRoutesSuburbanSaturday()
        {
            try
            {
                List<string> lista = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteType == Enums.TypeOfRoute.SUBURBAN && x.DayType == Enums.TypeOfDay.SATURDAY && !x.Deleted).Select(x => x.RouteNumber).ToList();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getRoutesSuburbanSunday")]
        public IHttpActionResult GetRoutesSuburbanSunday()
        {
            try
            {
                List<string> lista = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteType == Enums.TypeOfRoute.SUBURBAN && x.DayType == Enums.TypeOfDay.SUNDAY && !x.Deleted).Select(x => x.RouteNumber).ToList();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpPost, Route("timetable")]
        public IHttpActionResult GetTimetable(object par)
        {
            try
            {
                JObject jObject = JObject.FromObject(par);
                JToken jUser = jObject;
                string timetable = (string)jUser["timetable"];
                string day = (string)jUser["day"];
                string route = (string)jUser["route"];

                Enums.TypeOfDay d = Enums.TypeOfDay.WORKDAY;
                if (String.Compare(day.ToUpper(), d.ToString()) == 0)
                    d = Enums.TypeOfDay.WORKDAY;
                else if (String.Compare(day.ToUpper(), Enums.TypeOfDay.SATURDAY.ToString()) == 0)
                    d = Enums.TypeOfDay.SATURDAY;
                else
                    d = Enums.TypeOfDay.SUNDAY;

                List<string> departures = unitOfWork.RouteRepository.Find(x => x.RouteNumber == route && x.DayType == d).Select(x => x.Departures).ToList();
                string[] time = departures[0].Split('.');

                List<string> list = new List<string>();
                foreach (var s in time)
                {
                    list.Add(s);
                }
                return Ok(list);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        private void SendMail(string emailTo, Enums.StateType state)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("busns021@gmail.com", "Test123!");

            string body = "";
            string subject = "BusNs status update";
            if (state == Enums.StateType.ACCEPTED)
            {
                body = "Hello,\n\n We are glad to inform you that your status has been changed to Accepted.\n\n Best regards,\nBus Ns";
            }
            else if (state == Enums.StateType.DENIED)
            {
                body = "Hello,\n\n We regret to inform you that your status has been changed to Denied.\n\n Best regards,\nBus Ns";
            }

            MailMessage mm = new MailMessage("busns021@gmail.com", emailTo, subject, body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

    }
}
