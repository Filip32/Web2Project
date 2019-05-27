using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Item
    {
        private int id;
        private Enums.TypeOfTicket typeOfTicket;

        public Item()
        {

        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Enums.TypeOfTicket TypeOfTicket
        {
            get
            {
                return typeOfTicket;
            }
            set
            {
                typeOfTicket = value;
            }
        }
    }
}