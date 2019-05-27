using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PricelistItem
    {
        private Pricelist pricelist;
        private Item item;
        private Coefficients coefficients;
        private decimal price;
        private int id;

        public PricelistItem()
        {

        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Pricelist Pricelist
        {
            get
            {
                return pricelist;
            }
            set
            {
                pricelist = value;
            }
        }

        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        public Coefficients Coefficients
        {
            get
            {
                return coefficients;
            }
            set
            {
                coefficients = value;
            }
        }

        public decimal Price
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
    }
}