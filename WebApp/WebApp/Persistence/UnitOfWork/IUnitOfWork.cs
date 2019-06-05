using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //Sve repo ovako
        IAddressRepository AddressRepository { get; set; }
        ILocationRepository LocationRepository { get; set; }
        IPassengerRepository PassengerRepository { get; set; }
        ICoefficientRepository CoefficientRepository { get; set; }
        IItemRepository ItemRepository { get; set; }
        IPricelistItemRepository PricelistItemRepository { get; set; }
        IPricelistRepository PricelistRepository { get; set; }
        IRouteRepository RouteRepository { get; set; }
        IStationRepository StationRepository { get; set; }
        ITicketRepository TicketRepository { get; set; }
        ITimetableRepository TimetableRepository { get; set; }
        IRouteStationRepositpry RouteStationRepositpry { get; set; }
        int Complete();
    }
}
