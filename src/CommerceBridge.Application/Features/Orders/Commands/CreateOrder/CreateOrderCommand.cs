using MediatR;

namespace CommerceBridge.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    List<CreateOrderItemRequest> Items
) : IRequest<CreateOrderResponse>;