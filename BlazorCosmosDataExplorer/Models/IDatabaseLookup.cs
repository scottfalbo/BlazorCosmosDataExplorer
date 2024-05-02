// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public interface IDatabaseLookup : IReadOnlyDictionary<string, IContainerLookup>
{

}
