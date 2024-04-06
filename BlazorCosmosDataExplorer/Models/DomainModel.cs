// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public record DomainModel
{
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    public string DocumentId { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string PartitionKey { get; set; } = string.Empty;

    public decimal Revenue { get; set; }

    public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
}