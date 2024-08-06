using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Configurations;

internal class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> userClaimBuilder)
	{
		userClaimBuilder.ToTable(EntityDbTableNames.UserClaim);
	}
}