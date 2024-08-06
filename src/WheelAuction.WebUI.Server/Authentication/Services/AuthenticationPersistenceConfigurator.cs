using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WheelAuction.WebUI.Client.Authentication.Enumerations;
using WheelAuction.WebUI.Server.Authentication.Entities;
using WheelAuction.WebUI.Server.Authentication.Services.Abstractions;
using WheelAuction.WebUI.Server.Configuration.Options;
using WheelAuction.WebUI.Server.Configuration.Options.InputModels;

namespace WheelAuction.WebUI.Server.Authentication.Services;

internal class AuthenticationPersistenceConfigurator(
	IServiceScopeFactory serviceScopeFactory,
	IOptions<AuthenticationOptions> authenticationOptionsAccessor)
	: IAuthenticationPersistenceConfigurator
{
	private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
	private readonly AuthenticationOptions _authenticationOptions = authenticationOptionsAccessor.Value;

	public async Task ConfigureAsync()
	{
		await EnsureRolesAsync();
		await EnsureUsersAsync();
	}

	private async Task EnsureUsersAsync()
	{
		await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
		UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

		await EnsureUserAsync(userManager, _authenticationOptions.MainUser, RoleName.Administrator);

		if (_authenticationOptions.AdditionalUsers?.Any() is true)
		{
			foreach (AuthenticationOptionsUserInputModel additionalUser in _authenticationOptions.AdditionalUsers)
			{
				await EnsureUserAsync(
					userManager,
					additionalUser,
					Enum.TryParse(additionalUser.RoleName, out RoleName roleName) ? roleName : RoleName.User);
			}
		}
	}

	private async Task EnsureRolesAsync()
	{
		await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
		RoleManager<Role> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

		string[] definedRoleNames = Enum.GetNames(typeof(RoleName));

		if (roleManager.SupportsQueryableRoles)
		{
			string[] storedRoleNames = [.. roleManager.Roles.Select(role => role.Name)];

			foreach (string roleName in definedRoleNames)
			{
				if (!storedRoleNames.Contains(roleName))
					await roleManager.CreateAsync(Role.Create(roleName));
			}
		}
		else
		{
			foreach (string roleName in definedRoleNames)
			{
				Role? role = await roleManager.FindByNameAsync(roleName);

				if (role is null)
					await roleManager.CreateAsync(Role.Create(roleName));
			}
		}
	}

	private static async Task EnsureUserAsync(
		UserManager<User> userManager,
		AuthenticationOptionsUserInputModel userInputModel,
		RoleName roleName)
	{
		if (await userManager.FindByNameAsync(userInputModel.UserName) is null)
		{
			User user = User.Create(userInputModel.UserName);
			await userManager.CreateAsync(user, userInputModel.Password);

			if (userManager.SupportsUserRole)
				await userManager.AddToRoleAsync(user, roleName.ToString());
		}
	}
}