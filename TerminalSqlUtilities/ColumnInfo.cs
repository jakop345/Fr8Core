﻿using System;
using System.Data;

namespace TerminalSqlUtilities
{
    public class ColumnInfo
    {
        public ColumnInfo(string fullColumnName, DbType dbType)
        {
            var columnNameTokens = fullColumnName.Split(
                new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (columnNameTokens.Length == 2)
            {
                _tableInfo = new TableInfo("", columnNameTokens[0]);
                _columnName = columnNameTokens[1];
                _dbType = dbType;
            }
            else if (columnNameTokens.Length == 3)
            {
                _tableInfo = new TableInfo(columnNameTokens[0], columnNameTokens[1]);
                _columnName = columnNameTokens[2];
                _dbType = dbType;
            }
            else
            {
                throw new ApplicationException("Invalid column name");
            }
        }

        public ColumnInfo(TableInfo tableInfo, string columnName, DbType dbType)
        {
            _tableInfo = tableInfo;
            _columnName = columnName;
            _dbType = dbType;
        }

        public TableInfo TableInfo { get { return _tableInfo; } }

        public string ColumnName { get { return _columnName; } }

        public DbType DbType { get { return _dbType; } }

        
        private readonly TableInfo _tableInfo;
        private readonly string _columnName;
        private readonly DbType _dbType;
    }
}
