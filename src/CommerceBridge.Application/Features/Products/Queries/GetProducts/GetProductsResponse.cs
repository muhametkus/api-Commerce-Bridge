namespace CommerceBridge.Application.Features.Products.Queries.GetProducts;

public sealed record GetProductsResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);