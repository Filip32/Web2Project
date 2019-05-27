using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Pricelist
    {
        private int id;
        private DateTime from;
        private DateTime to;

        public Pricelist()
        {

        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
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