using MediatR;

namespace CommerceBridge.Application.Features.Orders.Commands.CancelOrder;

public sealed record CancelOrderCommand(Guid Id)
    : IRequest<bool>;