using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using WheelAuction.WebUI.Client.Services.Abstractions;

namespace WheelAuction.WebUI.Client.Services;

public class RedirectManager(NavigationManager navigationManager) : IRedirectManager
{
	private readonly NavigationManager _navigationManager = navigationManager;

	public void RedirectTo(
		[StringSyntax(StringSyntaxAttribute.Uri)] string? uri,
		bool forceLoad = false)
	{
		uri = NormalizeUri(uri);

		_navigationManager.NavigateTo(uri, forceLoad);
	}

	public void RedirectTo(
		[StringSyntax(StringSyntaxAttribute.Uri)] string? uri,
		IEnumerable<KeyValuePair<string, object?>> queryParameters,
		bool forceLoad = false)
	{
		uri = NormalizeUri(uri);
		uri = _navigationManager.GetUriWithQueryParameters(uri, queryParameters.ToDictionary());

		_navigationManager.NavigateTo(uri, forceLoad);
	}

	private string NormalizeUri(string? uri)
	{
		uri ??= string.Empty;

		if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
			uri = _navigationManager.ToBaseRelativePath(uri);

		return uri;
	}
}