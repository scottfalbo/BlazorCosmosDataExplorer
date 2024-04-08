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
    private IProcessor _processor { get; set; }

    private string Container { get; set; }

    private string CurrentSortColumn { get; set; }

    private string Database { get; set; }

    private Dictionary<string, List<string>> DatabasesAndContainers { get; set; }

    private bool IsSortAscending { get; set; } = true;

    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    private string Query { get; set; } = "select * from c where c.partitionKey = \"partitionKey_01\"";

    private List<dynamic> Results { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("preventShiftEnter", "query_field");
        }
    }

    protected override async Task OnInitializedAsync()
    {
        DatabasesAndContainers = await _processor.GetDatabasesAndContainers();
        Database = DatabasesAndContainers.Keys.FirstOrDefault();
        Container = DatabasesAndContainers[Database].FirstOrDefault();
    }

    //private void DeDupe()
    //{
    //    var deDupedResults = Results.GroupBy(x => x.Id).Select(x => x.First()).ToList();
    //    Results.Clear();
    //    Results.AddRange(deDupedResults);
    //}

    private void DownloadExcel()
    {
        if (Results.Count > 0)
        {
            _processor.DownloadExcel(Results);
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
        var results = await _processor.Process(queryInput);

        Results.AddRange(results);
        //DeDupe();
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

        Results = Results.SortResults(CurrentSortColumn, IsSortAscending);
    }
}

#nullable enable