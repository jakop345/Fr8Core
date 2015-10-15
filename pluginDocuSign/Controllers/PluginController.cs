using System;
using System.Web.Http;
using Data.Interfaces.DataTransferObjects;
using AutoMapper;
using Data.Entities;
using Newtonsoft.Json;
using System.Reflection;
using PluginBase.BaseClasses;
using System.Collections.Generic;
using Core.Services;
using Data.States;

namespace pluginDocuSign.Controllers
{    
    [RoutePrefix("plugins")]
    public class PluginController : ApiController
    {
        [HttpGet]
        [Route("discover")]
        public IHttpActionResult Get()
        {
            var plugin = new PluginDO()
            {
                Name = "pluginDocuSign",
                PluginStatus = PluginStatus.Active,
                Endpoint = "localhost:53234",
                RequiresAuthentication = true,
                Version = "1"
            };

            var waitForDocusignEventActionTemplate = new ActivityTemplateDO()
            {
                Version = "1",
                Name = "Monitor_DocuSign",
                Category = ActivityCategory.Monitors,
                Plugin = plugin
            };

			var sendDocuSignEnvelopeActionTemplate = new ActivityTemplateDO()
			{
				Version = "1",
				Name = "Send_DocuSign_Envelope",
                Category = ActivityCategory.Forwarders,
				Plugin = plugin
			};

            var extractDataFromEnvelopeActionTemplate = new ActivityTemplateDO()
            {
                Version = "1",
                Name = "Extract_From_DocuSign_Envelope",
                Category = ActivityCategory.Receivers,
                Plugin = plugin
            };

            var recordDocuSignEvents = new ActivityTemplateDO
            {
                Name = "Record_DocuSign_Events",
                Version = "1",
                Category = ActivityCategory.Forwarders,
                Plugin = plugin
            };

            var actionList = new List<ActivityTemplateDO>()
            {
                waitForDocusignEventActionTemplate,
                extractDataFromEnvelopeActionTemplate,
                sendDocuSignEnvelopeActionTemplate,
                recordDocuSignEvents
            };

            return Ok(actionList);
        }
    }
}