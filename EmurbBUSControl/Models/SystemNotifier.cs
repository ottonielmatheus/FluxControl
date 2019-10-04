using EmurbBUSControl.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models
{
    public static class SystemNotifier
    {
        private static IHubContext<HistoricHub> _hub;
        
        public static void Init(IHubContext<HistoricHub> hub)
        {
            if (_hub == null)
                _hub = hub;
        }

        public static Task SendNotificationAsync(CommunicationStatus status)
        {
            return _hub.Clients.All.SendAsync("VehicleArrived", status);
        }
    }
}
