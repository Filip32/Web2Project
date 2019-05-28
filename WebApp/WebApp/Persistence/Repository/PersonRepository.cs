using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class PersonRepository : Repository<Person, int>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}