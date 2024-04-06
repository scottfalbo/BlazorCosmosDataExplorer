﻿// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IDataExplorerProcessor
{
    Task DownloadExcel(List<DomainModel> domainModels);

    Task<List<DomainModel>> Process(QueryInput query);
}