using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SchedulingHouseholdChores.UI;
using SchedulingHouseholdChores.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthClientService>();

builder.Services.AddScoped<AuthHttpMessageHandler>();

builder.Services.AddScoped(sp => new HttpClient(sp.GetRequiredService<AuthHttpMessageHandler>())
{ 
    BaseAddress = new Uri("https://localhost:7201") 
});

builder.Services.AddMudServices();

var host = builder.Build();
await host.RunAsync();
