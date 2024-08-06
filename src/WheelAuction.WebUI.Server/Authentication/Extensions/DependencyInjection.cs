using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WheelAuction.Infrastructure.Persistence.Services.Abstractions;
using WheelAuction.WebUI.Server.Authentication.Entities;
using WheelAuction.WebUI.Server.Authentication.Persistence;
using WheelAuction.WebUI.Server.Authentication.Persistence.Services;
using WheelAuction.WebUI.Server.Authentication.Services.Abstractions;
using WheelAuction.WebUI.Server.Authentication.Services;

namespace WheelAuction.WebUI.Server.Authentication.Extensions;

internal static class DependencyInjection
{
	public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
	{
		services
			.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>()
			.AddCascadingAuthenticationState()
			.AddSingleton<IAuthenticationPersistenceConfigurator, AuthenticationPersistenceConfigurator>();

		return services;
	}

	public static IServiceCollection AddConfiguredIdentity(this IServiceCollection services, string identityDbConnectionString)
	{
		services
			.AddDbContext<IdentityDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseNpgsql(identityDbConnectionString, npgsqlOptionsBuilder =>
				{
					npgsqlOptionsBuilder.MigrationsHistoryTable(
						HistoryRepository.DefaultTableName,
						DbContextSettings.DefaultSchema);
				});
			})
			.AddSingleton<IMigrationsConfigurator, IdentityDbContextMigrationsConfigurator>();

		services
			.AddIdentity<User, Role>(identityOptions =>
			{
				identityOptions.SignIn.RequireConfirmedAccount = AuthenticationSettings.SignInRequireConfirmedAccount;
				identityOptions.SignIn.RequireConfirmedEmail = AuthenticationSettings.SignInRequireConfirmedEmail;
				identityOptions.SignIn.RequireConfirmedPhoneNumber = AuthenticationSettings.SignInRequireConfirmedPhoneNumber;

				identityOptions.Lockout.AllowedForNewUsers = AuthenticationSettings.LockoutAllowedForNewUsers;
				identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(AuthenticationSettings.LockoutTimeMinutes);
				identityOptions.Lockout.MaxFailedAccessAttempts = AuthenticationSettings.LockoutMaxFailedAccessAttempts;

				identityOptions.User.AllowedUserNameCharacters = AuthenticationSettings.UserNameAllowedCharacters;

				identityOptions.Password.RequiredLength = AuthenticationSettings.PasswordMinLength;
				identityOptions.Password.RequiredUniqueChars = AuthenticationSettings.PasswordRequiredUniqueCharsCount;
				identityOptions.Password.RequireLowercase = AuthenticationSettings.PasswordRequireLowercaseChar;
				identityOptions.Password.RequireUppercase = AuthenticationSettings.PasswordRequireUppercaseChar;
				identityOptions.Password.RequireDigit = AuthenticationSettings.PasswordRequireDigit;
				identityOptions.Password.RequireNonAlphanumeric = AuthenticationSettings.PasswordRequireNonAlphanumericChar;
			})
			.AddEntityFrameworkStores<IdentityDbContext>();

		return services;
	}
}