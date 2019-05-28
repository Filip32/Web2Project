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
        private PricelistItem pricelistItem;
        private DateTime from;
        private DateTime to;
        private Passenger passenger;
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

        public PricelistItem PricelistItem
        {
            get
            {
                return pricelistItem;
            }
            set
            {
                pricelistItem = value;
            }
        }

        public Passenger Passenger
        {
            get
            {
                return passenger;
            }
            set
            {
                passenger = value;
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