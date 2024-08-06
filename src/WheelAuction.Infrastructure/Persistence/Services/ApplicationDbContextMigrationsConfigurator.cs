using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WheelAuction.Infrastructure.Persistence.Services.Abstractions;

namespace WheelAuction.Infrastructure.Persistence.Services;

internal class ApplicationDbContextMigrationsConfigurator(IServiceScopeFactory serviceScopeFactory) : IMigrationsConfigurator
{
	private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

	public async Task ApplyAsync()
	{
		await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
		ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		applicationDbContext.Database.Migrate();
	}
}