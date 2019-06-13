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
    [RoutePrefix("api/Stations")]
    public class StationAdminController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IUnitOfWork unitOfWork;

        public StationAdminController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [Route("getStationsAdmin")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult getStations()
        {
            List<StationHelper> stationHelper = new List<StationHelper>();

            List<Station> stations = unitOfWork.StationRepository.GetAll().ToList();

            foreach (Station s in stations)
            {
                if (s.IsStation)
                {
                    Address a = unitOfWork.AddressRepository.Get(s.Address_id);
                    List<RouteStation> routeStations = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Station_id == s.Id).ToList();

                    List<int> routesId = new List<int>();
                    List<string> routesName = new List<string>();
                    List<int> stationNumbers = new List<int>();
                    string routesNamee = "";

                    foreach (RouteStation rs in routeStations)
                    {
                        Route rr = unitOfWork.RouteRepository.Get(rs.Route_id);
                        routesName.Add(rr.RouteNumber);
                        routesId.Add(rr.Id);
                        stationNumbers.Add(rs.Station_num);
                        routesNamee += "[" + rr.RouteNumber + "] ";
                    }

                    stationHelper.Add(new StationHelper()
                    {
                        X = s.X,
                        Y = s.Y,
                        Name = s.Name,
                        IdStation = s.Id,
                        IsStation = s.IsStation,
                        Address = a.City + ", " + a.StreetName + " " + a.StreetNumber,
                        IdRoute = routesId,
                        RouteNumber = routesNamee,
                        RouteNumbers = routesName,
                        StationNumbers = stationNumbers
                    });
                }
            }

            return Ok(stationHelper);
        }

        [Route("getRoutesAddStation")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult getRoutesAddStation()
        {
            List<Routes> routes = new List<Routes>();
            List<Route> r = unitOfWork.RouteRepository.GetAll().Where(x => x.DayType == Enums.TypeOfDay.WORKDAY).ToList();

            foreach(Route rr in r)
            {
                routes.Add(new Routes() { Id = rr.Id, RouteNumber = rr.RouteNumber });
            }
            return Ok(routes);
        }

        [Route("getNewRoutes")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult getNewRoutes()
        {
            List<Routes> routes = new List<Routes>();
            List<Route> r = unitOfWork.RouteRepository.GetAll().Where(x => x.DayType == Enums.TypeOfDay.WORKDAY).ToList();

            foreach (Route rr in r)
            {
                List<RouteStation> rs = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Route_id == rr.Id).ToList();
                if (rs.Count == 0)
                {
                    routes.Add(new Routes() { Id = rr.Id, RouteNumber = rr.RouteNumber });
                }
            }
            return Ok(routes);
        }
        
    }
}
