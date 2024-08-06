using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WheelAuction.WebUI.Client.Authorization.Extensions;
using WheelAuction.WebUI.Client.Services.Abstractions;
using WheelAuction.WebUI.Client.Services;
using WheelAuction.WebUI.Client.Authentication.Extensions;

WebAssemblyHostBuilder hostBuilder = WebAssemblyHostBuilder.CreateDefault(args);

hostBuilder.Services
	.AddLocalization();

hostBuilder.Services
	.AddScoped<IRedirectManager, RedirectManager>();

hostBuilder.Services
	.AddAuthenticationServices()
	.AddConfiguredAuthorization();

WebAssemblyHost host = hostBuilder.Build();

await host.RunAsync();