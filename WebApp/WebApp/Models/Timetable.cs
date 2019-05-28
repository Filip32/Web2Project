using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApp.Models
{
    public class Timetable
    {
        private int id;
        private Enums.TypeOfDay typeOfDay;
        private List<Route> routes;

        public Timetable()
        {
            Route = new List<Route>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<Route> Route
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
    }
}