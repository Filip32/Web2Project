using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AddLinesHelper
    {
        public int Id;
        public string RouteNumber;
        public List<Dot> dots;

        public AddLinesHelper()
        {
            dots = new List<Dot>();
        }
    }
}