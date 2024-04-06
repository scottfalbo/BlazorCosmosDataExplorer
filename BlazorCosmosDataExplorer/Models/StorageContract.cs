// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlazorCosmosDataExplorer.Models;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
public class StorageContract
{
    public DateTimeOffset CreatedDate { get; set; }

    public string DocumentId { get; set; }

    [JsonRequired]
    public Guid Id { get; set; }

    [JsonRequired]
    public string Name { get; set; }

    [JsonRequired]
    public string PartitionKey { get; set; }

    public decimal Revenue { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public StorageContract(DomainModel domainModel)
    {
        Id = domainModel.Id;
        Name = domainModel.Name;
        DocumentId = domainModel.DocumentId;
        PartitionKey = domainModel.PartitionKey;
        Revenue = domainModel.Revenue;
        CreatedDate = domainModel.CreatedDate;
        UpdatedDate = domainModel.UpdatedDate;
    }
}