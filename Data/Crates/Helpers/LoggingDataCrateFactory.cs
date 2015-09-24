﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StructureMap;
using Data.Interfaces.DataTransferObjects;
using Utilities.Serializers.Json;

namespace Data.Crates.Helpers
{
    public class LoggingDataCrateFactory
    {
        public CrateDTO Create(LoggingData loggingData)
        {
            // var serializer = new JsonSerializer();
            // var contents = serializer.Serialize(loggingData);

            var contents = JsonConvert.SerializeObject(loggingData);

            return new CrateDTO()
            {
                Id = Guid.NewGuid().ToString(),
                Label = "Dockyard Plugin Event or Incident Report",
                Contents = contents,
                ManifestType = "Dockyard Plugin Event or Incident Report",
                ManifestId = 3
            };
        }
    }
}
