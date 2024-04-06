// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public static class DomainModelExtensions
{
    public static List<DomainModel> SortResults(this List<DomainModel> results, string columnName, bool ascending)
    {
        var propertyInfo = typeof(DomainModel).GetProperty(columnName);
        if (propertyInfo == null) return results;

        return ascending
            ? results.OrderBy(e => propertyInfo.GetValue(e, null)).ToList()
            : results.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
    }
}