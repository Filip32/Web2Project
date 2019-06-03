using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public Route()
        {
            Stations = new List<int>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    }
}