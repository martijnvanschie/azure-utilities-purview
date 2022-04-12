using System.Text.Json.Serialization;

namespace Reduct.Azure.Services.Purview.Model
{
    public class DataSourceList
    {
        [JsonPropertyName("value")]
        public List<DataSource> DataSources { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class DataSource
    {
        [JsonPropertyName("properties")]
        public Properties Properties { get; set; }

        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class CollectionRef
    {
        [JsonPropertyName("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }

        [JsonPropertyName("referenceName")]
        public string ReferenceName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Properties
    {
        [JsonPropertyName("subscriptionId")]
        public object SubscriptionId { get; set; }

        [JsonPropertyName("resourceGroup")]
        public string ResourceGroup { get; set; }

        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }

        [JsonPropertyName("dataUseGovernance")]
        public string DataUseGovernance { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }

        [JsonPropertyName("parentCollection")]
        public object ParentCollection { get; set; }

        [JsonPropertyName("collection")]
        public CollectionRef Collection { get; set; }

        [JsonPropertyName("dataSourceCollectionMovingState")]
        public int DataSourceCollectionMovingState { get; set; }
    }
}
