using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Timetable
    {
        private int id;
        private Enums.TypeOfDay typeOfDay;
        private Enums.TypeOfRoute typeOfRoute;
        private List<Route> routes;

        public Timetable()
        {
            Routes = new List<Route>();
        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Enums.TypeOfDay TypeOfDay
        {
            get
            {
                return typeOfDay;
            }
            set
            {
                typeOfDay = value;
            }
        }

        public Enums.TypeOfRoute TypeOfRoute
        {
            get
            {
                return typeOfRoute;
            }
            set
            {
                typeOfRoute = value;
            }
        }

        public List<Route> Routes
        {
            get
            {
                return routes;
            }
            set
            {
                routes = value;
            }
        }
    }
}