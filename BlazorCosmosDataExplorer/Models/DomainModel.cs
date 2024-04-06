// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public record DomainModel
{
    public DateTimeOffset? CreatedDate { get; set; }

    public string? DocumentId { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? PartitionKey { get; set; }

    public decimal? Revenue { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }
}