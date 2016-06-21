﻿using Newtonsoft.Json;

namespace terminalBasecamp.Data
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Product { get; set; }
        [JsonProperty("href")]
        public string ApiUrl { get; set; }
    }
}