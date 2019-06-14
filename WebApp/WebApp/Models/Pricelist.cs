using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Pricelist
    {
        private int id;
        private DateTime from;
        private DateTime to;
        private DateTime lastUpdate;

        public Pricelist()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public DateTime LastUpdate
        {
            get
            {
                return lastUpdate;
            }
            set
            {
                lastUpdate = value;
            }
        }
    }
}