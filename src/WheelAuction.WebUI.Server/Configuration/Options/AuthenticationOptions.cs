using WheelAuction.WebUI.Server.Configuration.Options.InputModels;

namespace WheelAuction.WebUI.Server.Configuration.Options;

internal class AuthenticationOptions
{
	public const string ConfigurationSectionName = "Authentication";

	public AuthenticationOptionsUserInputModel MainUser { get; set; } = default!;

	public IEnumerable<AuthenticationOptionsUserInputModel>? AdditionalUsers { get; set; }
}