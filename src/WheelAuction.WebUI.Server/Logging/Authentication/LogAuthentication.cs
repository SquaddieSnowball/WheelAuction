namespace WheelAuction.WebUI.Server.Logging.Authentication;

internal static partial class LogAuthentication
{
	[LoggerMessage(
		EventId = 1,
		EventName = nameof(UserLoggedIn),
		Level = LogLevel.Debug,
		Message = "User logged in: '{UserName}'")]
	public static partial void UserLoggedIn(ILogger logger, string userName);

	[LoggerMessage(
		EventId = 2,
		EventName = nameof(LoginAttemptFailed),
		Level = LogLevel.Debug,
		Message = "Failed to log in user: '{UserName}'")]
	public static partial void LoginAttemptFailed(ILogger logger, string userName);

	[LoggerMessage(
		EventId = 3,
		EventName = nameof(UserLoggedOut),
		Level = LogLevel.Debug,
		Message = "User logged out: '{UserName}'")]
	public static partial void UserLoggedOut(ILogger logger, string userName);

	[LoggerMessage(
		EventId = 11,
		EventName = nameof(UserRegistered),
		Level = LogLevel.Information,
		Message = "New user registered: '{UserName}'")]
	public static partial void UserRegistered(ILogger logger, string userName);

	[LoggerMessage(
		EventId = 12,
		EventName = nameof(RegistrationAttemptFailed),
		Level = LogLevel.Debug,
		Message = "Failed to register new user. Error codes: '{ErrorCodes}'")]
	public static partial void RegistrationAttemptFailed(ILogger logger, string errorCodes);

	[LoggerMessage(
		EventId = 13,
		EventName = nameof(UserRoleAssignmentFailed),
		Level = LogLevel.Error,
		Message = "Failed to assign role to user named '{UserName}'. The user has been deleted. Error codes: '{ErrorCodes}'")]
	public static partial void UserRoleAssignmentFailed(ILogger logger, string userName, string errorCodes);
}