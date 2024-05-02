// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public class DatabaseLookup : Dictionary<string, IContainerLookup>, IDatabaseLookup
{
    public DatabaseLookup(IDictionary<string, IContainerLookup> databases) : base(databases) { }
}
