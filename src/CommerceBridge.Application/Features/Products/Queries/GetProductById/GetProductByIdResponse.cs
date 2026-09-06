namespace CommerceBridge.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);