﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Data.Control;
using Data.Interfaces.DataTransferObjects;
using Data.Interfaces.Manifests;
using Data.States;
using HealthMonitor.Utility;
using Hub.Managers;
using NUnit.Framework;

namespace terminalFr8CoreTests.Integration
{
	/// <summary>
	/// Mark test case class with [Explicit] attiribute.
	/// It prevents test case from running when CI is building the solution,
	/// but allows to trigger that class from HealthMonitor.
	/// </summary>
	[Explicit]
	public class Select_Fr8_Object_v1Tests : BaseTerminalIntegrationTest
    {
		public override string TerminalName
		{
			get { return "terminalFr8Core"; }
		}

		[Test]
		public void Check_Initial_Configuration_Crate_Structure()
		{
			var configureUrl = GetTerminalConfigureUrl();

			var requestActionDTO = CreateRequestActionFixture();

			var responseActionDTO = HttpPostAsync<ActivityDTO, ActivityDTO>(configureUrl, requestActionDTO).Result;

			Assert.NotNull(responseActionDTO);
			Assert.NotNull(responseActionDTO.CrateStorage);
			Assert.NotNull(responseActionDTO.CrateStorage.Crates);

			var crateStorage = Crate.FromDto(responseActionDTO.CrateStorage);

			Assert.AreEqual(2, crateStorage.Count);

			Assert.AreEqual(1, crateStorage.CratesOfType<StandardConfigurationControlsCM>().Count(x => x.Label == "Configuration_Controls"));
			Assert.AreEqual(1, crateStorage.CratesOfType<StandardDesignTimeFieldsCM>().Count(x => x.Label == "Select Fr8 Object"));

			var configCrate = crateStorage
				.CrateContentsOfType<StandardConfigurationControlsCM>(x => x.Label == "Configuration_Controls")
				.SingleOrDefault();

			ValidateConfigurationCrateStructure(configCrate);

			var designTimeCrate = crateStorage
				.CrateContentsOfType<StandardDesignTimeFieldsCM>(x => x.Label == "Select Fr8 Object")
				.SingleOrDefault();

			ValidateFr8ObjectCrateStructure(designTimeCrate);
		}

		[Test]
		public void Check_FollowUp_Configuration_Crate_Structure_When_Routes_Option_Is_Selected()
		{
			var configureUrl = GetTerminalConfigureUrl();

			var requestActionDTO = CreateRequestActionFixture();

			var responseActionDTO = HttpPostAsync<ActivityDTO, ActivityDTO>(configureUrl, requestActionDTO).Result;

			SetRoutesOptionSelected(responseActionDTO);

			responseActionDTO = HttpPostAsync<ActivityDTO, ActivityDTO>(configureUrl, responseActionDTO).Result;

			Assert.NotNull(responseActionDTO);
			Assert.NotNull(responseActionDTO.CrateStorage);
			Assert.NotNull(responseActionDTO.CrateStorage.Crates);

			var crateStorage = Crate.FromDto(responseActionDTO.CrateStorage);

			Assert.AreEqual(3, crateStorage.Count);

			Assert.AreEqual(1, crateStorage.CratesOfType<StandardConfigurationControlsCM>().Count(x => x.Label == "Configuration_Controls"));
			Assert.AreEqual(1, crateStorage.CratesOfType<StandardDesignTimeFieldsCM>().Count(x => x.Label == "Select Fr8 Object"));
			Assert.AreEqual(1, crateStorage.CratesOfType<StandardDesignTimeFieldsCM>().Count(x => x.Label == "StandardFr8RoutesCM"));

			var configCrate = crateStorage
				.CrateContentsOfType<StandardConfigurationControlsCM>(x => x.Label == "Configuration_Controls")
				.SingleOrDefault();

			ValidateConfigurationCrateStructure(configCrate);

			var configurationControl = (DropDownList)configCrate.Controls.FirstOrDefault();

			Assert.AreEqual("19", configurationControl.Value);
			Assert.AreEqual("Routes", configurationControl.selectedKey);

			var selectFr8ObjectDesignTimeCrate = crateStorage
				.CrateContentsOfType<StandardDesignTimeFieldsCM>(x => x.Label == "Select Fr8 Object")
				.SingleOrDefault();

			ValidateFr8ObjectCrateStructure(selectFr8ObjectDesignTimeCrate);

			var fr8RoutesDesignTimeCrate = crateStorage
				.CrateContentsOfType<StandardDesignTimeFieldsCM>(x => x.Label == "StandardFr8RoutesCM")
				.SingleOrDefault();

			Assert.AreEqual(8, fr8RoutesDesignTimeCrate.Fields.Count);

			var fr8RoutesCrateFields = fr8RoutesDesignTimeCrate.Fields;

			Assert.AreEqual("CreateDate", fr8RoutesCrateFields[0].Key);
			Assert.AreEqual("DateTime", fr8RoutesCrateFields[0].Value);

			Assert.AreEqual("LastUpdated", fr8RoutesCrateFields[1].Key);
			Assert.AreEqual("DateTime", fr8RoutesCrateFields[1].Value);

			Assert.AreEqual("Description", fr8RoutesCrateFields[2].Key);
			Assert.AreEqual("String", fr8RoutesCrateFields[2].Value);

			Assert.AreEqual("Name", fr8RoutesCrateFields[3].Key);
			Assert.AreEqual("String", fr8RoutesCrateFields[3].Value);

			Assert.AreEqual("Ordering", fr8RoutesCrateFields[4].Key);
			Assert.AreEqual("Int32", fr8RoutesCrateFields[4].Value);

			Assert.AreEqual("RouteState", fr8RoutesCrateFields[5].Key);
			Assert.AreEqual("String", fr8RoutesCrateFields[5].Value);

			Assert.AreEqual("SubRoutes", fr8RoutesCrateFields[6].Key);
			Assert.AreEqual("System.Collections.Generic.List`1[[Data.Interfaces.DataTransferObjects.SubrouteDTO, Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]", fr8RoutesCrateFields[6].Value);

			Assert.AreEqual("ManifestType", fr8RoutesCrateFields[7].Key);
			Assert.AreEqual("CrateManifestType", fr8RoutesCrateFields[7].Value);
		}

