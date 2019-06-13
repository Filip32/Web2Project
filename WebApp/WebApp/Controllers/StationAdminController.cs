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

        [Route("saveStationChanges")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult saveStationChanges(StationHelper sh)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {

                    Station station = unitOfWork.StationRepository.Find(u => u.Id == sh.IdStation).FirstOrDefault();
                    station.Name = sh.Name;
                    unitOfWork.StationRepository.Update(station);
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

        [Route("deleteStationFromRoute")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult deleteStationFromRoute(StationHelper sh)
        {
            int IdRoute = Int32.Parse(sh.RouteNumber);
            RouteStation rs = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Route_id == IdRoute && x.Station_id == sh.IdStation).FirstOrDefault();
            unitOfWork.RouteStationRepositpry.Remove(rs);
            unitOfWork.Complete();

            List<RouteStation> routeStations = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Station_id == sh.IdStation).ToList();

            if (routeStations.Count == 0)
            {
                Station station = unitOfWork.StationRepository.Get(sh.IdStation);
                station.IsStation = false;
                unitOfWork.StationRepository.Update(station);
                unitOfWork.Complete();
            }

            return Ok();
        }

        [Route("addStation")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult addStation(StationHelper sh)
        {
            string[] split = sh.Address.Split(',');
            Address address = new Address() { City = split[0], StreetName = split[1], StreetNumber = Int32.Parse(split[2]) };
            unitOfWork.AddressRepository.Add(address);
            unitOfWork.Complete();
            
            address = unitOfWork.AddressRepository.GetAll().Where(x => x.City == split[0] && x.StreetName == split[1] && x.StreetNumber == Int32.Parse(split[2])).FirstOrDefault();
            Station station = new Station() { Name = sh.Name, X = sh.X, Y = sh.Y, IsStation = true , Address_id = address.Id};
            unitOfWork.StationRepository.Add(station);
            unitOfWork.Complete();
            station = unitOfWork.StationRepository.GetAll().Where(x => x.Address_id == address.Id && x.X == sh.X && x.Y == sh.Y).FirstOrDefault();

            RouteStation routeStation = new RouteStation()
            {
                Route_id = sh.IdRoute[0],
                Station_id = station.Id,
                Station_num = sh.NumberInRoute
            };

            List<RouteStation> routeStations = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Route_id == sh.IdRoute[0] && x.Station_num > 0).ToList();
            routeStations = routeStations.OrderBy(x => x.Station_num).ToList();
            List<RouteStation> list = routeStations.Where(x => x.Station_num == routeStation.Station_num).ToList();

            int count = routeStation.Station_num;
            if (list.Count != 0)
            {
                foreach (RouteStation rs in routeStations)
                {
                    if (count == rs.Station_num)
                    {
                        RouteStation pom = unitOfWork.RouteStationRepositpry.Get(rs.Id);
                        pom.Station_num++;
                        count++;
                        unitOfWork.RouteStationRepositpry.Update(pom);
                        unitOfWork.Complete();
                    }
                }
            }

            unitOfWork.RouteStationRepositpry.Add(routeStation);
            unitOfWork.Complete();
            return Ok();
        }

        [Route("addLines")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IHttpActionResult addLines(AddLinesHelper alh)
        {
            foreach(Dot dot in alh.dots)
            {
                Station station = new Station() { Address_id = -1, X = dot.X, Y = dot.Y, Name = "", IsStation = false };
                unitOfWork.StationRepository.Add(station);
                unitOfWork.Complete();
                station = unitOfWork.StationRepository.GetAll().Where(x => x.X == dot.X && x.Y == dot.Y).FirstOrDefault();
                unitOfWork.RouteStationRepositpry.Add(new RouteStation() { Station_id = station.Id, Route_id = alh.Id, Station_num = 0 });
                unitOfWork.Complete();
            }
            return Ok();
        }

    }
}
