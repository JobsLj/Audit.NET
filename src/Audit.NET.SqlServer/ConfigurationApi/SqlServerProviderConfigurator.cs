﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Core;

namespace Audit.SqlServer.Configuration
{
    public class SqlServerProviderConfigurator : ISqlServerProviderConfigurator
    {
        internal Func<AuditEvent, string> _connectionStringBuilder = ev => "data source=localhost;initial catalog=Audit;integrated security=true;";
        internal Func<AuditEvent, string> _schemaBuilder = null;
        internal Func<AuditEvent, string> _tableNameBuilder = ev => "Event";
        internal Func<AuditEvent, string> _idColumnNameBuilder = ev => "Id";
        internal Func<AuditEvent, string> _jsonColumnNameBuilder = ev => "Data";
        internal Func<AuditEvent, string> _lastUpdatedColumnNameBuilder = null;

        public ISqlServerProviderConfigurator ConnectionString(string connectionString)
        {
            _connectionStringBuilder = ev => connectionString;
            return this;
        }

        public ISqlServerProviderConfigurator ConnectionString(Func<AuditEvent, string> connectionStringBuilder)
        {
            _connectionStringBuilder = connectionStringBuilder;
            return this;
        }

        public ISqlServerProviderConfigurator TableName(string tableName)
        {
            _tableNameBuilder = ev => tableName;
            return this;
        }

        public ISqlServerProviderConfigurator TableName(Func<AuditEvent, string> tableNameBuilder)
        {
            _tableNameBuilder = tableNameBuilder;
            return this;
        }

        public ISqlServerProviderConfigurator IdColumnName(string idColumnName)
        {
            _idColumnNameBuilder = ev => idColumnName;
            return this;
        }

        public ISqlServerProviderConfigurator IdColumnName(Func<AuditEvent, string> idColumnNameBuilder)
        {
            _idColumnNameBuilder = idColumnNameBuilder;
            return this;
        }

        public ISqlServerProviderConfigurator JsonColumnName(string jsonColumnName)
        {
            _jsonColumnNameBuilder = ev => jsonColumnName;
            return this;
        }

        public ISqlServerProviderConfigurator JsonColumnName(Func<AuditEvent, string> jsonColumnNameBuilder)
        {
            _jsonColumnNameBuilder = jsonColumnNameBuilder;
            return this;
        }

        public ISqlServerProviderConfigurator LastUpdatedColumnName(string lastUpdatedColumnName)
        {
            _lastUpdatedColumnNameBuilder = ev => lastUpdatedColumnName;
            return this;
        }

        public ISqlServerProviderConfigurator LastUpdatedColumnName(Func<AuditEvent, string> lastUpdatedColumnNameBuilder)
        {
            _lastUpdatedColumnNameBuilder = lastUpdatedColumnNameBuilder;
            return this;
        }

        public ISqlServerProviderConfigurator Schema(string schema)
        {
            _schemaBuilder = ev => schema;
            return this;
        }

        public ISqlServerProviderConfigurator Schema(Func<AuditEvent, string> schemaBuilder)
        {
            _schemaBuilder = schemaBuilder;
            return this;
        }
    }
}
