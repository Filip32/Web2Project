using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Helper
{
    public static class HelperReader
    {
        static ApplicationDbContext dbContext = new ApplicationDbContext();
        static IUnitOfWork unitOfWork;

        public static void Reader(IUnitOfWork uw)
        {
            unitOfWork = uw;
            bool state = true;
            string line;
            int idRoute = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteNumber == "32B").FirstOrDefault().Id;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\filip\Desktop\Web2\Web2Project\32B.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line == "-")
                {
                    state = false;
                    continue;
                }

                if (state) // dots
                {
                    HelperReader.DoDot(line, idRoute);
                }
                else // stations
                {
                    HelperReader.DoStation(line, idRoute);
                }
            }

            file.Close();
        }

        private static void DoDot(string dot, int idRoute)
        {
            string []split = dot.Split(',');
            double X = Convert.ToDouble(split[0]);
            double Y = Convert.ToDouble(split[1]);
            int idStation;

            Station s = unitOfWork.StationRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault();

            if (s == null)
            {
                // dodati station
                Station station = new Station() { X = X, Y = Y, Name = "", IsStation = false, Address_id = -1 };
                unitOfWork.StationRepository.Add(station);
                unitOfWork.Complete();
                idStation = unitOfWork.StationRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault().Id;
            }
            else
            {
                idStation = s.Id;
            }

            // dodati statinoRoute
            RouteStation routeStation = new RouteStation() { Route_id = idRoute, Station_id = idStation };
            unitOfWork.RouteStationRepositpry.Add(routeStation);
            unitOfWork.Complete();
        }

        private static void DoStation(string station, int idRoute)
        {
            string[] split = station.Split('|');
            double Y = Convert.ToDouble(split[0]);
            double X = Convert.ToDouble(split[1]);
            string Name = split[2];

            int idStation;

            Station s = unitOfWork.StationRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault();

            if (s == null)
            {
                Address address = new Address() { City = split[3], StreetName = split[4], StreetNumber = Int32.Parse(split[5]) };
                unitOfWork.AddressRepository.Add(address);
                unitOfWork.Complete();
                int idAddress = unitOfWork.AddressRepository.GetAll().Where(x => x.City == split[3] && x.StreetName == split[4]  && x.StreetNumber == Int32.Parse(split[5])).FirstOrDefault().Id;

                // dodati station                                                               
                Station stationA = new Station() { X = X, Y = Y, Name = Name, IsStation = true, Address_id = idAddress };
                unitOfWork.StationRepository.Add(stationA);
                unitOfWork.Complete();
                idStation = unitOfWork.StationRepository.GetAll().Where(x => x.X == X && x.Y == Y).FirstOrDefault().Id;
            }
            else
            {
                idStation = s.Id;
            }

            // dodati statinoRoute
            RouteStation routeStation = new RouteStation() { Route_id = idRoute, Station_id = idStation };
            unitOfWork.RouteStationRepositpry.Add(routeStation);
            unitOfWork.Complete();
        }
    }
}