// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer;

public class CosmosClientFactory : ICosmosClientFactory
{
    public CosmosClient CreateCosmosClient(string accountEndpoint, string accountKey)
    {
        return new CosmosClient(accountEndpoint, accountKey);
    }
}