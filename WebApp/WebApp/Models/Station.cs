using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Station
    {
        private int id;
        private double x;
        private double y;
        private string name;
        private Address address;
        private List<Route> routes;

        public Station()
        {
            Routes = new List<Route>();
        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
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