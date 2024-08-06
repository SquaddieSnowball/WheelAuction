using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WheelAuction.WebUI.Server.Authentication.Entities;
using WheelAuction.WebUI.Server.Shared;

namespace WheelAuction.WebUI.Server.Authentication.Persistence;

internal class IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions)
	: IdentityDbContext<User, Role, string>(dbContextOptions)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(DbContextSettings.DefaultSchema);
		modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
	}
}