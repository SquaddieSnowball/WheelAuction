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
				identityOptions.User.AllowedUserNameCharacters = AuthenticationSettings.UserName.AllowedCharacters;

				identityOptions.Password.RequiredLength = AuthenticationSettings.Password.MinLength;
				identityOptions.Password.RequiredUniqueChars = AuthenticationSettings.Password.RequiredUniqueCharsCount;
				identityOptions.Password.RequireLowercase = AuthenticationSettings.Password.RequireLowercaseChar;
				identityOptions.Password.RequireUppercase = AuthenticationSettings.Password.RequireUppercaseChar;
				identityOptions.Password.RequireDigit = AuthenticationSettings.Password.RequireDigit;
				identityOptions.Password.RequireNonAlphanumeric = AuthenticationSettings.Password.RequireNonAlphanumericChar;

				identityOptions.SignIn.RequireConfirmedAccount = AuthenticationSettings.SignIn.RequireConfirmedAccount;
				identityOptions.SignIn.RequireConfirmedEmail = AuthenticationSettings.SignIn.RequireConfirmedEmail;
				identityOptions.SignIn.RequireConfirmedPhoneNumber = AuthenticationSettings.SignIn.RequireConfirmedPhoneNumber;

				identityOptions.Lockout.AllowedForNewUsers = AuthenticationSettings.Lockout.AllowedForNewUsers;
				identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(AuthenticationSettings.Lockout.TimeMinutes);
				identityOptions.Lockout.MaxFailedAccessAttempts = AuthenticationSettings.Lockout.MaxFailedAccessAttempts;
			})
			.AddEntityFrameworkStores<IdentityDbContext>();

		return services;
	}
}