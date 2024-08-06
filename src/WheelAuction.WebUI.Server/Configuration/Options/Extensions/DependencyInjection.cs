namespace WheelAuction.WebUI.Server.Configuration.Options.Extensions;

internal static class DependencyInjection
{
	public static IServiceCollection AddConfiguredOptions(this IServiceCollection services)
	{
		services
			.AddOptionsWithFluentValidation<AuthenticationOptions>(AuthenticationOptions.ConfigurationSectionName);

		return services;
	}

	public static IServiceCollection AddOptionsWithFluentValidation<TOptions>(
		this IServiceCollection services,
		string configurationSectionName)
		where TOptions : class
	{
		services
			.AddOptions<TOptions>()
			.BindConfiguration(configurationSectionName)
			.ValidateFluentValidation();

		return services;
	}
}