// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.JSInterop;
using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public class Processor : IProcessor
{
    private readonly IDatabaseLookup _databaseLookup;
    private readonly IExcelWorkbookFactory _excelWorkbookFactory;
    private readonly IJSRuntime _jsRuntime;
    private readonly IRepository _repository;

    public Processor(
        IDatabaseLookup databaseLookup,
        IRepository dataExplorerRepository,
        IExcelWorkbookFactory excelWorkbookFactory,
        IJSRuntime jSRuntime)
    {
        _databaseLookup = databaseLookup;
        _repository = dataExplorerRepository;
        _excelWorkbookFactory = excelWorkbookFactory;
        _jsRuntime = jSRuntime;
    }

    public async Task DownloadExcel(List<dynamic> results, List<dynamic> filteredResults)
    {
        var excelData = _excelWorkbookFactory.Create(results, filteredResults);

        var fileName = $"sample.xlsx";

        var base64 = Convert.ToBase64String(excelData);
        var dataUrl = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";

        await _jsRuntime.InvokeVoidAsync("downloadFileFromBase64", fileName, dataUrl);
    }

    public IDatabaseLookup DatabaseLookup()=> _databaseLookup;

    public async Task<List<ExpandoObject>> Process(QueryInput queryInput)
    {
        var response = await _repository.GetItems(queryInput);

        var filteredResults = (List<ExpandoObject>)response.FilterProperties();

        return filteredResults;
    }
}