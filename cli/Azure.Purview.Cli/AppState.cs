using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Reduct.Azure;


namespace Azure.Purview.Cli
{
    internal class AppState
    {
        private static ILogger<AppState> _logger;
        internal static DefaultAzureCredential Credential = CredentialsManager.GetDefaultCredential();

        static AppState()
        {
            _logger = LoggingManager.LoggerFactoryInstance.CreateLogger<AppState>();
        }
    }
}
