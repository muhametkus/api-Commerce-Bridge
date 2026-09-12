using MediatR;

namespace CommerceBridge.Application.Features.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery
    : IRequest<List<GetOrdersResponse>>;