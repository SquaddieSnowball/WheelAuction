using WheelAuction.WebUI.Server.Components;

WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

WebApplication application = applicationBuilder.Build();

if (application.Environment.IsDevelopment())
{
	application
		.UseWebAssemblyDebugging();
}
else
{
	application
		.UseExceptionHandler("/Error", true)
		.UseHsts();
}

application
	.UseHttpsRedirection()
	.UseStaticFiles()
	.UseAntiforgery();

application
	.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(WheelAuction.WebUI.Client.Components._Imports).Assembly);

await application.RunAsync();