// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Configuration;

#nullable disable

public class ServiceConfiguration : IServiceConfiguration
{
    public string CosmosEndpoint { get; set; }
    public string CosmosKey { get; set; }
    public string KeyVaultUri { get; set; }
}

#nullable enable