using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class StationHelper
    {
        public double X;
        public double Y;
        public string Name;
        public string Address;
        public string RouteNumber;
        public int IdStation;
        public List<int> IdRoute;
        public List<string> RouteNumbers;
        public bool IsStation;
        public List<int> StationNumbers;
        public int NumberInRoute;
        public string LastUpdate { get; set; }

        public StationHelper()
        {
            StationNumbers = new List<int>();
            IdRoute = new List<int>();
            RouteNumbers = new List<string>();
        }
    }
}