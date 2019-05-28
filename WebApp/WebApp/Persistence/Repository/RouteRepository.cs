using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class RouteRepository : Repository<Route, int>, IRouteRepository
    {
        public RouteRepository(DbContext context) : base(context)
        {
        }
    }
}