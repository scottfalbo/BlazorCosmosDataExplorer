// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer.Models;

public static class DomainModelExtensions
{
    public static List<ExpandoObject> SortResults(this List<ExpandoObject> results, string columnName, bool ascending)
    {
        var propertyInfo = typeof(object).GetProperty(columnName);
        if (propertyInfo == null) return results;

        return ascending
            ? results.OrderBy(e => propertyInfo.GetValue(e, null)).ToList()
            : results.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
    }
}