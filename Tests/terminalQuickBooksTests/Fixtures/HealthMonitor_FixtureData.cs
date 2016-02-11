﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces.DataTransferObjects;
using Data.Interfaces.Manifests;

namespace terminalQuickBooksTests.Fixtures
{
    class HealthMonitor_FixtureData
    {
        public static AuthorizationTokenDTO QuickBooks_AuthTokenDTO()
        {
            return new AuthorizationTokenDTO()
            {
                Token = "lvprdLxfSv3vtByicAac86nlzDiuYnoTA5KxB5roJzCcSpbw;;;;;;;f2TY1CkAtXm1kwXGwcUTCQyiwOAKwJeAvbhLDeFw;;;;;;;1429888620"
            };
        }
        public static AuthorizationTokenDO QuickBooks_AuthTokenDO()
        {
            return new AuthorizationTokenDO()
            {
                Token = "lvprdLxfSv3vtByicAac86nlzDiuYnoTA5KxB5roJzCcSpbw;;;;;;;f2TY1CkAtXm1kwXGwcUTCQyiwOAKwJeAvbhLDeFw;;;;;;;1429888620"
            };
        }

        public static ActivityTemplateDTO Action_Create_Journal_Entry_ActivityTemplate()
        {
            return new ActivityTemplateDTO()
            {
                Id = 1,
                Name = "Create_Journal_Entry_TEST",
                Version = "1"
            };
        }
        public static Fr8DataDTO Action_Create_Journal_Entry_v1_InitialConfiguration_Fr8DataDTO()
        {
            var activityTemplate = Action_Create_Journal_Entry_ActivityTemplate();

            var activityDTO = new ActivityDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Create_Journal_Entry",
                Label = "Create Journal Entry",
                AuthToken = QuickBooks_AuthTokenDTO(),
                ActivityTemplate = activityTemplate
            };

            return new Fr8DataDTO { ActivityDTO = activityDTO };
        }
        public static ActivityTemplateDTO Convert_TableData_To_AccountingTransactions_ActivityTemplate()
        {
            return new ActivityTemplateDTO()
            {
                Id = 2,
                Name = "Convert_TableData_To_AccountingTransactions_TEST",
                Version = "1"
            };
        }
        public static Fr8DataDTO Convert_TableData_To_AccountingTransactions_v1_InitialConfiguration_Fr8DataDTO()
        {
            var activityTemplate = Convert_TableData_To_AccountingTransactions_ActivityTemplate();

            var activityDTO = new ActivityDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Convert_TableData_To_AccountingTransactions",
                Label = "Convert TableData To AccountingTransactions",
                AuthToken = QuickBooks_AuthTokenDTO(),
                ActivityTemplate = activityTemplate
            };

            return new Fr8DataDTO { ActivityDTO = activityDTO };
        }
        public static StandardAccountingTransactionCM GetAccountingTransactionCM()
        {
            var curFinLineDTOList = new List<FinancialLineDTO>();
            var curFirstLineDTO = new FinancialLineDTO()
            {
                Amount = "100",
                AccountId = "1",
                AccountName = "Account-A",
                DebitOrCredit = "Debit",
                Description = "Move money to Accout-B"
            };
            var curSecondLineDTO = new FinancialLineDTO()
            {
                Amount = "100",
                AccountId = "2",
                AccountName = "Account-B",
                DebitOrCredit = "Credit",
                Description = "Move money from Accout-A"
            };
            curFinLineDTOList.Add(curFirstLineDTO);
            curFinLineDTOList.Add(curSecondLineDTO);


            var curAccoutingTransactionDTO = new StandardAccountingTransactionDTO()
            {
                Memo = "That is the test crate",
                FinancialLines = curFinLineDTOList,
                Name = "Code1",
                TransactionDate = DateTime.Parse("2015-12-15")
            };
            var curCrate = new StandardAccountingTransactionCM()
            {
                AccountingTransactions = new List<StandardAccountingTransactionDTO>()
                {
                   curAccoutingTransactionDTO
                }
            };
            return curCrate;
        }

        public static StandardTableDataCM StandardTableData_Test1()
        {
            var headerRow = new TableRowDTO()
            {
                Row = new List<TableCellDTO>()
                {
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("1", "Date")
                    },
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("2", "Description")
                    },
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("3", "Phone")
                    },
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("4", "Travelling")
                    }
                }
            };
            var dataRow1 = new TableRowDTO()
            {
                Row = new List<TableCellDTO>()
                {
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("5", "30/12/2015")
                    },
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("6", "Trip to Samarkand")
                    },
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("7", "70")
                    },
                    new TableCellDTO()
                    {
                        Cell = new FieldDTO("8", "90")
                    }
                }
            };
            return new StandardTableDataCM
            {
                FirstRowHeaders = true,
                Table = new List<TableRowDTO> {headerRow, dataRow1}
            };
        }

        public static ChartOfAccountsCM ChartOfAccounts_Test1()
        {
            return new ChartOfAccountsCM
            {
                Accounts = new List<AccountDTO>
                {
                    new AccountDTO
                    {
                        Id = "1",
                        Name = "Phone"
                    },
                    new AccountDTO
                    {
                        Id = "2",
                        Name = "Travelling"
                    },
                    new AccountDTO
                    {
                        Id = "3",
                        Name = "Accounts Payable"
                    },
                }
            };
        }
    }
}
