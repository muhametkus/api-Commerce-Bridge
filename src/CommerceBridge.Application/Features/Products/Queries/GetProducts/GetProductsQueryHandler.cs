using CommerceBridge.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, List<GetProductsResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetProductsResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new GetProductsResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price,
                x.Stock,
                x.IsActive,
                x.CreatedAt,
                x.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}