using FluentValidation;
using Microsoft.Extensions.Localization;
using WheelAuction.WebUI.Server.Configuration.Options;
using WheelAuction.WebUI.Server.Configuration.Options.InputModels;
using WheelAuction.WebUI.Server.Resources.Validation.Validators.Configuration.Options;

namespace WheelAuction.WebUI.Server.Validation.Validators.Configuration.Options;

internal class AuthenticationOptionsValidator : AbstractValidator<AuthenticationOptions>
{
	public AuthenticationOptionsValidator(
		IStringLocalizer<AuthenticationOptionsValidator> stringLocalizer,
		IValidator<AuthenticationOptionsUserInputModel> authenticationOptionsUserInputModelValidator)
	{
		RuleFor(options => options.MainUser)
			.Cascade(CascadeMode.Stop)
			.NotNull()
			.WithMessage(stringLocalizer[AuthenticationOptionsValidatorResourceNames.ErrorMessageMainUserNull])
			.SetValidator(authenticationOptionsUserInputModelValidator);

		RuleForEach(options => options.AdditionalUsers)
			.SetValidator(authenticationOptionsUserInputModelValidator);
	}
}