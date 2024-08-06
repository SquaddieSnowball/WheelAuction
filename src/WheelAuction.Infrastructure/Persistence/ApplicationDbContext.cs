using Microsoft.EntityFrameworkCore;

namespace WheelAuction.Infrastructure.Persistence;

internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
	: DbContext(dbContextOptions)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(DbContextSettings.DefaultSchema);
	}
}