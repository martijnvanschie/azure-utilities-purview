using Azure.Analytics.Purview.Scanning;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Reduct.Azure.Services.Purview.Scanning
{
    /// <summary>
    /// Represents a client around the Purview Scanning Data Plane 
    /// https://docs.microsoft.com/en-us/rest/api/purview/scanningdataplane/data-sources
    /// </summary>
    public class DataSourceClient
    {
        private readonly ILogger<DataSourceClient> _logger;

        private static TokenCredential _tokenCredential;
        private static Uri _uri;

        public DataSourceClient(string accountName, TokenCredential credential, ILogger<DataSourceClient>? logger = null)
        {
            _logger = logger ?? NullLogger<DataSourceClient>.Instance;

            if (credential == null)
            {
                _logger.LogWarning($"No credential was passed to the constructor.");
                throw new ArgumentNullException(nameof(credential));
            }

            _tokenCredential = credential;
            _uri = new Uri($"https://{accountName}.purview.azure.com/account");
            _logger.LogDebug($"Setting Purview endpoint uri to [{_uri}].");
        }

        public async Task DeleteDataSourceAsync(string dataSourceName)
        {
            var dsClient = new PurviewDataSourceClient(_uri, dataSourceName, _tokenCredential);

            try
            {
                _logger.LogDebug("Calling Purview Scan Data Plane - Data Sources - Delete");
                var response2 = await dsClient.DeleteAsync();
                _logger.LogInformation($"Purview API returned status [{response2.Status}].");
                _logger.LogDebug($"Purview API returned [{response2.Content}].");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while sending request to Purview API. [{ex.Message}]");
                throw;
            }
        }
    }
}