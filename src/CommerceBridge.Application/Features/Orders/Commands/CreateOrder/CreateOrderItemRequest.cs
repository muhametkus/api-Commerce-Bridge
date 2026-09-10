namespace CommerceBridge.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderItemRequest(
    Guid ProductId,
    int Quantity
);