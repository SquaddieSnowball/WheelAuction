namespace WheelAuction.WebUI.Client.Services.Abstractions;

public interface IRedirectManager
{
	public void RedirectTo(string? uri, bool forceLoad = false);

	public void RedirectTo(string? uri, IEnumerable<KeyValuePair<string, object?>> queryParameters, bool forceLoad = false);
}