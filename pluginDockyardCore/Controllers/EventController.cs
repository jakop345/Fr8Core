﻿using System.Web.Http;
using Core.Interfaces;
using StructureMap;

namespace pluginDockyardCore.Controllers
{
    public class EventController : ApiController
    {
        private IEvent _event;

        public EventController()
        {
            _event = ObjectFactory.GetInstance<IEvent>();
        }

        [HttpPost]
        [Route("events")]
        public async void ProcessIncomingNotification()
        {
            _event.Process(await Request.Content.ReadAsStringAsync());
        }
    }
}
