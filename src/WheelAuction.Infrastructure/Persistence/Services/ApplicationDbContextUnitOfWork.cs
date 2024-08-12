using WheelAuction.Application.Abstractions;

namespace WheelAuction.Infrastructure.Persistence.Services;

internal class ApplicationDbContextUnitOfWork(ApplicationDbContext applicationDbContext) : IUnitOfWork
{
	private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

	public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
		_applicationDbContext.SaveChangesAsync(cancellationToken);
}