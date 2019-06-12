using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Models
{
    public class RegisterUser
    {
        private string username;
        private string password;
        private string originalPassword;
        private string name;
        private string lastname;
        private DateTime birthday;
        private string sendBackBirthday;
        private string streetName;
        private int streetNumber;
        private string city;
        private Enums.PassengerType userType;
        private string photo;

        public RegisterUser()
        {

        }
        public string Photo { get { return photo; } set { photo = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string OriginalPassword { get { return originalPassword; } set { originalPassword = value; } }
        public string SendBackBirthday { get { return sendBackBirthday; } set { sendBackBirthday = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Lastname { get { return lastname; } set { lastname = value; } }
        public DateTime Birthday { get { return birthday; } set { birthday = value; } }
        public string StreetName { get { return streetName; } set { streetName = value; } }
        public int StreetNumber { get { return streetNumber; } set { streetNumber = value; } }
        public string City { get { return city; } set { city = value; } }
        public Enums.PassengerType UserType { get { return userType; } set { userType = value; } }


    }
}