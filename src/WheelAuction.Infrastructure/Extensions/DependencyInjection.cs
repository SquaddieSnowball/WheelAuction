using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using WheelAuction.Application.Abstractions;
using WheelAuction.Infrastructure.Persistence;
using WheelAuction.Infrastructure.Persistence.Services;
using WheelAuction.Infrastructure.Persistence.Services.Abstractions;

namespace WheelAuction.Infrastructure.Extensions;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string applicationDbConnectionString)
	{
		services
			.AddDbContext<ApplicationDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseNpgsql(applicationDbConnectionString, npgsqlOptionsBuilder =>
				{
					npgsqlOptionsBuilder.MigrationsHistoryTable(
						HistoryRepository.DefaultTableName,
						DbContextSettings.DefaultSchema);
				});
			})
			.AddScoped<IUnitOfWork, ApplicationDbContextUnitOfWork>()
			.AddSingleton<IMigrationsConfigurator, ApplicationDbContextMigrationsConfigurator>();

		return services;
	}
}