using Microsoft.Extensions.DependencyInjection;

namespace WheelAuction.Application.Extensions;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services) => services;
}