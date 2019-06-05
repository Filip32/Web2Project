using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        private Enums.TypeOfTicket typeOfTicket;
        private float price;
        private DateTime from;
        private DateTime to;
        private int passenger_id;
        private bool isDeleted;
        private int id;

        public Ticket()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public int Passenger_id
        {
            get
            {
                return passenger_id;
            }
            set
            {
                passenger_id = value;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value;
            }
        }

        public float Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public DateTime From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
            }
        }

        public DateTime To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
            }
        }
    }
}