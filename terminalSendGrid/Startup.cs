﻿using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Owin;
using TerminalBase.BaseClasses;
using System.Web.Http.Dispatcher;
using TerminalBase.Services;
using terminalSendGrid.Actions;

[assembly: OwinStartup("SendGridStartup", typeof(terminalSendGrid.Startup))]
namespace terminalSendGrid
{
    public class Startup : BaseConfiguration
    {
        public void Configuration(IAppBuilder app)
        {
            Configuration(app, false);
        }

        public void Configuration(IAppBuilder app, bool selfHost)
        {
            ConfigureProject(selfHost, TerminalSendGridStructureMapBootstrapper.LiveConfiguration);
            RoutesConfig.Register(_configuration);
            ConfigureFormatters();

            app.UseWebApi(_configuration);

            if (!selfHost)
            {
                StartHosting("terminalSendGrid");
            }
        }

        public override ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
        {
            return new Type[] {
                    typeof(Controllers.ActivityController),
                    typeof(Controllers.TerminalController)
                };
        }
        protected override void RegisterActivities()
        {
            ActivityStore.RegisterActivity<SendEmailViaSendGrid_v1>(SendEmailViaSendGrid_v1.ActivityTemplateDTO);
        }
    }
}