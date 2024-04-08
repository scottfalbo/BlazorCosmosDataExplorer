// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

var cosmosEndpoint = builder.Configuration["CosmosEndpoint"];
var cosmosKey = builder.Configuration["CosmosKey"];

builder.Services.AddSingleton<IClient, Client>(x =>
{
    var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
    return new Client(cosmosClient);
});

builder.Services.AddTransient<IProcessor, Processor>();
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IExcelWorkbookFactory, ExcelWorkbookFactory>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();