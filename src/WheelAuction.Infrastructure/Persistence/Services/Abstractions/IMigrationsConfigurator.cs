namespace WheelAuction.Infrastructure.Persistence.Services.Abstractions;

public interface IMigrationsConfigurator
{
	Task ApplyAsync();
}