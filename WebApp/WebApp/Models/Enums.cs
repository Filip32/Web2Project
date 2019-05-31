using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Enums
    {
        public enum PassengerType { REGULAR = 0,PENSIONER, STUDENT }
        public enum TypeOfTicket { TIMED = 0, DIALY, MONTHLY, YEARLY}
        public enum TypeOfRoute { TOWN = 0, SUBURBAN}
        public enum TypeOfDay { WORKDAY = 0, SATURDAY, SUNDAY }
        public enum StateType { UNVERIFIED = 0, ACCEPTED, DENIED }
    }
}