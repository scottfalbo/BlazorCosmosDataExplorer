// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Configuration;

#nullable disable

public class ServiceConfiguration
{
    public IEnumerable<CosmosAccountConfiguration> CosmosAccounts { get; set; }
}

#nullable enable