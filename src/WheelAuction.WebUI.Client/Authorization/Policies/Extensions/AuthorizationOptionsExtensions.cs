using Microsoft.AspNetCore.Authorization;
using WheelAuction.WebUI.Client.Authentication.Enumerations;

namespace WheelAuction.WebUI.Client.Authorization.Policies.Extensions;

public static class AuthorizationOptionsExtensions
{
	public static AuthorizationOptions AddAllPolicies(this AuthorizationOptions authorizationOptions)
	{
		authorizationOptions.
			AddRoleRequirementPolicies();

		return authorizationOptions;
	}

	public static AuthorizationOptions AddRoleRequirementPolicies(this AuthorizationOptions authorizationOptions)
	{
		authorizationOptions.AddPolicy(PolicyNames.Administrator, authorizationPolicyBuilder =>
		{
			authorizationPolicyBuilder.RequireRole(
				RoleName.Administrator.ToString());
		});

		authorizationOptions.AddPolicy(PolicyNames.User, authorizationPolicyBuilder =>
		{
			authorizationPolicyBuilder.RequireRole(
				RoleName.Administrator.ToString(),
				RoleName.User.ToString());
		});

		return authorizationOptions;
	}
}