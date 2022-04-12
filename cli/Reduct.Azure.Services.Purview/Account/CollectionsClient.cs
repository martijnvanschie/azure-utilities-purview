using Azure.Analytics.Purview.Account;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Reduct.Azure.Services.Purview.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reduct.Azure.Services.Purview.Account
{
    /// <summary>
    /// Represents a client around the Purview Account Data Plane
    /// https://docs.microsoft.com/en-us/rest/api/purview/accountdataplane/collections
    /// </summary>
    public class CollectionsClient
    {
        private readonly ILogger<CollectionsClient> _logger;

        private static TokenCredential _tokenCredential;
        private static Uri _uri;
        private static PurviewAccountClient _client;

        public CollectionsClient(string accountName, TokenCredential credential, ILogger<CollectionsClient>? logger = null)
        {
            _logger = logger ?? NullLogger<CollectionsClient>.Instance;

            if (credential == null)
            {
                _logger.LogWarning($"No credential was passed to the constructor.");
                throw new ArgumentNullException(nameof(credential));
            }

            _tokenCredential = credential;

            _uri = new Uri($"https://{accountName}.purview.azure.com/account");
            _logger.LogDebug($"Setting Purview endpoint uri to [{_uri}].");

            _client = new PurviewAccountClient(_uri, credential);
        }

        public async Task<CollectionList> GetAllCollectionsAsync()
        {
            try
            {
                _logger.LogDebug("Calling Purview Account API");
                var response2 = await _client.GetCollectionsAsync();
                _logger.LogDebug($"Purview API returned status [{response2.Status}].");
                _logger.LogDebug($"Purview API returned [{response2.Content.ToString().Substring(0, 50)}].");

                var collectionsResponse = response2.Content.ToObjectFromJson<CollectionList>();

                return collectionsResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while sending request to Purview API. [{ex.Message}]");
                throw;
            }
        }

        public async Task<Collection> CreateOrUpdateCollectionAsync(Collection collection)
        {
            try
            {
                PurviewCollection _c = _client.GetCollectionClient(collection.Name);

                var content = RequestContent.Create(collection);
                var response2 = await _c.CreateOrUpdateCollectionAsync(content);

                _logger.LogInformation($"Purview API returned status [{response2.Status}].");
                if (response2.Status == 200)
                {
                    _logger.LogInformation($"Collection [{collection.Name}] sucessfully created.");
                }

                return response2.Content.ToObjectFromJson<Collection>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while sending request to Purview API. [{ex.Message}]");
                throw;
            }
        }

        public async Task<bool> DeleteCollectionAsync(Collection collection)
        {
            return await DeleteCollectionAsync(collection.Name);
        }

        public async Task<bool> DeleteCollectionAsync(string collectionName)
        {
            try
            {
                PurviewCollection _c = _client.GetCollectionClient(collectionName);

                _logger.LogDebug("Calling Purview Account Data Plane - Collections - Delete Collection");
                var response2 = await _c.DeleteCollectionAsync();
                _logger.LogInformation($"Purview API returned status [{response2.Status}].");
                return response2.Status.Equals(204);
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
