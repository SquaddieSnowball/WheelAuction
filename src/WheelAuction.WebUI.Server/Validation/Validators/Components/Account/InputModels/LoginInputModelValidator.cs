using FluentValidation;
using Microsoft.Extensions.Localization;
using WheelAuction.WebUI.Server.Components.Account.InputModels;
using WheelAuction.WebUI.Server.Resources.Validation.Validators.Components.Account.InputModels;

namespace WheelAuction.WebUI.Server.Validation.Validators.Components.Account.InputModels;

internal class LoginInputModelValidator : AbstractValidator<LoginInputModel>
{
	public LoginInputModelValidator(IStringLocalizer<LoginInputModelValidator> stringLocalizer)
	{
		RuleFor(loginInputModel => loginInputModel.UserName)
			.NotEmpty()
			.WithMessage(stringLocalizer[LoginInputModelValidatorResourceNames.ErrorMessageUserNameEmpty]);

		RuleFor(loginInputModel => loginInputModel.Password)
			.NotEmpty()
			.WithMessage(stringLocalizer[LoginInputModelValidatorResourceNames.ErrorMessagePasswordEmpty]);
	}
}