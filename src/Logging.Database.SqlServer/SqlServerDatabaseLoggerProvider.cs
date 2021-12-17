using System;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tolitech.CodeGenerator.Logging.Database.SqlServer
{
    [ProviderAlias("Database")]
    public class SqlServerDatabaseLoggerProvider : DatabaseLoggerProvider
    {
        public SqlServerDatabaseLoggerProvider(IOptionsMonitor<DatabaseLoggerOptions> settings) : base(settings.CurrentValue)
        {

        }

        public SqlServerDatabaseLoggerProvider(DatabaseLoggerOptions settings) : base(settings)
        {

        }

        protected override DbConnection GetNewConnection
        {
            get
            {
                return new SqlConnection(Settings.ConnectionString);
            }
        }

        protected override string Sql
        {
            get
            {
                return @"insert into [Cg].[Log] 
                    (logId, time, userName, hostName, category, level, text, exception, eventId, activityId, userId, loginName, actionId, actionName, requestId, requestPath, filePath, sql, parameters, stateText, stateProperties, scopeText, scopeProperties) 
                    values 
                    (@logId, @time, @userName, @hostName, @category, @level, @text, @exception, @eventId, @activityId, @userId, @loginName, @actionId, @actionName, @requestId, @requestPath, @filePath, @sql, @parameters, @stateText, @stateProperties, @scopeText, @scopeProperties)";
            }
        }
    }
}
