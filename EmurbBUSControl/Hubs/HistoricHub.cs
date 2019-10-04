using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ArduinoCommunication.Models;
using EmurbBUSControl.Models;

namespace EmurbBUSControl.Hubs
{
    public class HistoricHub : Hub
    {
        public async Task SendMessage(int status)
        {
            SerialCommunication.CommunicationStatusType serialCommuncation = (SerialCommunication.CommunicationStatusType) status;
            SerialCommunication.SendStatus(serialCommuncation);
            await Clients.All.SendAsync("ReceiveMessage", serialCommuncation);
        }

        public async Task VehicleArrived(string licensePlate)
        {
            await Clients.All.SendAsync("VehicleArrived", licensePlate);
        }
        
    }
}
