using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WheelAuction.WebUI.Server.Authentication.Endpoints;
using WheelAuction.WebUI.Server.Authentication.Entities;
using WheelAuction.WebUI.Server.Logging.Authentication;

namespace WheelAuction.WebUI.Server.Authentication.Extensions;

internal static class EndpointRouteBuilderExtensions
{
	public static IEndpointConventionBuilder MapAdditionalAccountEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
	{
		RouteGroupBuilder accountRouteGroupBuilder = endpointRouteBuilder.MapGroup(EndpointRouteGroups.Account);

		accountRouteGroupBuilder
			.MapLogoutEndpoint();

		return accountRouteGroupBuilder;
	}

	private static IEndpointRouteBuilder MapLogoutEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
	{
		endpointRouteBuilder.MapPost(EndpointRoutes.Logout, async (
			[FromServices] ILoggerFactory loggerFactory,
			[FromServices] SignInManager<User> signInManager,
			[FromForm] string userName,
			[FromForm] string returnUrl) =>
		{
			await signInManager.SignOutAsync();

			ILogger logger = loggerFactory.CreateLogger(
				$"{typeof(EndpointRouteGroups).Namespace}." +
				$"{nameof(EndpointRouteGroups.Account)}." +
				nameof(EndpointRoutes.Logout));

			LogAuthentication.UserLoggedOut(logger, userName);

			return TypedResults.LocalRedirect($"~/{returnUrl}");
		});

		return endpointRouteBuilder;
	}
}