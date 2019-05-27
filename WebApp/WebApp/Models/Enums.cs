using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Enums
    {
        public enum PassengerType { REGULAR = 0,PENSIONER, STUDENT }
        public enum TypeOfTicket { TIMED, DIALY, MONTHLY, YEARLY}
        public enum TypeOfRoute { TOWN, SUBURBAN}
    }
}