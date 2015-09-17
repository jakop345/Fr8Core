﻿using Core.Interfaces;
using Core.Managers.APIManagers;
using Data.Entities;
using Data.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class ActivityTemplate : IActivityTemplate
    {
        public IEnumerable<ActivityTemplateDO> GetAll()
        {
            using (var uow = ObjectFactory.GetInstance<IUnitOfWork>())
            {
                return uow.ActivityTemplateRepository.GetAll();
            }
        }

        public ActivityTemplateDO GetByKey(int curActivityTemplateId)
        {
            using (var uow = ObjectFactory.GetInstance<IUnitOfWork>())
            {
                var curActivityTemplateDO = uow.ActivityTemplateRepository.GetByKey(curActivityTemplateId);
                if (curActivityTemplateDO == null)
                    throw new ArgumentNullException("ActionTemplateId");

                return curActivityTemplateDO;
            }

        }

        public void Register(ActivityTemplateDO activityTemplateDO)
        {
            using (var uow = ObjectFactory.GetInstance<IUnitOfWork>())
            {
                var existingPlugin = uow.PluginRepository
                    .FindOne(x => x.Name == activityTemplateDO.Plugin.Name);

                if (existingPlugin != null)
                {
                    activityTemplateDO.Plugin = existingPlugin;
                }

                if (!uow.ActivityTemplateRepository.GetQuery().Any(t => t.Name == activityTemplateDO.Name))
                {
                    uow.ActivityTemplateRepository.Add(activityTemplateDO);
                    uow.SaveChanges();
                }
            }
        }
    }
}
