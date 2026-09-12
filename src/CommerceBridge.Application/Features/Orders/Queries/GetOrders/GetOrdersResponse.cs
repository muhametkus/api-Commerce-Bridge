namespace CommerceBridge.Application.Features.Orders.Queries.GetOrders;

public sealed record GetOrdersResponse(
    Guid Id,
    string OrderNumber,
    decimal TotalAmount,
    string Status,
    int ItemCount,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);