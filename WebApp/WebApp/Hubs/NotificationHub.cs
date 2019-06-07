using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using WebApp.Models;

namespace WebApp.Hubs
{
    [HubName("notifications")]
    public class NotificationHub: Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        private static Timer timer = new Timer();

        public NotificationHub()
        {
        }

        public void TimeServerUpdates()
        {
            timer.Interval = 3000;
            timer.Start();
            timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            GetTime();
        }

        public void StopTimeServerUpdates()
        {
            timer.Stop();
        }

        public void Notify(string groupName)
        {
            hubContext.Clients.Group(groupName).userBusNotify();
        }

        public void GetTime()
        {
            //Svim klijentima se salje setRealTime poruka
            //kako znati na koju stanicu ocekuje
            //redosled stanica
            //kordinate(X,Y)
            Clients.Group("Lisener").setRealTime(DateTime.Now.ToString("h:mm:ss tt"));
           // Clients.All.setRealTime(DateTime.Now.ToString("h:mm:ss tt"));
        }

        public override Task OnConnected()
        {
           
            Groups.Add(Context.ConnectionId, "Lisener");
            
            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            Groups.Remove(Context.ConnectionId, "Lisener");

            return base.OnDisconnected(stopCalled);
        }
    }
}