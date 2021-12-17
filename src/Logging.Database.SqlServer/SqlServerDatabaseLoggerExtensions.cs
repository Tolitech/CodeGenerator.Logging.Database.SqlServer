using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Tolitech.CodeGenerator.Logging.Database.SqlServer
{
    public static class SqlServerDatabaseLoggerExtensions
    {
        static public ILoggingBuilder AddSqlServerDatabaseLogger(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SqlServerDatabaseLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<DatabaseLoggerOptions>, LoggerProviderOptionsChangeTokenSource<DatabaseLoggerOptions, SqlServerDatabaseLoggerProvider>>());
            return DatabaseLoggerExtensions.AddDatabaseLogger(builder);
        }

        static public ILoggingBuilder AddSqlServerDatabaseLogger(this ILoggingBuilder builder, Action<DatabaseLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddSqlServerDatabaseLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
