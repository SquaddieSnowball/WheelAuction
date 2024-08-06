using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using WheelAuction.WebUI.Server.Validation.Services;

namespace WheelAuction.WebUI.Server.Configuration.Options.Extensions;

internal static class OptionsBuilderExtensions
{
	public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
		where TOptions : class
	{
		optionsBuilder.Services
			.AddSingleton<IValidateOptions<TOptions>>(serviceProvider =>
				new FluentValidateOptions<TOptions>(
					serviceProvider.GetRequiredService<IServiceScopeFactory>(),
					serviceProvider.GetRequiredService<IStringLocalizerFactory>(),
					optionsBuilder.Name));

		return optionsBuilder;
	}
}