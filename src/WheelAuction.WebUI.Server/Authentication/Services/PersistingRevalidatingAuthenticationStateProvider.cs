using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WheelAuction.WebUI.Client.Authentication.Entities;
using WheelAuction.WebUI.Server.Authentication.Entities;

namespace WheelAuction.WebUI.Server.Authentication.Services;

internal class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly IdentityOptions _identityOptions;
	private readonly PersistentComponentState _persistentComponentState;
	private readonly PersistingComponentStateSubscription _persistingComponentStateSubscription;

	private Task<AuthenticationState>? _authenticationStateTask;

	protected override TimeSpan RevalidationInterval => TimeSpan.FromHours(1);

	public PersistingRevalidatingAuthenticationStateProvider(
		ILoggerFactory loggerFactory,
		IServiceScopeFactory serviceScopeFactory,
		IOptions<IdentityOptions> identityOptionsAccessor,
		PersistentComponentState persistentComponentState)
		: base(loggerFactory)
	{
		(_serviceScopeFactory, _identityOptions, _persistentComponentState) =
		(serviceScopeFactory, identityOptionsAccessor.Value, persistentComponentState);

		AuthenticationStateChanged += OnAuthenticationStateChanged;

		_persistingComponentStateSubscription = persistentComponentState.RegisterOnPersisting(
			OnPersistingAsync,
			RenderMode.InteractiveWebAssembly);
	}

	protected override async Task<bool> ValidateAuthenticationStateAsync(
		AuthenticationState authenticationState,
		CancellationToken cancellationToken)
	{
		await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
		UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

		ClaimsPrincipal claimsPrincipal = authenticationState.User;
		User? user = await userManager.GetUserAsync(claimsPrincipal);

		if (user is null)
			return false;
		else
			return await ValidateClaimsPrincipalSecurityStampAsync(claimsPrincipal, user, userManager);
	}

	private async Task<bool> ValidateClaimsPrincipalSecurityStampAsync(
		ClaimsPrincipal claimsPrincipal,
		User user,
		UserManager<User> userManager)
	{
		if (!userManager.SupportsUserSecurityStamp)
			return true;
		else
		{
			string? claimsPrincipalSecurityStamp =
				claimsPrincipal.FindFirstValue(_identityOptions.ClaimsIdentity.SecurityStampClaimType);
			string userSecurityStamp =
				await userManager.GetSecurityStampAsync(user);

			return claimsPrincipalSecurityStamp == userSecurityStamp;
		}
	}

	private void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask) =>
		_authenticationStateTask = authenticationStateTask;

	private async Task OnPersistingAsync()
	{
		if (_authenticationStateTask is null)
			return;

		AuthenticationState authenticationState = await _authenticationStateTask;
		ClaimsPrincipal claimsPrincipal = authenticationState.User;

		if (claimsPrincipal.Identity?.IsAuthenticated is true)
		{
			_persistentComponentState.PersistAsJson(
				nameof(AuthenticationStateInfo),
				new AuthenticationStateInfo(
					claimsPrincipal.FindFirst(_identityOptions.ClaimsIdentity.UserNameClaimType)?.Value ?? string.Empty,
					claimsPrincipal.FindFirst(_identityOptions.ClaimsIdentity.RoleClaimType)?.Value ?? string.Empty));
		}
	}

	protected override void Dispose(bool disposing)
	{
		AuthenticationStateChanged -= OnAuthenticationStateChanged;
		_persistingComponentStateSubscription.Dispose();

		base.Dispose(disposing);
	}
}