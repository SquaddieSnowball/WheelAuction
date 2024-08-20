using Microsoft.EntityFrameworkCore;
using WheelAuction.Infrastructure.Shared;

namespace WheelAuction.Infrastructure.Persistence;

internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
	: DbContext(dbContextOptions)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(DbContextSettings.DefaultSchema);
		modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
	}
}