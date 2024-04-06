// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public record QueryInput
{
    public string Query { get; }

    public string Container { get; }

    public string Database { get; }

    public QueryInput(string query, string container, string database)
    {
        Query = query;
        Container = container;
        Database = database;
    }
}