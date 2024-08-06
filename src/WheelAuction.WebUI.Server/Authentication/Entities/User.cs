using Microsoft.AspNetCore.Identity;

namespace WheelAuction.WebUI.Server.Authentication.Entities;

internal class User : IdentityUser
{
	public static User Create(string userName) => new()
	{
		UserName = userName
	};
}