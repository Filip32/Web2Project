using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Address
    {
        private string streetName;
        private int streetNumber;
        private string city;
        private int id;

        public Address()
        {

        }

        public Address(string streetName, int streetNumber)
        {
            this.streetNumber = streetNumber;
            this.streetName = streetName;
        }


        public int StreetNumber
        {
            get
            {
                return streetNumber;
            }
            set
            {
                streetNumber = value;
            }

        }

        public string StreetName
        {
            get
            {
                return streetName;
            }
            set
            {
                streetName = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        public int Id { get { return Id; } }
    }
}