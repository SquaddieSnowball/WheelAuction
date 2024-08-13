using FluentValidation;
using WheelAuction.Application.Extensions;
using WheelAuction.Infrastructure.Extensions;
using WheelAuction.Infrastructure.Persistence.Services.Abstractions;
using WheelAuction.WebUI.Client.Services;
using WheelAuction.WebUI.Client.Services.Abstractions;
using WheelAuction.WebUI.Server.Authentication.Extensions;
using WheelAuction.WebUI.Server.Authentication.Services.Abstractions;
using WheelAuction.WebUI.Server.Authorization.Extensions;
using WheelAuction.WebUI.Server.Components;
using WheelAuction.WebUI.Server.Configuration;
using WheelAuction.WebUI.Server.Configuration.Options.Extensions;
using WheelAuction.WebUI.Server.Helpers;
using WheelAuction.WebUI.Server.Resources;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

string dbConnectionString = applicationBuilder.Configuration.GetConnectionString(
	EnvironmentHelper.IsContainer
	? ConnectionStringNames.ContainerDb
	: ConnectionStringNames.LocalDb)!;

applicationBuilder.Services
	.AddApplicationServices()
	.AddInfrastructureServices(dbConnectionString);

applicationBuilder.Services
	.AddConfiguredOptions();

applicationBuilder.Services
	.AddValidatorsFromAssemblies([
		WheelAuction.WebUI.Server.Shared.AssemblyReference.Assembly,
		WheelAuction.WebUI.Client.Shared.AssemblyReference.Assembly],
		includeInternalTypes: true);

applicationBuilder.Services
	.AddLocalization(localizationOptions =>
	{
		localizationOptions.ResourcesPath = ResourcesSettings.Path;
	});

applicationBuilder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

applicationBuilder.Services
	.AddScoped<IRedirectManager, RedirectManager>();

applicationBuilder.Services
	.AddAuthenticationServices()
	.AddConfiguredIdentity(dbConnectionString)
	.AddConfiguredAuthorization();

WebApplication application = applicationBuilder.Build();

if (application.Environment.IsDevelopment())
{
	foreach (IMigrationsConfigurator migrationsConfigurator in application.Services.GetServices<IMigrationsConfigurator>())
		await migrationsConfigurator.ApplyAsync();

	await application.Services.GetRequiredService<IAuthenticationPersistenceConfigurator>().ConfigureAsync();

	application
		.UseWebAssemblyDebugging();
}
else
{
	application
		.UseHsts();
}

application
	.UseHttpsRedirection()
	.UseStaticFiles()
	.UseAntiforgery();

application
	.MapRazorComponents<App>()
	.AddAdditionalAssemblies(WheelAuction.WebUI.Client.Shared.AssemblyReference.Assembly)
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AllowAnonymous();

application.
	MapAdditionalAccountEndpoints();

await application.RunAsync();