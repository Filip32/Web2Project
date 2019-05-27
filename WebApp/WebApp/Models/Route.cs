using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Route
    {
        private List<Station> stations;
        private Enums.TypeOfRoute routeType;
        private List<DateTime> departures;
        private int id;
        private string routeNumber;
        private List<Vehicle> vehicles;

        public Route()
        {
            Vehicles = new List<Vehicle>();
        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<Station> Stations
        {
            get
            {
                return stations;
            }
            set
            {
                stations = value;
            }
        }
        public List<DateTime> Departures
        {
            get
            {
                return departures;
            }
            set
            {
                departures = value;
            }
        }
        public Enums.TypeOfRoute RouteType
        {
            get
            {
                return routeType;
            }
            set
            {
                routeType = value;
            }
        }

        public string RouteNumber
        {
            get
            {
                return routeNumber;
            }
            set
            {
                routeNumber = value;
            }
        }

        public List<Vehicle> Vehicles
        {
            get
            {
                return vehicles;
            }
            set
            {
                vehicles = value;
            }
        }
    }
}