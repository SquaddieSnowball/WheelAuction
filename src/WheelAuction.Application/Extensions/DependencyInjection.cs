using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WheelAuction.Application.Shared;

namespace WheelAuction.Application.Extensions;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services
			.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssembly(AssemblyReference.Assembly);
			});

		services
			.AddValidatorsFromAssembly(
				AssemblyReference.Assembly,
				includeInternalTypes: true);

		return services;
	}
}