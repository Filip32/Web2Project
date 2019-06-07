﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class StationHelp
    {
        private double x;
        private double y;
        private string name;
        private Address address;
        private bool isStation;

        public StationHelp()
        {
            Address = new Address();
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public bool IsStation
        {
            get
            {
                return isStation;
            }
            set
            {
                isStation = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
    }
}