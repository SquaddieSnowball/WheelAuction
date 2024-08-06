using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WheelAuction.WebUI.Server.Authentication.Entities;

namespace WheelAuction.WebUI.Server.Authentication.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> userBuilder)
	{
		userBuilder.ToTable(EntityDbTableNames.User);
		userBuilder.HasIndex(user => user.NormalizedUserName).HasDatabaseName(default).IsUnique();
		userBuilder.HasIndex(user => user.NormalizedEmail).HasDatabaseName(default);
	}
}