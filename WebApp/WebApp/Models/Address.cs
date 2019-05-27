using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
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
    }
}