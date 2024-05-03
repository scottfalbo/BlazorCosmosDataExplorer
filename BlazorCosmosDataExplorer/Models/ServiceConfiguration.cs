// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

#nullable disable

public class ServiceConfiguration
{
    public IEnumerable<CosmosAccountConfiguration> CosmosAccounts { get; set; }
}

#nullable enable