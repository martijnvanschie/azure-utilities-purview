using Azure.Analytics.Purview.Scanning;
using Azure.Core;
using Azure.Identity;

namespace Reduct.Azure.Services.Purview.Scanning
{
    public class DataSourceClient
    {
        private TokenCredential _tokenCredential;

        public DataSourceClient(TokenCredential? credential)
        {
            if (credential == null)
            {
                _tokenCredential = CredentialsManager.GetCredentials();
            }
            else
            {
                _tokenCredential = credential;
            }
        }

        public async Task DeleteDataSourceAsync(string accountName, string dataSourceName)
        {
            var uri = new Uri($"https://{accountName}.scan.purview.azure.com");

            //_logger.LogDebug($"Setting Purview endpoint uri to [{uri}].");
            var dsClient = new PurviewDataSourceClient(uri, dataSourceName, _tokenCredential);

            try
            {
                //_logger.LogDebug("Calling Purview Scan Data Plane - Data Sources - Delete");
                var response2 = await dsClient.DeleteAsync();
                //_logger.LogDebug($"Purview API returned status [{response2.Status}].");
                //_logger.LogDebug($"Purview API returned [{response2.Content}].");
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error while sending request to Purview API. [{ex.Message}]");
                throw;
            }
        }
    }
}