// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer;
using BlazorCosmosDataExplorer.Configuration;

var builder = WebApplication.CreateBuilder(args);

var cosmosAccountsSection = builder.Configuration.GetSection("CosmosAccounts");
var cosmosAccounts = new List<CosmosAccountConfiguration>();
cosmosAccountsSection.Bind(cosmosAccounts);

var serviceConfiguration = new ServiceConfiguration
{
    CosmosAccounts = cosmosAccounts
};

builder.Services.AddCosmosClients(serviceConfiguration);
// await builder.Services.AddDatabaseLookup(cosmosClient);
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