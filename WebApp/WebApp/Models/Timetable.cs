using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Timetable
    {
        private int id;
        private Enums.TypeOfDay typeOfDay;

        public Timetable()
        {
        }

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Enums.TypeOfDay TypeOfDay
        {
            get
            {
                return typeOfDay;
            }
            set
            {
                typeOfDay = value;
            }
        }
    }
}