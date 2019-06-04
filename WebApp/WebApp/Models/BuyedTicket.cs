using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BuyedTicket
    {
        private string typeOfUser;
        private string typeOfTicket;
        private float totalPrice;

        public BuyedTicket() { }

        public string TypeOfUser { get { return typeOfUser; } set { typeOfUser = value; } }
        public string TypeOfTicket { get { return typeOfTicket; } set { typeOfTicket  = value; } }
        public float TotalPrice { get { return totalPrice; } set { totalPrice  = value; } }
    }
}