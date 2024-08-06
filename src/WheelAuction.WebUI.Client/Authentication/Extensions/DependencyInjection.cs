using Microsoft.AspNetCore.Components.Authorization;
using WheelAuction.WebUI.Client.Authentication.Services;

namespace WheelAuction.WebUI.Client.Authentication.Extensions;

internal static class DependencyInjection
{
	public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
	{
		services
			.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>()
			.AddCascadingAuthenticationState();

		return services;
	}
}