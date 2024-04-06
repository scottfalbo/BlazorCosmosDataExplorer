// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlazorCosmosDataExplorer.Models;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
public class StorageContract
{
    public DateTimeOffset? CreatedDate { get; set; }

    public string? DocumentId { get; set; }

    [JsonRequired]
    public string Id { get; set; } = string.Empty;

    public string? Name { get; set; }

    [JsonRequired]
    public string PartitionKey { get; set; } = string.Empty;

    public decimal? Revenue { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }
}