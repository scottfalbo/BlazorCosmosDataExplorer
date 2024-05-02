// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;
using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IRepository
{
    Task<Dictionary<string, List<string>>> GetDatabasesAndContainers();

    Task<List<ExpandoObject>> GetItems(QueryInput queryInput);
}