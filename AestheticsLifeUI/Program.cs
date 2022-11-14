using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AestheticsLifeUI;
using AestheticsLifeUI.DataAccess;
using AestheticsLifeUI.DataAccess.Abstractions;
using AestheticsLifeUI.Helpers;
using AestheticsLifeUI.Services;
using AestheticsLifeUI.Services.Abstractions;
using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7090") });
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services
    .AddAuthorizationCore()
    .AddScoped<JwtAuthenticationStateProvider>()
    .AddScoped<AuthenticationStateProvider>(
        p => p.GetRequiredService<JwtAuthenticationStateProvider>())
    .AddScoped<ILoginService>(
        p => p.GetRequiredService<JwtAuthenticationStateProvider>())
    .AddScoped<IAuthService, AuthService>();
builder.Services.AddBlazorise( options =>
    {
        options.Immediate = true;
    } )
    .AddMaterialProviders()
    .AddMaterialIcons();

await builder.Build().RunAsync();