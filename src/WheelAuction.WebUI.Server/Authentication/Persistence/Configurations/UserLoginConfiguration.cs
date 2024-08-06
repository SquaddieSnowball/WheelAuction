using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Configurations;

internal class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> userLoginBuilder)
	{
		userLoginBuilder.ToTable(EntityDbTableNames.UserLogin);
	}
}