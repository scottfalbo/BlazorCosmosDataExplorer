// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer;

#nullable disable

public class ServiceConfiguration : IServiceConfiguration
{
    public string CosmosEndpoint { get; set; }
    public string CosmosKey { get; set; }
    public string KeyVaultUri { get; set; }
}

#nullable enable