using CommerceBridge.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, UpdateProductResponse?>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateProductResponse?> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (product is null)
            return null;

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.IsActive = request.IsActive;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.IsActive,
            product.CreatedAt,
            product.UpdatedAt);
    }
}