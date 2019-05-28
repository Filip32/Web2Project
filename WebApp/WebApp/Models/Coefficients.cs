using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Coefficients
    {
        private double coefficientPensioner;
        private double coefficientStudent;
        private int id;

        public Coefficients()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public double CoefficientPensioner
        {
            get
            {
                return coefficientPensioner;
            }
            set
            {
                coefficientPensioner = value;
            }
        }

        public double CoefficientStudent
        {
            get
            {
                return coefficientStudent;
            }
            set
            {
                coefficientStudent = value;
            }
        }
    }
}