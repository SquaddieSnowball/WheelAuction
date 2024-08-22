using Microsoft.EntityFrameworkCore;
using WheelAuction.Infrastructure.Persistence.Services.Abstractions;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Services;

internal class IdentityDbContextMigrationsConfigurator(IServiceScopeFactory serviceScopeFactory)
	: IMigrationsConfigurator
{
	private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

	public async Task ApplyAsync()
	{
		await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
		IdentityDbContext identityDbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

		identityDbContext.Database.Migrate();
	}
}