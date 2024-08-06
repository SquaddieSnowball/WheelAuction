using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Configurations;

internal class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserToken<string>> userTokenBuilder)
	{
		userTokenBuilder.ToTable(EntityDbTableNames.UserToken);
	}
}