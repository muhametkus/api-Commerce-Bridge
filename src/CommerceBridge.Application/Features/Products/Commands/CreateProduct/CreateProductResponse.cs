namespace CommerceBridge.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    bool IsActive,
    DateTime CreatedAt
);