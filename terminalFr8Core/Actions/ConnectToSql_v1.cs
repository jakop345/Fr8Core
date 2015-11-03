﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Data.Entities;
using Data.Interfaces;
using Data.Interfaces.DataTransferObjects;
using Data.Interfaces.Manifests;
using TerminalBase.BaseClasses;
using TerminalBase.Infrastructure;
using TerminalSqlUtilities;

namespace terminalFr8Core.Actions
{
    public class ConnectToSql_v1 : BasePluginAction
    {
        #region Configuration.

        private const string DefaultDbProvider = "System.Data.SqlClient";


        public override ConfigurationRequestType ConfigurationEvaluator(
            ActionDTO curActionDTO)
        {
            if (curActionDTO.CrateStorage == null
                || curActionDTO.CrateStorage.CrateDTO == null
                || curActionDTO.CrateStorage.CrateDTO.Count == 0)
            {
                return ConfigurationRequestType.Initial;
            }

            return ConfigurationRequestType.Followup;
        }

        protected override Task<ActionDTO> InitialConfigurationResponse(
            ActionDTO curActionDTO)
        {
            if (curActionDTO.CrateStorage == null)
            {
                curActionDTO.CrateStorage = new CrateStorageDTO();
            }

            var crateControls = CreateControlsCrate();
            curActionDTO.CrateStorage.CrateDTO.Add(crateControls);

            return Task.FromResult<ActionDTO>(curActionDTO);
        }

        private CrateDTO CreateControlsCrate()
        {
            var control = new TextBoxControlDefinitionDTO()
            {
                Label = "SQL Connection String",
                Name = "ConnectionString",
                Required = true,
                Events = new List<ControlEvent>()
                {
                    new ControlEvent("onChange", "requestConfig")
                }
            };

            return PackControlsCrate(control);
        }

        protected override Task<ActionDTO> FollowupConfigurationResponse(ActionDTO curActionDTO)
        {
            RemoveLabelControl(curActionDTO, "ErrorLabel");

            Crate.RemoveCrateByLabel(
                curActionDTO.CrateStorage.CrateDTO,
                "Sql Table Definitions"
            );

            var connectionString = ExtractConnectionString(curActionDTO);
            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    var tableDefinitions = RetrieveTableDefinitions(connectionString);
                    var tableDefinitionCrate = Crate
                        .CreateDesignTimeFieldsCrate(
                            "Sql Table Definitions",
                            tableDefinitions.ToArray()
                        );

                    curActionDTO.CrateStorage.CrateDTO.Add(tableDefinitionCrate);
                }
                catch
                {
                    AddLabelControl(
                        curActionDTO,
                        "ErrorLabel",
                        "Unexpected error",
                        "Error occured while trying to fetch columns from database specified."
                    );
                }
            }

            return base.FollowupConfigurationResponse(curActionDTO);
        }

        private string ExtractConnectionString(ActionDTO curActionDTO)
        {
            var configControls = Crate.GetConfigurationControls(Mapper.Map<ActionDO>(curActionDTO));
            var connectionStringControl = configControls.FindByName("ConnectionString");

            return connectionStringControl.Value;
        }

        private List<FieldDTO> RetrieveTableDefinitions(string connectionString)
        {
            var provider = DbProvider.GetDbProvider(DefaultDbProvider);

            using (var conn = provider.CreateConnection(connectionString))
            {
                conn.Open();

                using (var tx = conn.BeginTransaction())
                {
                    var fieldsList = new List<FieldDTO>();

                    var columns = provider.ListAllColumns(tx);
                    foreach (var column in columns)
                    {
                        var fullColumnName = GetColumnName(column);

                        fieldsList.Add(new FieldDTO()
                        {
                            Key = fullColumnName,
                            Value = fullColumnName
                        });
                    }

                    return fieldsList;
                }
            }
        }

        private string GetColumnName(ColumnInfo columnInfo)
        {
            if (string.IsNullOrEmpty(columnInfo.TableInfo.SchemaName))
            {
                return string.Format(
                    "{0}.{1}",
                    columnInfo.TableInfo.TableName,
                    columnInfo.ColumnName
                );
            }
            else
            {
                return string.Format(
                    "{0}.{1}.{2}",
                    columnInfo.TableInfo.SchemaName,
                    columnInfo.TableInfo.TableName,
                    columnInfo.ColumnName
                );
            }
        }

        #endregion Configuration.

        #region Execution.

        public Task<PayloadDTO> Run(ActionDTO curActionDTO)
        {
            return Task.FromResult<PayloadDTO>(null);
        }

        #endregion Execution.
    }
}