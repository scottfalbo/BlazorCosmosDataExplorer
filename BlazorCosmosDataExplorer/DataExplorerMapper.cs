// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Riok.Mapperly.Abstractions;

namespace BlazorCosmosDataExplorer;

[Mapper]
public partial class DataExplorerMapper
{
    public partial DomainModel ToDomainModel(StorageContract storageContract);
}