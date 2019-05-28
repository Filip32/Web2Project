using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        [Dependency]
        public IAddressRepository AddressRepository { get; set; }
        [Dependency]
        public ILocationRepository LocationRepository { get; set; }
        [Dependency]
        public IPassengerRepository PassengerRepository { get; set; }
        [Dependency]
        public ICoefficientRepository CoefficientRepository { get; set; }
        [Dependency]
        public IPersonRepository PersonRepository { get; set; }
        [Dependency]
        public IPricelistItemRepository PricelistItemRepository { get; set; }
        [Dependency]
        public IPricelistRepository PricelistRepository { get; set; }
        [Dependency]
        public IRouteRepository RouteRepository { get; set; }
        [Dependency]
        public IStationRepository StationRepository { get; set; }
        [Dependency]
        public ITicketRepository TicketRepository { get; set; }
        [Dependency]
        public ITimetableRepository TimetableRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}