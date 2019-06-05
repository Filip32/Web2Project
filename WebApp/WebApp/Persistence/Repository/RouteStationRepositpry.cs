using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RouteStationRepositpry : Repository<RouteStation, int>, IRouteStationRepositpry
    {
        public RouteStationRepositpry(DbContext context) : base(context)
        {
        }
    }
}