﻿using System.Web.Http;
using Fr8.TerminalBase.BaseClasses;

namespace terminalBasecamp
{
    public static class RoutesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            BaseTerminalWebApiConfig.Register("Basecamp", config);
        }
    }
}