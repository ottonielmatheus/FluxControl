using EmurbBUSControl.Hubs;
using EmurbBUSControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Controllers
{
    [Route("{controller}/")]
    public class MonitoramentoController : Controller
    {
        public MonitoramentoController(IHubContext<SerialHub> hub)
        {
            SystemNotifier.Init(hub);
        }

        [Route("Dashboard")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
