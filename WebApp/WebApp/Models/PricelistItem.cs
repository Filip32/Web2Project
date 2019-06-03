using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PricelistItem
    {
        private int pricelist_id;
        private int item_id;
        private int coefficients_id;
        private decimal price;
        private int id;

        public PricelistItem()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Pricelist_id
        {
            get
            {
                return pricelist_id;
            }
            set
            {
                pricelist_id = value;
            }
        }

        public int Item_id
        {
            get
            {
                return item_id;
            }
            set
            {
                item_id = value;
            }
        }

        public int Coefficients_id
        {
            get
            {
                return coefficients_id;
            }
            set
            {
                coefficients_id = value;
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