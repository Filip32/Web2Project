using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Route
    {
        private List<int> stations;
        private Enums.TypeOfRoute routeType;
        private string departures;
        private int id;
        private string routeNumber;
        private List<int> vehicles;

        public Route()
        {
            Vehicles = new List<int>();
            Stations = new List<int>();
        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<int> Stations
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
        public string Departures
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

        public List<int> Vehicles
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