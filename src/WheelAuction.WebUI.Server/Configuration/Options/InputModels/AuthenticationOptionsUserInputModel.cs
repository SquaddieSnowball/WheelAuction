namespace WheelAuction.WebUI.Server.Configuration.Options.InputModels;

internal class AuthenticationOptionsUserInputModel
{
	public string UserName { get; set; } = default!;

	public string Password { get; set; } = default!;

	public string? RoleName { get; set; }
}