﻿namespace Data.Interfaces.DataTransferObjects
{
    public class ActionPayloadDTO : ActionDTOBase
    {
        public string UserLabel { get; set; }

        public int? ActionListId { get; set; }

        public string ConfigurationStore { get; set; }

        public string ParentPluginRegistration { get; set; }

        public PayloadMappingsDTO PayloadMappings { get; set; }

        public string EnvelopeId { get; set; }
    }
}
