// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public class CosmosAccountLookup : Dictionary<string, IDatabaseLookup>, ICosmosAccountLookup
{
    public CosmosAccountLookup(IDictionary<string, IDatabaseLookup> accounts) : base(accounts)
    {
    }
}