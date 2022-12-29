using HiLoGame.Frontend;
using HiLoGame.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = builder.Configuration.GetValue<string>("BaseUrl");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<IGameInfoService, GameInfoService>();
builder.Services.AddScoped<IPlayerInfoService, PlayerInfoService>();
builder.Services.AddScoped<SessionStorageAccessor>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
