using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> userRoleBuilder)
	{
		userRoleBuilder.ToTable(EntityDbTableNames.UserRole);
	}
}