﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fr8.Infrastructure.Data.Control;
using Fr8.Infrastructure.Data.Crates;
using Fr8.Infrastructure.Data.DataTransferObjects;
using Fr8.Infrastructure.Data.Manifests;
using Newtonsoft.Json;
using NUnit.Framework;
using Fr8.Testing.Integration;
using terminalTests.Fixtures;

namespace terminalFr8CoreTests.Integration
{
    [Explicit]
    public class GetDataFromFr8Warehouse_v1_Tests : BaseTerminalIntegrationTest
    {
        public override string TerminalName
        {
            get { return "terminalFr8Core"; }
        }

        [Test]
        public async Task GetDataFromFr8Warehouse_Initial()
        {
            await ConfigureInitial();
        }

        [Test]
        public async Task GetDataFromFr8Warehouse_FollowUp()
        {
            await ConfigureFollowUp();
        }

        [Test]
        public async Task GetDataFromFr8Warehouse_Run()
        {
            var activityDTO = await ConfigureFollowUp();
            using (var updater = Crate.UpdateStorage(() => activityDTO.CrateStorage))
            {
                var controls = updater.CrateContentsOfType<StandardConfigurationControlsCM>().First();
                var queryBuilder = controls.FindByName<QueryBuilder>("QueryBuilder");
                queryBuilder.Value = JsonConvert.SerializeObject(
                    new List<FilterConditionDTO>()
                    {
                        new FilterConditionDTO()
                        {
                            Field = "Status",
                            Operator = "eq",
                            Value = ""
                        }
                    }
                );
            }

            activityDTO.AuthToken = new AuthorizationTokenDTO() { UserId = Guid.NewGuid().ToString() };

            var runUrl = GetTerminalRunUrl();
            var data = new Fr8DataDTO()
            {
                ActivityDTO = activityDTO                
            };

            AddPayloadCrate(data, new OperationalStateCM());

            var payload = await HttpPostAsync<Fr8DataDTO, PayloadDTO>(runUrl, data);
            Assert.IsNotNull(payload, "PayloadDTO is null");
            Assert.IsNotNull(payload.CrateStorage, "PayloadDTO.CrateStorage is null");

            var crateStorage = Crate.FromDto(payload.CrateStorage);
            Assert.AreEqual(
                1,
                crateStorage.CratesOfType<StandardTableDataCM>()
                    .Count(x => x.Label == "Table Generated by Get Data From Fr8 Warehouse"),
                "StandardTableDataCM is expected"
            );
        }

        private async Task<ActivityDTO> ConfigureInitial()
        {
            var configureUrl = GetTerminalConfigureUrl();

            var dataDTO = HealthMonitor_FixtureData
                .GetDataFromFr8Warehouse_v1_InitialConfiguration_Fr8DataDTO();

            var activityDTO =
                await HttpPostAsync<Fr8DataDTO, ActivityDTO>(
                    configureUrl,
                    dataDTO
                );

            Assert.IsNotNull(activityDTO, "ActivityDTO is null");
            Assert.IsNotNull(activityDTO.CrateStorage, "ActivityDTO.CrateStorage is null");

            var crateStorage = Crate.FromDto(activityDTO.CrateStorage);
            Assert.AreEqual(2, crateStorage.Count, "Invalid CrateStorage structure");
            Assert.AreEqual(1, crateStorage.CratesOfType<StandardConfigurationControlsCM>().Count(), "Invalid CrateStorage structure");
            Assert.AreEqual(1, crateStorage.CratesOfType<CrateDescriptionCM>().Count(), "Invalid CrateStorage structure");

            var controls = crateStorage.CrateContentsOfType<StandardConfigurationControlsCM>().First();
            Assert.AreEqual(3, controls.Controls.Count, "Invalid ConfigurationControls structure");
            Assert.AreEqual("AvailableObjects", controls.Controls[0].Name, "Invalid ConfigurationControls structure");
            Assert.AreEqual("SelectObjectLabel", controls.Controls[1].Name, "Invalid ConfigurationControls structure");
            Assert.AreEqual(false, controls.Controls[1].IsHidden, "Invalid ConfigurationControls structure");
            Assert.AreEqual("QueryBuilder", controls.Controls[2].Name, "Invalid ConfigurationControls structure");
            Assert.AreEqual(true, controls.Controls[2].IsHidden, "Invalid ConfigurationControls structure");

            var crateDescription = crateStorage.CrateContentsOfType<CrateDescriptionCM>().First();
            Assert.AreEqual(1, crateDescription.CrateDescriptions.Count, "Invalid CrateDescription structure");
            Assert.AreEqual("Table Generated by Get Data From Fr8 Warehouse", crateDescription.CrateDescriptions[0].Label, "Invalid CrateDescription structure");
            Assert.AreEqual("Standard Table Data", crateDescription.CrateDescriptions[0].ManifestType, "Invalid CrateDescription structure");

            return activityDTO;
        }