		[Test]
		public void Check_FollowUp_Configuration_Crate_Structure_When_Containers_Option_Is_Selected()
		{
			var configureUrl = GetTerminalConfigureUrl();

			var requestActionDTO = CreateRequestActionFixture();

			var responseActionDTO = HttpPostAsync<ActivityDTO, ActivityDTO>(configureUrl, requestActionDTO).Result;

			SetContainersOptionSelected(responseActionDTO);

			responseActionDTO = HttpPostAsync<ActivityDTO, ActivityDTO>(configureUrl, responseActionDTO).Result;

			Assert.NotNull(responseActionDTO);
			Assert.NotNull(responseActionDTO.CrateStorage);
			Assert.NotNull(responseActionDTO.CrateStorage.Crates);

			var crateStorage = Crate.FromDto(responseActionDTO.CrateStorage);

			Assert.AreEqual(3, crateStorage.Count);

			Assert.AreEqual(1, crateStorage.CratesOfType<StandardConfigurationControlsCM>().Count(x => x.Label == "Configuration_Controls"));
			Assert.AreEqual(1, crateStorage.CratesOfType<StandardDesignTimeFieldsCM>().Count(x => x.Label == "Select Fr8 Object"));
			Assert.AreEqual(1, crateStorage.CratesOfType<StandardDesignTimeFieldsCM>().Count(x => x.Label == "StandardFr8ContainersCM"));

			var configCrate = crateStorage
				.CrateContentsOfType<StandardConfigurationControlsCM>(x => x.Label == "Configuration_Controls")
				.SingleOrDefault();

			ValidateConfigurationCrateStructure(configCrate);

			var configurationControl = (DropDownList)configCrate.Controls.FirstOrDefault();

			Assert.AreEqual("21", configurationControl.Value);
			Assert.AreEqual("Containers", configurationControl.selectedKey);

			var selectFr8ObjectDesignTimeCrate = crateStorage
				.CrateContentsOfType<StandardDesignTimeFieldsCM>(x => x.Label == "Select Fr8 Object")
				.SingleOrDefault();

			ValidateFr8ObjectCrateStructure(selectFr8ObjectDesignTimeCrate);

			var fr8ContainersDesignTimeCrate = crateStorage
				.CrateContentsOfType<StandardDesignTimeFieldsCM>(x => x.Label == "StandardFr8ContainersCM")
				.SingleOrDefault();

			Assert.AreEqual(5, fr8ContainersDesignTimeCrate.Fields.Count);

			var fr8ContainersCrateFields = fr8ContainersDesignTimeCrate.Fields;

			Assert.AreEqual("Name", fr8ContainersCrateFields[0].Key);
			Assert.AreEqual("String", fr8ContainersCrateFields[0].Value);

			Assert.AreEqual("Description", fr8ContainersCrateFields[1].Key);
			Assert.AreEqual("String", fr8ContainersCrateFields[1].Value);

			Assert.AreEqual("CreatedDate", fr8ContainersCrateFields[2].Key);
			Assert.AreEqual("DateTime", fr8ContainersCrateFields[2].Value);

			Assert.AreEqual("LastUpdated", fr8ContainersCrateFields[3].Key);
			Assert.AreEqual("DateTime", fr8ContainersCrateFields[3].Value);

			Assert.AreEqual("ManifestType", fr8ContainersCrateFields[4].Key);
			Assert.AreEqual("CrateManifestType", fr8ContainersCrateFields[4].Value);
		}

