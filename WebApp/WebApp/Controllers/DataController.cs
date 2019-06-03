using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Data")]
    public class DataController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        IUnitOfWork unitOfWork;

        public DataController(IUnitOfWork uw)
        {
            unitOfWork = uw;
        }

        [Route("getPricelist")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetPricelist()
        {
            if (User.Identity.IsAuthenticated)
            {
               // User.IsInRole()
            }
            List<Address> a = unitOfWork.AddressRepository.GetAll().ToList();
            return Ok(a);
        }
    }
}