        private async Task<ActivityDTO> ConfigureFollowUp()
        {
            StandardConfigurationControlsCM controls;

            var activityDTO = await ConfigureInitial();

            using (var updater = Crate.UpdateStorage(() => activityDTO.CrateStorage))
            {
                controls = updater.CrateContentsOfType<StandardConfigurationControlsCM>().First();
                var availableObjects = controls.FindByName<DropDownList>("AvailableObjects");
                availableObjects.SelectByKey("Standard Business Fact");
            }

            var configureUrl = GetTerminalConfigureUrl();

            activityDTO =
                await HttpPostAsync<Fr8DataDTO, ActivityDTO>(
                    configureUrl,
                    new Fr8DataDTO { ActivityDTO = activityDTO }
                );

            Assert.IsNotNull(activityDTO, "ActivityDTO is null");
            Assert.IsNotNull(activityDTO.CrateStorage, "ActivityDTO.CrateStorage is null");

            var crateStorage = Crate.FromDto(activityDTO.CrateStorage);
            Assert.AreEqual(3, crateStorage.Count, "Invalid CrateStorage structure");
            Assert.AreEqual(1, crateStorage.CratesOfType<StandardConfigurationControlsCM>().Count(), "Invalid CrateStorage structure");
            Assert.AreEqual(1, crateStorage.CratesOfType<CrateDescriptionCM>().Count(), "Invalid CrateStorage structure");
            Assert.AreEqual(1, crateStorage.CratesOfType<FieldDescriptionsCM>().Count(), "Invalid CrateStorage structure");

            controls = crateStorage.CrateContentsOfType<StandardConfigurationControlsCM>().First();
            Assert.AreEqual(3, controls.Controls.Count, "Invalid ConfigurationControls structure");
            Assert.AreEqual("AvailableObjects", controls.Controls[0].Name, "Invalid ConfigurationControls structure");
            Assert.AreEqual("SelectObjectLabel", controls.Controls[1].Name, "Invalid ConfigurationControls structure");
            Assert.AreEqual(true, controls.Controls[1].IsHidden, "Invalid ConfigurationControls structure");
            Assert.AreEqual("QueryBuilder", controls.Controls[2].Name, "Invalid ConfigurationControls structure");
            Assert.AreEqual(false, controls.Controls[2].IsHidden, "Invalid ConfigurationControls structure");

            var crateDescription = crateStorage.CrateContentsOfType<CrateDescriptionCM>().First();
            Assert.AreEqual(1, crateDescription.CrateDescriptions.Count, "Invalid CrateDescription structure");
            Assert.AreEqual("Table Generated by Get Data From Fr8 Warehouse", crateDescription.CrateDescriptions[0].Label, "Invalid CrateDescription structure");
            Assert.AreEqual("Standard Table Data", crateDescription.CrateDescriptions[0].ManifestType, "Invalid CrateDescription structure");

            return activityDTO;
        }
    }
}
