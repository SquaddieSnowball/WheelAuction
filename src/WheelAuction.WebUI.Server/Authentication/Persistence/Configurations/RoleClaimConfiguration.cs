using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Configurations;

internal class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
	public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> roleClaimBuilder)
	{
		roleClaimBuilder.ToTable(EntityDbTableNames.RoleClaim);
	}
}