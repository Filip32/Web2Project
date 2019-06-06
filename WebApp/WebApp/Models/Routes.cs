using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Routes
    {
        public string RouteNumber { get; set; }
        public string RouteType { get; set; }
        public List<StationHelp> Stations { get; set; }

        public Routes()
        {
            Stations = new List<StationHelp>();
        }
    }
}