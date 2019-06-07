using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Hubs;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class NotifyController : ApiController
    { 
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;
        private NotificationHub hub;

        public NotifyController(IUnitOfWork uw, NotificationHub hub)
        {
            this.hub = hub;
            unitOfWork = uw;
        }

        // GET: api/Notify
        public IHttpActionResult Post()
        {
            hub.Notify("1A");
            return Ok("Hello");
        }
    }
}
