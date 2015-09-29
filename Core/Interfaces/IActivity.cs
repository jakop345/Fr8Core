﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;

namespace Core.Interfaces
{
	public interface IActivity
	{
		List<ActivityDO> GetUpstreamActivities(ActivityDO curActivityDO);
		List<ActivityDO> GetDownstreamActivities(ActivityDO curActivityDO);
        Task Process(int curActivityId, ProcessDO curProcessDO);
        IEnumerable<ActivityDO> GetNextActivities(ActivityDO curActivityDO);
        IEnumerable<ActivityTemplateDO> GetAvailableActivities(IDockyardAccountDO curAccount);
	}
}