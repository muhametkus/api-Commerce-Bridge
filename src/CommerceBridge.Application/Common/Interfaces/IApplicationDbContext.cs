using CommerceBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}