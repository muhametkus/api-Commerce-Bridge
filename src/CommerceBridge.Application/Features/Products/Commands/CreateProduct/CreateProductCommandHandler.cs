using CommerceBridge.Application.Common.Interfaces;
using CommerceBridge.Domain.Entities;
using MediatR;

namespace CommerceBridge.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateProductResponse> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock
        };

        await _context.Products.AddAsync(
            product,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        return new CreateProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.IsActive,
            product.CreatedAt);
    }
}