using Microsoft.Extensions.Localization;

namespace WheelAuction.WebUI.Server.Localization.Extensions;

internal static class StringLocalizerFactoryExtensions
{
	public static IStringLocalizer? CreateForGenericService(
		this IStringLocalizerFactory stringLocalizerFactory,
		Type serviceType)
	{
		if (!serviceType.IsGenericType)
			return default;

		string location = serviceType.Assembly.GetName().Name!;
		string baseName = $"{serviceType.Namespace}.{serviceType.Name[..serviceType.Name.IndexOf('`')]}"[(location.Length + 1)..];

		return stringLocalizerFactory.Create(baseName, location);
	}
}