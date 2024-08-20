using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WheelAuction.Application.Extensions;
using WheelAuction.WebUI.Client.Authentication.Extensions;
using WheelAuction.WebUI.Client.Authorization.Extensions;
using WheelAuction.WebUI.Client.Services;
using WheelAuction.WebUI.Client.Services.Abstractions;
using WheelAuction.WebUI.Client.Shared;

WebAssemblyHostBuilder hostBuilder = WebAssemblyHostBuilder.CreateDefault(args);

hostBuilder.Services
	.AddApplicationServices();

hostBuilder.Services
	.AddValidatorsFromAssembly(
		AssemblyReference.Assembly,
		includeInternalTypes: true);

hostBuilder.Services
	.AddLocalization();

hostBuilder.Services
	.AddScoped<IRedirectManager, RedirectManager>();

hostBuilder.Services
	.AddAuthenticationServices()
	.AddConfiguredAuthorization();

WebAssemblyHost host = hostBuilder.Build();

await host.RunAsync();