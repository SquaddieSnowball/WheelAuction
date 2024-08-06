namespace WheelAuction.WebUI.Server.Components.Account.InputModels;

internal class RegisterInputModel
{
	public string UserName { get; set; } = default!;

	public string Password { get; set; } = default!;

	public string ConfirmPassword { get; set; } = default!;
}