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
    [RoutePrefix("api/Timetable")]
    public class TimetableAdminController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IUnitOfWork unitOfWork;
        private static object locka = new object();
        private static object lockb = new object();
        private static object lockc = new object();
        private static object lockd = new object();
        private static object locke = new object();

        public TimetableAdminController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [HttpGet, Route("getRoutesAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetRoutesAdmin()
        {
            try
            {
                List<RouteHelp> routes = new List<RouteHelp>();

                List<Route> routesDb = unitOfWork.RouteRepository.GetAll().Where(x => !x.Deleted).ToList();

                foreach (Route l in routesDb)
                {
                    routes.Add(new RouteHelp { Id = l.Id, RouteNumber = l.RouteNumber, RouteType = l.RouteType.ToString(), Day = l.DayType.ToString() });
                }

                routes = routes.OrderBy(x => x.RouteType).ToList();

                return Ok(routes);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getRouteListAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetRouteListAdmin(int id)
        {
            try
            {
                Route l = unitOfWork.RouteRepository.Get(id);
                string[] departures = l.Departures.Split('.');

                List<string> list = new List<string>();
                foreach (var s in departures)
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

        [HttpGet, Route("getRouteAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetRouteAdmin(int id)
        {
            try
            {
                Route l = unitOfWork.RouteRepository.Get(id);
                string departures = l.Departures;
                string[] vremena = departures.Split('.');
                string ret = "";
                foreach (var s in vremena)
                {
                    ret += s + '\n';
                }
                return Ok(ret);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getTypeOfDay")]
        public IHttpActionResult GetTypeOfDay()
        {
            try
            {
                List<string> list = new List<string>();
                list.Add(Enums.TypeOfDay.WORKDAY.ToString());
                list.Add(Enums.TypeOfDay.SATURDAY.ToString());
                list.Add(Enums.TypeOfDay.SUNDAY.ToString());
                return Ok(list);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("changeRouteNumberAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult ChangeRouteNumberAdmin(DepartureHelp  departureHelp)
        {
            try
            {
                lock (locka) {
                    Route l = unitOfWork.RouteRepository.Get(departureHelp.Id);

                    l.RouteNumber = departureHelp.RouteNumber;

                    unitOfWork.RouteRepository.Update(l);
                    unitOfWork.Complete();

                    return Ok("Route Number changed.");
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("chageDayRouteAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult ChageDayRouteAdmin(DepartureHelp  departureHelp)
        {
            try
            {
                lock (lockb)
                {
                    Route l = unitOfWork.RouteRepository.Get(departureHelp.Id);

                    if (String.Compare(departureHelp.Day.ToUpper(), Enums.TypeOfDay.WORKDAY.ToString()) == 0)
                        l.DayType = Enums.TypeOfDay.WORKDAY;
                    else if (String.Compare(departureHelp.Day.ToUpper(), Enums.TypeOfDay.SATURDAY.ToString()) == 0)
                        l.DayType = Enums.TypeOfDay.SATURDAY;
                    else if (String.Compare(departureHelp.Day.ToUpper(), Enums.TypeOfDay.SUNDAY.ToString()) == 0)
                        l.DayType = Enums.TypeOfDay.SUNDAY;

                    unitOfWork.RouteRepository.Update(l);
                    unitOfWork.Complete();

                    return Ok("Day of  route changed.");
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("changeDepAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult ChangeDepAdmin(DepartureHelp departureHelp)
        {
            try
            {
                lock (lockc) {
                    Route l = unitOfWork.RouteRepository.Get(departureHelp.Id);

                    string[] vremena = departureHelp.Departures.Split('\n');
                    string s = "";
                    foreach (var ss in vremena)
                    {
                        s += ss + '.';
                    }
                    l.Departures = s;

                    unitOfWork.RouteRepository.Update(l);
                    unitOfWork.Complete();

                    return Ok("Polasci uspešno izmenjeni...");
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("deleteRouteAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteRouteAdmin(int id)
        {
            try
            {
                lock (lockd)
                {
                    Route l = unitOfWork.RouteRepository.Get(id);
                    l.Deleted = true;

                    unitOfWork.RouteRepository.Update(l);
                    unitOfWork.Complete();

                    return Ok("Route deleted.");
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet, Route("getTypeOfTimetable")]
        public IHttpActionResult GetTypeOfTimetable()
        {
            try
            {
                List<string> list = new List<string>();
                list.Add(Enums.TypeOfRoute.TOWN.ToString());
                list.Add(Enums.TypeOfRoute.SUBURBAN.ToString());
                return Ok(list);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost, Route("addNewRouteAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddNewRouteAdmin(DepartureHelp  departureHelp)
        {
            try
            {
                lock (locke)
                {
                    Route l = new Route();
                    l.Deleted = false;
                    l.RouteNumber = departureHelp.RouteNumber;
                    l.Departures = departureHelp.Departures;

                    if (String.Compare(departureHelp.TyoeOfRoute.ToUpper(), Enums.TypeOfRoute.TOWN.ToString()) == 0)
                        l.RouteType = Enums.TypeOfRoute.TOWN;
                    else
                        l.RouteType = Enums.TypeOfRoute.SUBURBAN;

                    if (String.Compare(departureHelp.Day, Enums.TypeOfDay.WORKDAY.ToString()) == 0)
                        l.DayType = Enums.TypeOfDay.WORKDAY;
                    else if (String.Compare(departureHelp.Day, Enums.TypeOfDay.SATURDAY.ToString()) == 0)
                        l.DayType = Enums.TypeOfDay.SATURDAY;
                    else if (String.Compare(departureHelp.Day, Enums.TypeOfDay.SUNDAY.ToString()) == 0)
                        l.DayType = Enums.TypeOfDay.SUNDAY;

                    unitOfWork.RouteRepository.Add(l);
                    unitOfWork.Complete();

                    return Ok("New route added.");
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
