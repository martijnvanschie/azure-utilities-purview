using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Purview.Cli
{
    internal class LoggingManager
    {
        static ILoggerFactory _loggerFactory;

        public static ILoggerFactory LoggerFactoryInstance { get { return _loggerFactory; } }

        public static void SetupLogging()
        {
            var _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConfiguration(_config.GetSection("Logging"))
                    .AddConsole();
            });

            _loggerFactory = loggerFactory;
        }
    }
}
