using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Tolitech.CodeGenerator.Logging.Database.SqlServer.Tests
{
    public class LoggingDatabaseSqlServerTest
    {
        private const string CONNECTION_STRING = "Server=localhost; Database=Logging; User ID=sa; Password=Password@123;";

        private readonly ILogger<LoggingDatabaseSqlServerTest> _logger;        

        public LoggingDatabaseSqlServerTest()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var logLevel = (LogLevel)config.GetSection("Logging:Database:LogLevel").GetValue(typeof(LogLevel), "Default");

            var loggerFactory = LoggerFactory.Create(logger =>
            {
                logger
                    .AddConfiguration(config.GetSection("Logging"))
                    .AddSqlServerDatabaseLogger(x =>
                    {
                        x.LogLevel = logLevel;
                        x.ConnectionString = CONNECTION_STRING;
                    });
            });

            _logger = loggerFactory.CreateLogger<LoggingDatabaseSqlServerTest>();
        }

        [Fact(DisplayName = "LoggingDatabaseSqlServer - Log - Valid")]
        public void LoggingDatabaseSqlServer_LogEntry_Valid()
        {
            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");
            _logger.LogInformation("Information");
            _logger.LogWarning("Warning");
            _logger.LogError("Error");
            _logger.LogCritical("Critical");
            Thread.Sleep(5000);

            string selectCount = "select count(*) from [cg].[log]";
            using var conn = new SqlConnection(CONNECTION_STRING);
            using var command = new SqlCommand(selectCount, conn);
            conn.Open();
            int count = (int)command.ExecuteScalar();
            conn.Close();

            Assert.True(count > 0);
        }
    }
}
