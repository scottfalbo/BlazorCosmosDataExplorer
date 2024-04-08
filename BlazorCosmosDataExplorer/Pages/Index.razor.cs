// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorCosmosDataExplorer.Pages;

#nullable disable

public partial class Index : ComponentBase
{
    [Inject]
    private IJSRuntime _jSRuntime { get; set; }

    [Inject]
    private IProcessor _processor { get; set; }

    private string Container { get; set; }

    private string CurrentSortColumn { get; set; }

    private string Database { get; set; }

    private Dictionary<string, List<string>> DatabasesAndContainers { get; set; }

    private List<dynamic> FilteredResults { get; set; } = new();

    private bool IsSortAscending { get; set; } = true;

    private string Query { get; set; } = "select * from c where c.partitionKey = \"partitionKey_01\"";

    private List<dynamic> Results { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _jSRuntime.InvokeVoidAsync("preventShiftEnter", "query_field");
        }
    }

    protected override async Task OnInitializedAsync()
    {
        DatabasesAndContainers = await _processor.GetDatabasesAndContainers();

        if (DatabasesAndContainers.Any())
        {
            Database = DatabasesAndContainers.Keys.First();
            Container = DatabasesAndContainers[Database].First();
        }
    }

    private void DownloadExcel()
    {
        if (FilteredResults.Count > 0)
        {
            _processor.DownloadExcel(Results, FilteredResults);
        }
    }

    private Task HandleFormSubmit()
    {
        // Prevent the form from submitting when the Enter key is pressed without Shift.
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
        FilteredResults.Clear();

        var queryInput = new QueryInput(Query, Container, Database);
        var results = await _processor.Process(queryInput);

        Results.AddRange(results);
        FilteredResults.AddRange(results);
    }

    private void SelectContainer(string database, string container)
    {
        Database = database;
        Container = container;
    }

    private void SortTable(string columnName)
    {
        if (columnName == CurrentSortColumn)
        {
            IsSortAscending = !IsSortAscending;
        }
        else
        {
            CurrentSortColumn = columnName;
            IsSortAscending = true;
        }

        FilteredResults = FilteredResults.SortResults(CurrentSortColumn, IsSortAscending);
    }
}

#nullable enable