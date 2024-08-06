using Microsoft.Extensions.DependencyInjection;

namespace WheelAuction.Domain.Extensions;

public static class DependencyInjection
{
	public static IServiceCollection AddDomain(this IServiceCollection services) => services;
}