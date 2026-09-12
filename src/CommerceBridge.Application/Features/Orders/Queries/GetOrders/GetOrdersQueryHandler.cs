using CommerceBridge.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Orders.Queries.GetOrders;

public sealed class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQuery, List<GetOrdersResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetOrdersResponse>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new GetOrdersResponse(
                x.Id,
                x.OrderNumber,
                x.TotalAmount,
                x.Status.ToString(),
                x.Items.Count,
                x.CreatedAt,
                x.UpdatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}