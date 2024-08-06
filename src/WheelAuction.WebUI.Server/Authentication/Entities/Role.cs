using Microsoft.AspNetCore.Identity;

namespace WheelAuction.WebUI.Server.Authentication.Entities;

internal class Role : IdentityRole
{
	public static Role Create(string name) => new()
	{
		Name = name
	};
}