using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public Address(object json)
        {
            JObject jObject = JObject.Parse(json.ToString());
            JToken jUser = jObject;
            streetName = (string)jUser["streetName"];
            streetNumber = (int)jUser["streetNumber"];
            city = (string)jUser["city"];
        }

        public Address()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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