using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Data")]
    public class DataController : ApiController
    {
        [Route("getPricelist")]
        public IHttpActionResult GetPricelist()
        {

            return Ok("Tu je.");
        }
    }
}
