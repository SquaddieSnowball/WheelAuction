using FluentValidation;
using Microsoft.Extensions.Localization;
using WheelAuction.WebUI.Server.Authentication;
using WheelAuction.WebUI.Server.Components.Account.InputModels;
using WheelAuction.WebUI.Server.Resources.Validation.Validators.Components.Account.InputModels;
using WheelAuction.WebUI.Server.Validation.Extensions;

namespace WheelAuction.WebUI.Server.Validation.Validators.Components.Account.InputModels;

internal class RegisterInputModelValidator : AbstractValidator<RegisterInputModel>
{
	public RegisterInputModelValidator(IStringLocalizer<RegisterInputModelValidator> stringLocalizer)
	{
		RuleFor(registerInputModel => registerInputModel.UserName)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.WithMessage(stringLocalizer[RegisterInputModelValidatorResourceNames.ErrorMessageUserNameEmpty])
			.UserName()
			.WithMessage(stringLocalizer[
				RegisterInputModelValidatorResourceNames.ErrorMessageUserNameInvalid,
				AuthenticationSettings.UserNameMinLength,
				AuthenticationSettings.UserNameMaxLength]);

		RuleFor(registerInputModel => registerInputModel.Password)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.WithMessage(stringLocalizer[RegisterInputModelValidatorResourceNames.ErrorMessagePasswordEmpty])
			.Password()
			.WithMessage(stringLocalizer[
				RegisterInputModelValidatorResourceNames.ErrorMessagePasswordInvalid,
				AuthenticationSettings.PasswordMinLength,
				AuthenticationSettings.PasswordRequiredUniqueCharsCount]);

		RuleFor(registerInputModel => registerInputModel.ConfirmPassword)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.WithMessage(stringLocalizer[RegisterInputModelValidatorResourceNames.ErrorMessageConfirmPasswordEmpty])
			.Equal(registerInputModel => registerInputModel.Password)
			.WithMessage(stringLocalizer[RegisterInputModelValidatorResourceNames.ErrorMessageConfirmPasswordMissmatch]);
	}
}