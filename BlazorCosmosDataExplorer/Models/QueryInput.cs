// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer.Models;

public record QueryInput
{
    public string Container { get; }
    public string Database { get; }
    public string Query { get; }

    public QueryInput(string userInput, string container, string database)
    {
        Query = userInput;
        Container = container;
        Database = database;
    }
}