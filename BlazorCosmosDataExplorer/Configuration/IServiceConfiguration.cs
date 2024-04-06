// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Configuration;

public interface IServiceConfiguration
{
    string CosmosEndpoint { get; set; }
    string CosmosKey { get; set; }
    string KeyVaultUri { get; set; }
}