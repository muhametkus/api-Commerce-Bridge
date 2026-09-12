using CommerceBridge.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse?>
{
    private readonly IApplicationDbContext _context;

    public GetOrderByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetOrderByIdResponse?> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetOrderByIdResponse(
                x.Id,
                x.OrderNumber,
                x.TotalAmount,
                x.Status.ToString(),
                x.CreatedAt,
                x.UpdatedAt,
                x.Items
                    .Select(item => new GetOrderByIdItemResponse(
                        item.ProductId,
                        item.ProductName,
                        item.UnitPrice,
                        item.Quantity,
                        item.TotalPrice))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}