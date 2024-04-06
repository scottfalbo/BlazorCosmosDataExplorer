// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer;

public record DomainModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string DocumentId { get; set; }

    public string PartitionKey { get; set; }

    public decimal Revenue { get; set; }

    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
}