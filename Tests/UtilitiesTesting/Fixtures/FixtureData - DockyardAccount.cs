﻿using Core.Services;
using Data.Entities;
using Data.Interfaces;
using Data.States;
using Microsoft.AspNet.Identity.EntityFramework;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace UtilitiesTesting.Fixtures
{
    partial class FixtureData
    {
        public static Fr8AccountDO TestDockyardAccount1()
        {
            var curEmailAddressDO = TestEmailAddress1();
            return new Fr8AccountDO()
            {
                Id = "testuser1",
                EmailAddress = curEmailAddressDO,
                FirstName = "Alex",
                State = 1
            };
        }
        public static Fr8AccountDO TestDockyardAccount2()
        {
            var curEmailAddressDO = TestEmailAddress2();
            return new Fr8AccountDO()
            {
                Id = "testUser1",
                EmailAddress = curEmailAddressDO,
                FirstName = "Alex",
                State = 1
            };
        }

        public static Fr8AccountDO TestDeveloperAccount()
        {
            var curEmailAddressDO = TestEmailAddress2();
            return new Fr8AccountDO()
            {
                Id = "developerfoo",
                EmailAddress = curEmailAddressDO,
                FirstName = "developer",
                State = 1
            };
        }

        public static Fr8AccountDO TestDockyardAccount3()
        {
            DockyardAccount _dockyardAccount = ObjectFactory.GetInstance<DockyardAccount>();
            using (var uow = ObjectFactory.GetInstance<IUnitOfWork>())
            {
                return _dockyardAccount.Register(uow, "alexlucre", "Alex", "Lucre1", "alex@123", Roles.Admin);
            }
        }

        public static Fr8AccountDO TestDockyardAccount4()
        {
            var curEmailAddressDO = TestEmailAddress1();
            return new Fr8AccountDO()
            {
                EmailAddress = curEmailAddressDO,
                FirstName = "Alex",
                LastName = "Lucre1",
                UserName = "alexlucre"
               
            };


        }

        /* public DockyardAccountDO TestDockyardAccount2()
         {
             var curEmailAddressDO = TestEmailAddress5();
             return _uow.DockyardAccountRepository.GetOrCreateDockyardAccount(curEmailAddressDO);
         }

         public DockyardAccountDO TestDockyardAccount3()
         {
             var curEmailAddressDO = TestEmailAddress3();
             return _uow.DockyardAccountRepository.GetOrCreateDockyardAccount(curEmailAddressDO);
         }*/
    }
}