		[Test]
		public void Run_With_Route_Payload()
		{
			var configureUrl = GetTerminalConfigureUrl();

			var requestActionDTO = CreateRequestActionFixture();

            var dataDTO = new Fr8DataDTO { ActivityDTO = requestActionDTO };

            var responseActionDTO = HttpPostAsync<Fr8DataDTO, ActivityDTO>(configureUrl, dataDTO).Result;

			SetRoutesOptionSelected(responseActionDTO);

			var runUrl = GetTerminalRunUrl();

		    dataDTO.ActivityDTO = responseActionDTO;

            AddPayloadCrate(dataDTO, new StandardFr8RoutesCM
			{
				CreateDate = DateTime.UtcNow,
				LastUpdated = DateTime.UtcNow,
				Description = "Some description",
				Name = "Some name",
				Ordering = 1,
				RouteState = "Some state",
				SubRoutes = new List<SubrouteDTO>()
			});

			var runResponse = HttpPostAsync<Fr8DataDTO, PayloadDTO>(runUrl, dataDTO).Result;

			Assert.NotNull(runResponse);
		}

		[Test]
		public void Run_With_Container_Payload()
		{
			var configureUrl = GetTerminalConfigureUrl();

			var requestActionDTO = CreateRequestActionFixture();

            var dataDTO = new Fr8DataDTO { ActivityDTO = requestActionDTO };

            var responseActionDTO = HttpPostAsync<Fr8DataDTO, ActivityDTO>(configureUrl, dataDTO).Result;

			SetContainersOptionSelected(responseActionDTO);

			var runUrl = GetTerminalRunUrl();

            dataDTO.ActivityDTO = responseActionDTO;

            AddPayloadCrate(dataDTO, new StandardFr8ContainersCM()
			{
				Name = "Some name",
				Description = "Some description",
				LastUpdated = DateTime.UtcNow,
				CreatedDate = DateTime.UtcNow
			});

			var runResponse = HttpPostAsync<Fr8DataDTO, PayloadDTO>(runUrl, dataDTO).Result;

			Assert.NotNull(runResponse);
		}

		private ActivityTemplateDTO CreateActivityTemplateFixture()
		{
			var activityTemplate = new ActivityTemplateDTO
			{
				Id = 1,
				Name = "Select_Fr8_Object_TEST",
				Version = "1"
			};

			return activityTemplate;
		}

		private ActivityDTO CreateRequestActionFixture()
		{
			var activityTemplate = CreateActivityTemplateFixture();

			var requestActionDTO = new ActivityDTO
			{
				Id = Guid.NewGuid(),
				Name = "Select_Fr8_Object",
				Label = "Select Fr8 Object",
				ActivityTemplate = activityTemplate,
				AuthToken = null
			};

			return requestActionDTO;
		}

		private void SetRoutesOptionSelected(ActivityDTO responseActionDTO)
		{
			using (var updater = Crate.UpdateStorage(responseActionDTO))
			{
				var controls = updater.CrateStorage
					.CrateContentsOfType<StandardConfigurationControlsCM>()
					.Single();

				var dropdownList = (DropDownList)controls.Controls[0];
				dropdownList.selectedKey = "Routes";
				dropdownList.Value = "19";
			}
		}

		private void SetContainersOptionSelected(ActivityDTO responseActionDTO)
		{
			using (var updater = Crate.UpdateStorage(responseActionDTO))
			{
				var controls = updater.CrateStorage
					.CrateContentsOfType<StandardConfigurationControlsCM>()
					.Single();

				var dropdownList = (DropDownList)controls.Controls[0];
				dropdownList.selectedKey = "Containers";
				dropdownList.Value = "21";
			}
		}

		private void ValidateConfigurationCrateStructure(StandardConfigurationControlsCM configCrate)
		{
			var controls = configCrate.Controls;

			Assert.AreEqual(1, controls.Count);

			var configurationControl = controls.FirstOrDefault();

			Assert.IsInstanceOf<DropDownList>(configurationControl);
			Assert.AreEqual("Selected_Fr8_Object", configurationControl.Name);
			Assert.AreEqual("Select Fr8 Object", configurationControl.Label);

			var configurationControlEvents = configurationControl.Events;

			Assert.AreEqual(1, configurationControlEvents.Count);

			var configurationControlEvent = configurationControlEvents.FirstOrDefault();

			Assert.AreEqual("onChange", configurationControlEvent.Name);
			Assert.AreEqual("requestConfig", configurationControlEvent.Handler);

			Assert.NotNull(configurationControl.Source);

			var configurationControlSourceField = configurationControl.Source;

			Assert.AreEqual("Select Fr8 Object", configurationControlSourceField.Label);
			Assert.AreEqual("Standard Design-Time Fields", configurationControlSourceField.ManifestType);
		}

		private static void ValidateFr8ObjectCrateStructure(StandardDesignTimeFieldsCM designTimeCrate)
		{
			var designTimeFields = designTimeCrate.Fields;

			Assert.AreEqual(2, designTimeFields.Count);

			var field1 = designTimeFields[0];

			Assert.AreEqual("Routes", field1.Key);
			Assert.AreEqual("19", field1.Value);

			var field2 = designTimeFields[1];

			Assert.AreEqual("Containers", field2.Key);
			Assert.AreEqual("21", field2.Value);
		}
	}
}