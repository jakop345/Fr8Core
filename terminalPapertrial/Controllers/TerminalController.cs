using System.Collections.Generic;
using System.Web.Http;
using Data.Entities;
using Data.States;
using Utilities.Configuration.Azure;
using System.Web.Http.Description;
using Data.Interfaces.Manifests;

namespace terminalPapertrial.Controllers
{
    [RoutePrefix("terminals")]
    public class TerminalController : ApiController
    {
        [HttpGet]
        [Route("discover")]
        [ResponseType(typeof (StandardFr8TerminalCM))]
        public IHttpActionResult Get()
        {
            var terminal = new TerminalDO()
            {
                Name = "terminalPapertrial",
                TerminalStatus = TerminalStatus.Active,
                Endpoint = CloudConfigurationManager.GetSetting("TerminalEndpoint"),
                Version = "1"
            };

            var writeToLogActionTemplate = new ActivityTemplateDO()
            {
                Version = "1",
                Name = "Write_To_Log",
                Label = "Write To Log",
                Category = ActivityCategory.Processors,
                Terminal = terminal,
                AuthenticationType = AuthenticationType.None,
                MinPaneWidth = 330
            };

            var curStandardFr8TerminalCM = new StandardFr8TerminalCM()
            {
                Definition = terminal,
                Actions = new List<ActivityTemplateDO> {writeToLogActionTemplate}
            };

            return Json(curStandardFr8TerminalCM);
        }
    }
}