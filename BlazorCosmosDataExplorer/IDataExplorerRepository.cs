// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public interface IDataExplorerRepository
{
    Task<List<ExpandoObject>> GetItems(QueryInput queryInput);
}