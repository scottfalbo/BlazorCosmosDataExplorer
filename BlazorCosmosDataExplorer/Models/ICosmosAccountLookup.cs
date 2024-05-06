// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public interface ICosmosAccountLookup : IReadOnlyDictionary<string, IDatabaseLookup>
{
}