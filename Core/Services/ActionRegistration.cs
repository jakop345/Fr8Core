﻿using Core.Interfaces;
using Data.Entities;
using Data.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ActionTemplate :IActionTemplate
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
                var curActionTemplateDO = uow.ActivityTemplateRepository.GetByKey(curActivityTemplateId);
                if (curActionTemplateDO == null)
                    throw new ArgumentNullException("ActionTemplateId");

                return curActionTemplateDO;
            }

        }
    }
}
