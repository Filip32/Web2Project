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
        private int pricelistItem_id;
        private DateTime from;
        private DateTime to;
        private int passenger_id;
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

        public int PricelistItem_id
        {
            get
            {
                return pricelistItem_id;
            }
            set
            {
                pricelistItem_id = value;
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