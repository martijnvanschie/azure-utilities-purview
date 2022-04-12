using System.Text.Json.Serialization;

namespace Reduct.Azure.Services.Purview.Model
{
    public class CollectionList
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<Collection> Collections { get; set; }
    }

    public class Collection
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("friendlyName")]
        public string FriendlyName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("systemData")]
        public SystemData SystemData { get; set; }

        [JsonPropertyName("collectionProvisioningState")]
        public string CollectionProvisioningState { get; set; }

        [JsonPropertyName("parentCollection")]
        public CollectionRef ParentCollection { get; set; }

        /// <summary>
        /// Formats the name to a generic name.
        /// </summary>
        /// <param name="value">The value representing the name.</param>
        /// <param name="prefix">The optional preefix for the name.</param>
        /// <returns></returns>
        public static string EncodeStringForName(string value, string prefix = "")
        {
            return EncodeString(prefix) + EncodeString(value);
        }

        private static string EncodeString(string value)
        {
            return value.ToLower().Replace(' ', '-').Replace(".", null);
        }
    }

    //public class ParentCollection
    //{
    //    [JsonPropertyName("type")]
    //    public string Type { get; set; }

    //    [JsonPropertyName("referenceName")]
    //    public string ReferenceName { get; set; }
    //}

    public class SystemData
    {
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdByType")]
        public string CreatedByType { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedByType")]
        public string LastModifiedByType { get; set; }

        [JsonPropertyName("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }
    }
}
