using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RouteStation
    {
        private int id;
        private int route_id;
        private int station_id;

        public RouteStation()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return id; } set { id = value; } }
        public int Route_id { get { return route_id; } set { route_id = value; } }
        public int Station_id { get { return station_id; } set { station_id = value; } }
    }
}