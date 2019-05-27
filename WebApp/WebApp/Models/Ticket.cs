using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        private Enums.TypeOfTicket ticketType;
        private DateTime from;
        private DateTime to;
        private int id;

        public Ticket()
        {

        }

        public Enums.TypeOfTicket TicketType
        {
            get
            {
                return ticketType;
            }
            set
            {
                ticketType = value;
            }
        }
        public int Id
        {
            get { return id; }
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