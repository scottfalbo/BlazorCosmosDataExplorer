// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public record DomainModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string DocumentId { get; set; } = string.Empty;

    public string PartitionKey { get; set; } = string.Empty;

    public decimal Revenue { get; set; }

    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
}