using FluentValidation;
using WheelAuction.WebUI.Client.Authentication.Enumerations;
using WheelAuction.WebUI.Server.Authentication;

namespace WheelAuction.WebUI.Server.Validation.Extensions;

internal static class FluentValidationRuleBuilderExtensions
{
	public static IRuleBuilderOptions<T, string?> UserName<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
		ruleBuilder.Must(str =>
		{
			if (str is null)
				return true;

			if (!IsValidUserNameLength(str))
				return false;

			if (!IsValidUserNameCharacters(str))
				return false;

			return true;
		});

	public static IRuleBuilderOptions<T, string?> Password<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
		ruleBuilder.Must(str =>
		{
			if (str is null)
				return true;

			if (!IsValidPasswordLength(str))
				return false;

			if (!IsValidPasswordCharacters(str))
				return false;

			return true;
		});

	public static IRuleBuilderOptions<T, string?> RoleName<T>(this IRuleBuilder<T, string?> ruleBuilder) =>
		ruleBuilder.IsEnumName(typeof(RoleName));

	private static bool IsValidUserNameLength(string userName) =>
		(userName.Length >= AuthenticationSettings.UserNameMinLength) &&
		(userName.Length <= AuthenticationSettings.UserNameMaxLength);

	private static bool IsValidUserNameCharacters(string userName) =>
		!userName.Any(c => !AuthenticationSettings.UserNameAllowedCharacters.Contains(c));

	private static bool IsValidPasswordLength(string password) =>
		password.Length >= AuthenticationSettings.PasswordMinLength;

	private static bool IsValidPasswordCharacters(string password)
	{
		if (password.Distinct().Count() < AuthenticationSettings.PasswordRequiredUniqueCharsCount)
			return false;

		if (AuthenticationSettings.PasswordRequireNonAlphanumericChar && !password.Any(c => !char.IsLetterOrDigit(c)))
			return false;

		if (AuthenticationSettings.PasswordRequireLowercaseChar && !password.Any(char.IsLower))
			return false;

		if (AuthenticationSettings.PasswordRequireUppercaseChar && !password.Any(char.IsUpper))
			return false;

		if (AuthenticationSettings.PasswordRequireDigit && !password.Any(char.IsDigit))
			return false;

		return true;
	}
}