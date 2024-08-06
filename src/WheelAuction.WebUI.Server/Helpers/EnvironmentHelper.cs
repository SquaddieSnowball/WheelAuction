namespace WheelAuction.WebUI.Server.Helpers;

internal static class EnvironmentHelper
{
	private const string IsContainerEnvironmentVariable = "DOTNET_RUNNING_IN_CONTAINER";

	public static bool IsContainer => Environment.GetEnvironmentVariable(IsContainerEnvironmentVariable) is "true";
}