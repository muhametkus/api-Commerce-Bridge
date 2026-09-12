namespace CommerceBridge.Application.Features.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdResponse(
    Guid Id,
    string OrderNumber,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    List<GetOrderByIdItemResponse> Items
);

public sealed record GetOrderByIdItemResponse(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice
);