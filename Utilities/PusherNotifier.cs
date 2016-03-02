﻿using PusherServer;
using Utilities.Configuration.Azure;
using Utilities.Interfaces;

namespace Utilities
{
	public class PusherNotifier : IPusherNotifier
    {
        private Pusher _pusher { get; set; }

        public PusherNotifier()
        {
            var pusherAppId = CloudConfigurationManager.AppSettings.GetSetting("pusherAppId");
            var pusherAppKey = CloudConfigurationManager.AppSettings.GetSetting("pusherAppKey");
            var pusherAppSercret = CloudConfigurationManager.AppSettings.GetSetting("pusherAppSecret");

            if (!string.IsNullOrEmpty(pusherAppId) && !string.IsNullOrEmpty(pusherAppKey) &&
                !string.IsNullOrEmpty(pusherAppSercret))
            {
                _pusher = new Pusher(pusherAppId, pusherAppKey, pusherAppSercret,
                    new PusherOptions
                    {
                        Encrypted = true
                    }
                );
            }
        }

        public void Notify(string channelName, string eventName, object message)
        {
            _pusher?.Trigger(channelName, eventName, message);
        }
    }
}
