using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RouteHelp
    {
        public int Id { get; set; }
        public string RouteNumber { get; set; }
        public Enums.TypeOfRoute TypeOfRoute { get; set; }
        public string Day { get; set; }
        public List<StationHelp> Stations { get; set; }
        public string RouteType { get; set; }

        public RouteHelp()
        {
            Stations = new List<StationHelp>();
        }
    }
}