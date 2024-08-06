using WheelAuction.WebUI.Client.Authorization.Policies.Extensions;

namespace WheelAuction.WebUI.Server.Authorization.Extensions;

internal static class DependencyInjection
{
	public static IServiceCollection AddConfiguredAuthorization(this IServiceCollection services)
	{
		services
			.AddAuthorization(authorizationOptions =>
			{
				authorizationOptions
					.AddAllPolicies();
			});

		return services;
	}
}