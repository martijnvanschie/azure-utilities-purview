using Azure;
using Azure.Analytics.Purview.Scanning;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Reduct.Azure.Services.Purview.Model;

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
        private static Uri _uriAccount;
        private static Uri _uriScan;

        public DataSourceClient(string accountName, TokenCredential credential, ILogger<DataSourceClient>? logger = null)
        {
            _logger = logger ?? NullLogger<DataSourceClient>.Instance;

            if (credential == null)
            {
                _logger.LogWarning($"No credential was passed to the constructor.");
                throw new ArgumentNullException(nameof(credential));
            }

            _tokenCredential = credential;
            _uriAccount = new Uri($"https://{accountName}.purview.azure.com/account");
            _uriAccount = new Uri($"https://{accountName}.scan.purview.azure.com");
            _logger.LogDebug($"Setting Purview endpoint uri to [{_uriAccount}].");
        }

        public async Task<DataSourceList> ListAllDataSourceAsync()
        {
            _logger.LogDebug($"Setting Purview endpoint uri to [{_uriAccount}].");
            var dsClient = new PurviewScanningServiceClient(_uriAccount, _tokenCredential);

            try
            {
                _logger.LogDebug("Calling Purview Scan Data Plane - Data Sources - List All");

                var options = new RequestOptions(perCall =>
                {
                    _logger.LogDebug($"Calling paged Purview API [{perCall.Request.Uri}]");
                });

                var response2 = dsClient.GetDataSourcesAsync(options);

                var dsList = new DataSourceList();
                dsList.DataSources = new List<DataSource>();
                dsList.Count = 0;

                await foreach (var secretProperties in response2)
                {
                    var ds = secretProperties.ToObjectFromJson<DataSource>();
                    dsList.DataSources.Add(ds);
                    dsList.Count++;
                }

                return dsList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while sending request to Purview API. [{ex.Message}]");
                throw;
            }
        }

        public async Task DeleteDataSourceAsync(string dataSourceName)
        {
            var dsClient = new PurviewDataSourceClient(_uriAccount, dataSourceName, _tokenCredential);

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