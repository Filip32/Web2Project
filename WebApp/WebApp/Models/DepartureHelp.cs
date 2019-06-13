using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DepartureHelp
    {
        public string Departures { get; set; }
        public int Id { get; set; }
        public string RouteNumber { get; set; }
        public string Day { get; set; }
        public string TyoeOfRoute { get; set; }

        public DepartureHelp() { }
    }
}