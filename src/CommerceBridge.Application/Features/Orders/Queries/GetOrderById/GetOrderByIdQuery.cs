using MediatR;

namespace CommerceBridge.Application.Features.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id)
    : IRequest<GetOrderByIdResponse?>;