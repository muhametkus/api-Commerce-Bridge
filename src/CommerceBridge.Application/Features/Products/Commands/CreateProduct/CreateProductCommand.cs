using MediatR;

namespace CommerceBridge.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price,
    int Stock
) : IRequest<CreateProductResponse>;