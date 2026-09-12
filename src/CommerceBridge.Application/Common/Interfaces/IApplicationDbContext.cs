using CommerceBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Order> Orders { get; }

    DbSet<OrderItem> OrderItems { get; }
    DbSet<Payment> Payments { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}