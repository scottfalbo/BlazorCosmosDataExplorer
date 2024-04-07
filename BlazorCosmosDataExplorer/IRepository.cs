// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public interface IRepository
{
    Task<List<ExpandoObject>> GetItems(QueryInput queryInput);
}