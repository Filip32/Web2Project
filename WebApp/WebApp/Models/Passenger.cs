using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Passenger:Person
    {
        private bool isValidated;
        private string picture;
        private Enums.PassengerType passengerType;

        public Passenger():base()
        {

        }

        public bool IsValidated
        {
            get
            {
                return isValidated;
            }
            set
            {
                isValidated = value;
            }
        }

        public string Picture
        {
            get
            {
                return picture;
            }
            set
            {
                picture = value;
            }
        }

        public Enums.PassengerType PassengerType
        {
            get
            {
                return passengerType;
            }
            set
            {
                passengerType = value;
            }
        }
    }
}