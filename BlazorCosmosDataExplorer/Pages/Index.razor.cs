// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorCosmosDataExplorer.Pages;

#nullable disable

public partial class Index : ComponentBase
{
    [Inject]
    private IDataExplorerProcessor _dataExplorerProcessor { get; set; }

    private string Container { get; set; } = "ContainerOne";

    private string Database { get; set; } = "PaleSpecter";

    private string Query { get; set; }

    private List<DomainModel> Results { get; set; } = new();

    private void DownloadExcel()
    {
        if (Results.Count > 0)
        {
            _dataExplorerProcessor.DownloadExcel(Results);
        }
    }

    private Task HandleFormSubmit()
    {
        // Prevent the form from submitting when the Enter key is pressed without Shift.
        // This is called on form submission, which we don't want to happen on just Enter.
        return Task.CompletedTask;
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter" && e.ShiftKey)
        {
            await ProcessQuery();
        }
    }

    private async Task ProcessQuery()
    {
        Results.Clear();
        var queryInput = new QueryInput(Query, Container, Database);
        var results = await _dataExplorerProcessor.Process(queryInput);
        Results = results;
    }
}

#nullable enable