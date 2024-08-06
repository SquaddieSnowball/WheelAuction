using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using WheelAuction.WebUI.Server.Localization.Extensions;
using WheelAuction.WebUI.Server.Resources.Validation.Services;

namespace WheelAuction.WebUI.Server.Validation.Services;

internal class FluentValidateOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly IStringLocalizer _stringLocalizer;
	private readonly string? _name;

	public FluentValidateOptions(
		IServiceScopeFactory serviceScopeFactory,
		IStringLocalizerFactory stringLocalizerFactory,
		string? name)
	{
		(_serviceScopeFactory, _stringLocalizer, _name) = (
			serviceScopeFactory,
			stringLocalizerFactory.CreateForGenericService(typeof(FluentValidateOptions<>))!,
			name);
	}

	public ValidateOptionsResult Validate(string? name, TOptions options)
	{
		if ((_name is not null) && (_name != name))
			return ValidateOptionsResult.Skip;

		using var scope = _serviceScopeFactory.CreateScope();
		IValidator<TOptions> validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

		ValidationResult validationResult = validator.Validate(options);

		if (validationResult.IsValid)
			return ValidateOptionsResult.Success;

		string optionsTypeName = typeof(TOptions).Name;
		List<string> validationFailureErrorMessages = [];

		foreach (ValidationFailure validationFailure in validationResult.Errors)
		{
			validationFailureErrorMessages.Add(_stringLocalizer[
				FluentValidateOptionsResourceNames.ErrorMessageValidationFailure,
				optionsTypeName,
				validationFailure.PropertyName,
				validationFailure.ErrorMessage]);
		}

		return ValidateOptionsResult.Fail(validationFailureErrorMessages);
	}
}