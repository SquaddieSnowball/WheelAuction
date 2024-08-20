using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace WheelAuction.WebUI.Server.Validation.Services;

internal class FluentValidateOptions<TOptions>(
	IServiceScopeFactory serviceScopeFactory,
	string? name)
	: IValidateOptions<TOptions> where TOptions : class
{
	private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
	private readonly string? _name = name;

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
			validationFailureErrorMessages.Add(
				$"Validation failed for '{optionsTypeName}.{validationFailure.PropertyName}' " +
				$"with the error: '{validationFailure.ErrorMessage}'");
		}

		return ValidateOptionsResult.Fail(validationFailureErrorMessages);
	}
}