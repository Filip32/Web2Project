using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Passenger
    {
        int id;
        int appUserId;
        private Enums.StateType isValidated;
        private string name;
        private string lastName;
        private int address_id;
        private DateTime birthday;
        private string picture;
        private Enums.PassengerType passengerType;

        public Passenger():base()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int AppUserId
        {
            get { return appUserId; }
            set { appUserId = value; }
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

        public Enums.StateType IsValidated
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

        public int Address_id
        {
            get
            {
                return address_id;
            }
            set
            {
                address_id = value;
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