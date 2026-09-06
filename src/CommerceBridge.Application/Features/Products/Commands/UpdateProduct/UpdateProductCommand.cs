using MediatR;

namespace CommerceBridge.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    bool IsActive
) : IRequest<UpdateProductResponse?>;