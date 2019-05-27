using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Vehicle
    {
        private int id;
        private Route route;
        private Location location;

        public Vehicle()
        {

        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Route Route
        {
            get
            {
                return route;
            }
            set
            {
                route = value;
            }
        }

        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
    }
}