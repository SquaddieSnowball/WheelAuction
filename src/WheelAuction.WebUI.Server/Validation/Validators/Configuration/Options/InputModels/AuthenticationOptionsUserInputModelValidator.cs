using FluentValidation;
using Microsoft.Extensions.Localization;
using WheelAuction.WebUI.Server.Authentication;
using WheelAuction.WebUI.Server.Configuration.Options.InputModels;
using WheelAuction.WebUI.Server.Resources.Validation.Validators.Configuration.Options.InputModels;
using WheelAuction.WebUI.Server.Validation.Extensions;

namespace WheelAuction.WebUI.Server.Validation.Validators.Configuration.Options.InputModels;

internal class AuthenticationOptionsUserInputModelValidator : AbstractValidator<AuthenticationOptionsUserInputModel>
{
	public AuthenticationOptionsUserInputModelValidator(IStringLocalizer<AuthenticationOptionsUserInputModelValidator> stringLocalizer)
	{
		RuleFor(authenticationOptionsUserInputModel => authenticationOptionsUserInputModel.UserName)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.WithMessage(stringLocalizer[AuthenticationOptionsUserInputModelValidatorResourceNames.ErrorMessageUserNameEmpty])
			.UserName()
			.WithMessage(stringLocalizer[
				AuthenticationOptionsUserInputModelValidatorResourceNames.ErrorMessageUserNameInvalid,
				AuthenticationSettings.UserName.MinLength,
				AuthenticationSettings.UserName.MaxLength]);

		RuleFor(authenticationOptionsUserInputModel => authenticationOptionsUserInputModel.Password)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.WithMessage(stringLocalizer[AuthenticationOptionsUserInputModelValidatorResourceNames.ErrorMessagePasswordEmpty])
			.Password()
			.WithMessage(stringLocalizer[
				AuthenticationOptionsUserInputModelValidatorResourceNames.ErrorMessagePasswordInvalid,
				AuthenticationSettings.Password.MinLength,
				AuthenticationSettings.Password.RequiredUniqueCharsCount]);

		RuleFor(authenticationOptionsUserInputModel => authenticationOptionsUserInputModel.RoleName)
			.RoleName()
			.WithMessage(stringLocalizer[AuthenticationOptionsUserInputModelValidatorResourceNames.ErrorMessageRoleNameInvalid]);
	}
}