namespace WheelAuction.WebUI.Server.Authentication.Services.Abstractions;

internal interface IAuthenticationPersistenceConfigurator
{
	Task ConfigureAsync();
}