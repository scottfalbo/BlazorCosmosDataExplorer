// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer;

public interface ICosmosClientFactory
{
    CosmosClient CreateCosmosClient(string accountEndpoint, string accountKey);
}