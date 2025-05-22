using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KooliProjekt.BlazorApp;
using KooliProjekt.PublicAPI; // lisa PublicAPI namespace

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient, mis osutab sinu API aadressile
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7136/api/") });

// Lisa IApiClient ja ApiClient teenus
builder.Services.AddScoped<IApiClient, ApiClient>();

await builder.Build().RunAsync();
