namespace WheelAuction.WebUI.Server.Components.Account.InputModels;

internal class LoginInputModel()
{
	public string UserName { get; set; } = default!;

	public string Password { get; set; } = default!;

	public bool RememberMe { get; set; }
}