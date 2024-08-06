using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WheelAuction.WebUI.Client.Authentication.Entities;

namespace WheelAuction.WebUI.Client.Authentication.Services;

internal class PersistentAuthenticationStateProvider : AuthenticationStateProvider
{
	private static readonly Task<AuthenticationState> s_defaultAuthenticationStateTask =
		Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

	private readonly Task<AuthenticationState> _authenticationStateTask = s_defaultAuthenticationStateTask;

	public PersistentAuthenticationStateProvider(PersistentComponentState persistentComponentState)
	{
		AuthenticationStateInfo? authenticationStateInfo = GetAuthenticationStateInfo(persistentComponentState);

		if (authenticationStateInfo is null)
			return;

		IEnumerable<Claim> claims = GenerateClaims(authenticationStateInfo);

		_authenticationStateTask =
			Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims))));
	}

	public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authenticationStateTask;

	private AuthenticationStateInfo? GetAuthenticationStateInfo(PersistentComponentState persistentComponentState)
	{
		persistentComponentState.TryTakeFromJson(
			nameof(AuthenticationStateInfo),
			out AuthenticationStateInfo? authenticationStateInfo);

		return authenticationStateInfo;
	}

	private static IEnumerable<Claim> GenerateClaims(AuthenticationStateInfo authenticationStateInfo) =>
		[
			new Claim(ClaimTypes.Name, authenticationStateInfo.UserName),
			new Claim(ClaimTypes.Role, authenticationStateInfo.RoleName)
		];
}