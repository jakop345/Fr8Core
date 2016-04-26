﻿﻿using Data.Entities;
using Data.Interfaces.DataTransferObjects;
using Data.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesTesting.Fixtures
{
    partial class FixtureData
    {
        public static ActivityTemplateDTO TestActivityTemplateDTO1()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Write_To_Sql_Server",
                Version = "1"
            };
        }

        public static ActivityTemplateDTO TestActivityTemplateSalesforce()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Create_Lead",
                Version = "1"
            };
        }

        public static ActivityTemplateDTO TestActivityTemplateSendGrid()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "SendEmailViaSendGrid",
                Version = "1"
            };
        }

        public static ActivityTemplateDO TwilioActivityTemplateDTO()
        {
            return new ActivityTemplateDO
            {
                Id = Guid.NewGuid(),
                Name = "Send_Via_Twilio",
                Version = "1"
            };
        }

        public static ActivityTemplateDTO ActivityTemplateDTOSelectFr8Object()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Select Fr8 Object",
                Version = "1"
            };
        }

        public static ActivityTemplateDTO QueryDocuSignActivityTemplate()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Query_DocuSign",
                Version = "1",
            };
        }

        public static ActivityTemplateDTO SaveToGoogleSheetActivityTemplate()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Save_To_Google_Sheet",
                Version = "1"
            };
        }

        public static ActivityTemplateDTO GetGoogleSheetDataActivityTemplate()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Get_Google_Sheet_Data",
                Version = "1"
            };
        }

        public static ActivityTemplateDTO BuildMessageActivityTemplate()
        {
            return new ActivityTemplateDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Build_Message",
                Version = "1"
            };
        }

        public static ActivityTemplateDTO GetFileListActivityTemplate()
        {
            return new ActivityTemplateDTO
            {
                Id = Guid.NewGuid(),
                Name = "Get_File_List",
                Version = "1"
            };
        }
    }
}