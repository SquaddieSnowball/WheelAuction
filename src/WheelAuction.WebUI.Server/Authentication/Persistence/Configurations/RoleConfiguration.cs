using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelAuction.WebUI.Server.Authentication.Entities;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> roleBuilder)
	{
		roleBuilder.ToTable(EntityDbTableNames.Role);
		roleBuilder.HasIndex(role => role.NormalizedName).HasDatabaseName(default).IsUnique();
	}
}