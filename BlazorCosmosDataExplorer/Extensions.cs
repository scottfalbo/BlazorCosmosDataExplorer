// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public static class Extensions
{
    public static List<dynamic> SortResults(this List<dynamic> results, string columnName, bool ascending)
    {
        return ascending
            ? results.OrderBy(x => ((IDictionary<string, object>)x)[columnName]).ToList()
            : results.OrderByDescending(x => ((IDictionary<string, object>)x)[columnName]).ToList();
    }
}