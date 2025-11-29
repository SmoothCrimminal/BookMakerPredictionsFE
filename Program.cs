using BookMakerPredictionsFE;
using BookMakerPredictionsFE.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddHttpClient(nameof(FixtureService), sp => sp.BaseAddress = new Uri("http://192.168.0.21:5000"));
builder.Services.AddSingleton<FixtureService>();
builder.Services.AddDistributedMemoryCache();

await builder.Build().RunAsync();