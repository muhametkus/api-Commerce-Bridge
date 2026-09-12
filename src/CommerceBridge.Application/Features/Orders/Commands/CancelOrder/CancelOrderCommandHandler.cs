using CommerceBridge.Application.Common.Interfaces;
using CommerceBridge.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Orders.Commands.CancelOrder;

public sealed class CancelOrderCommandHandler
    : IRequestHandler<CancelOrderCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CancelOrderCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        CancelOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (order is null)
            return false;

        if (order.Status == OrderStatus.Cancelled)
            return true;

        order.Status = OrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}