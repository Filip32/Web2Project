using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using WebApp.Controllers;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Hubs
{
    [HubName("notifications")]
    public class NotificationHub: Hub
    {
        public static ConcurrentDictionary<string, Timer> Timers = new ConcurrentDictionary<string, Timer>();
        public static readonly object balanceLock = new object();
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private IUnitOfWork unitOfWork = AccountController.unitOfWork;
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        private static Dictionary<string,string> groupNames = new Dictionary<string, string>();
        private static Dictionary<string, List<int>> RouteBus = new Dictionary<string, List<int>>();

        public NotificationHub()
        {
        }

        public void StopTimeServerUpdates()
        {
            if (groupNames.ContainsKey(Context.ConnectionId))
            {
                Timer timer = new Timer();
                Timers.TryRemove(Context.ConnectionId, out timer);
                timer.Close();
                Groups.Remove(Context.ConnectionId, groupNames[Context.ConnectionId]);
                RouteBus.Remove(groupNames[Context.ConnectionId]);
                groupNames.Remove(Context.ConnectionId);
            }
        }

        public void BroadcastData(string nameOfGroup)
        {
            Groups.Add(Context.ConnectionId, nameOfGroup);
            groupNames[Context.ConnectionId] = nameOfGroup;
            List<int> list = new List<int>();
            Random r = new Random();
            int count = r.Next(1, 4);
            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    int k = r.Next(1, 6);
                    if (!list.Contains(k))
                    {
                        list.Add(k);
                        break;
                    }
                }
            }

            RouteBus[nameOfGroup] = list;
        }

        public void TimeServerUpdates()
        {
                Random random = new Random();
                Timer timer = new Timer();
                timer.Interval = random.Next(5000, 10000);
                timer.Start();
                timer.Elapsed += OnTimedEvent;
                Timers[Context.ConnectionId] = timer;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            GetTime();
        }

        public void GetTime()
        {
            try
            {
                foreach (string val in groupNames.Values)
                {
                    lock (balanceLock)
                    {
                        Route route = unitOfWork.RouteRepository.GetAll().Where(x => x.RouteNumber == val).FirstOrDefault();
                        List<RouteStation> routeStation = unitOfWork.RouteStationRepositpry.GetAll().Where(x => x.Route_id == route.Id).ToList();
                        Dictionary<int, Station> stations = new Dictionary<int, Station>();

                        foreach (RouteStation s in routeStation)
                        {
                            Station station = unitOfWork.StationRepository.Get(s.Station_id);

                            if (station.IsStation)
                            {
                                stations.Add(s.Station_num, station);
                            }
                        }

                        List<Station> stationsToSend = new List<Station>();
                        List<int> lis = RouteBus[val];

                        foreach (int k in lis)
                        {
                            stationsToSend.Add(stations[k]);
                        }

                        lis = lis.Select(x => x + 1).ToList();

                        Clients.Group(val).setRealTime(stationsToSend);


                        for(int i = 0; i < lis.Count; i++)
                        {
                            if (lis[i] > stations.Count)
                            {
                                lis[i] = 1;
                            }
                        }

                        RouteBus[val] = lis;

                    }
                }
        }catch(Exception e)
        {
        }
}

        public override Task OnConnected()
        {
            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            if (groupNames.ContainsKey(Context.ConnectionId))
            {
                Timer timer = new Timer();
                Timers.TryRemove(Context.ConnectionId, out timer);
                timer.Close();
                Groups.Remove(Context.ConnectionId, groupNames[Context.ConnectionId]);
                RouteBus.Remove(groupNames[Context.ConnectionId]);
                groupNames.Remove(Context.ConnectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}