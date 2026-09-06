namespace CommerceBridge.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);