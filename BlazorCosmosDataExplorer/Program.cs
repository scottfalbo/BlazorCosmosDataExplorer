// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Azure.Identity;
using BlazorCosmosDataExplorer;
using BlazorCosmosDataExplorer.Configuration;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
var serviceConfiguration = CreateServiceConfiguration(builder, builder.Configuration);

var cosmosEndpoint = serviceConfiguration.CosmosEndpoint;
var cosmosKey = serviceConfiguration.CosmosKey;
var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

builder.Services.AddSingleton<IDataExplorerClient, DataExplorerClient>(sp =>
{
    var cosmosClient = new CosmosClient(serviceConfiguration.CosmosEndpoint, serviceConfiguration.CosmosKey);
    return new DataExplorerClient(cosmosClient);
});

builder.Services.AddTransient<IDataExplorerProcessor, DataExplorerProcessor>();
builder.Services.AddTransient<IDataExplorerRepository, DataExplorerRepository>();

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

static ServiceConfiguration CreateServiceConfiguration(WebApplicationBuilder builder, IConfiguration configuration)
{
    var keyVaultUri = configuration["KeyVaultUri"];

    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddUserSecrets(builder.Environment.ApplicationName);

    var clientId = configuration["Azure:ClientId"];
    var clientSecret = configuration["Azure:ClientSecret"];
    var tenantId = configuration["Azure:TenantId"];

    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new ClientSecretCredential(tenantId, clientId, clientSecret));

    var serviceConfiguration = new ServiceConfiguration();
    builder.Configuration.Bind(serviceConfiguration);

    builder.Services.AddSingleton(serviceConfiguration);
    return serviceConfiguration;
}