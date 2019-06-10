using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PassengerForVerification
    {
        private string isValidated;
        private string name;
        private string lastName;
        private DateTime birthday;
        private string picture;
        private string passengerType;
        private string email;
        private string address;

        public string Address { get { return address; } set { address = value; } }

        public string Email { get { return email; } set { email = value; } }

        public PassengerForVerification()
        {

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

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public string IsValidated
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

        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
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

        public string PassengerType
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