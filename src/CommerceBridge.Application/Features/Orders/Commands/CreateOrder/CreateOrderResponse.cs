namespace CommerceBridge.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderResponse(
    Guid Id,
    string OrderNumber,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt,
    List<CreateOrderItemResponse> Items
);

public sealed record CreateOrderItemResponse(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice
);