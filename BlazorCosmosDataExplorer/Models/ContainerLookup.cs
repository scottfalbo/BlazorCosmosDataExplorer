// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public class ContainerLookup : Dictionary<string, IReadOnlyList<string>>, IContainerLookup
{
    public ContainerLookup(IDictionary<string, IReadOnlyList<string>> containers) : base(containers) { }
}
