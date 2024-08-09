namespace WheelAuction.WebUI.Server.Authentication;

internal static class AuthenticationSettings
{
	internal static class UserName
	{
		public static readonly int MinLength = 3;
		public static readonly int MaxLength = 30;
		public static readonly string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	}

	internal static class Password
	{
		public static readonly int MinLength = 8;
		public static readonly int RequiredUniqueCharsCount = 2;
		public static readonly bool RequireLowercaseChar = false;
		public static readonly bool RequireUppercaseChar = false;
		public static readonly bool RequireDigit = false;
		public static readonly bool RequireNonAlphanumericChar = false;
	}

	internal static class SignIn
	{
		public static readonly bool RequireConfirmedAccount = false;
		public static readonly bool RequireConfirmedEmail = false;
		public static readonly bool RequireConfirmedPhoneNumber = false;
	}

	internal static class Lockout
	{
		public static readonly bool AllowedForNewUsers = true;
		public static readonly int TimeMinutes = 5;
		public static readonly int MaxFailedAccessAttempts = 5;
	}
}