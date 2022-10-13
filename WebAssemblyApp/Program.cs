using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebAssemblyApp;
using WebAssemblyApp.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<AuthorizationMessageHandler>();
builder.Services.AddHttpClient("WebApi", client => 
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl"));
}).AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("WebApi"));

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
