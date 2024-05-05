// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer.Models;

public static class Extensions
{
    public static IEnumerable<dynamic> FilterProperties(this IEnumerable<dynamic> items)
    {
        var filteredItems = new List<ExpandoObject>();

        foreach (var item in items)
        {
            IDictionary<string, object?> expandoAsDictionary = new ExpandoObject();

            foreach (var kvp in (IDictionary<string, object>)item)
            {
                if (!kvp.Key.StartsWith("_"))
                {
                    expandoAsDictionary[kvp.Key] = kvp.Value;
                }
            }

            filteredItems.Add((ExpandoObject)expandoAsDictionary);
        }

        return filteredItems;
    }

    public static List<dynamic> SortResults(this List<dynamic> results, string columnName, bool ascending)
    {
        return ascending
            ? results.OrderBy(x => ((IDictionary<string, object>)x)[columnName]).ToList()
            : results.OrderByDescending(x => ((IDictionary<string, object>)x)[columnName]).ToList();
    }
}