using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PricelistHelp
    {
        public int Id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TimePrice { get; set; }
        public string DailyPrice { get; set; }
        public string MonthlyPrice { get; set; }
        public string YearlyPrice { get; set; }
        public bool Change { get; set; }
        public string LastUpdate { get; set; }

        public PricelistHelp() { }
    }
}