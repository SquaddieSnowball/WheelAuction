namespace WheelAuction.WebUI.Server.Authentication;

internal static class AuthenticationSettings
{
	public static readonly bool SignInRequireConfirmedAccount = false;
	public static readonly bool SignInRequireConfirmedEmail = false;
	public static readonly bool SignInRequireConfirmedPhoneNumber = false;

	public static readonly bool LockoutAllowedForNewUsers = true;
	public static readonly int LockoutTimeMinutes = 5;
	public static readonly int LockoutMaxFailedAccessAttempts = 5;

	public static readonly int UserNameMinLength = 3;
	public static readonly int UserNameMaxLength = 30;
	public static readonly string UserNameAllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

	public static readonly int PasswordMinLength = 8;
	public static readonly int PasswordRequiredUniqueCharsCount = 2;
	public static readonly bool PasswordRequireLowercaseChar = false;
	public static readonly bool PasswordRequireUppercaseChar = false;
	public static readonly bool PasswordRequireDigit = false;
	public static readonly bool PasswordRequireNonAlphanumericChar = false;
}