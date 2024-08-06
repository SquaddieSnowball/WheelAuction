using WheelAuction.WebUI.Client.Authorization.Policies.Extensions;

namespace WheelAuction.WebUI.Client.Authorization.Extensions;

internal static class DependencyInjection
{
	public static IServiceCollection AddConfiguredAuthorization(this IServiceCollection services)
	{
		services
			.AddAuthorizationCore(authorizationOptions =>
			{
				authorizationOptions
					.AddAllPolicies();
			});

		return services;
	}
}