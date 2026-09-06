using CommerceBridge.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse?>
{
    private readonly IApplicationDbContext _context;

    public GetProductByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductByIdResponse?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new GetProductByIdResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price,
                x.Stock,
                x.IsActive,
                x.CreatedAt,
                x.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}