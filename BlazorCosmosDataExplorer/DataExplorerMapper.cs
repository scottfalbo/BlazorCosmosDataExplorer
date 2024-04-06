// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Riok.Mapperly.Abstractions;

namespace BlazorCosmosDataExplorer;

[Mapper]
public partial class DataExplorerMapper
{
    public partial object ToDomainModel(object storageContract);
}