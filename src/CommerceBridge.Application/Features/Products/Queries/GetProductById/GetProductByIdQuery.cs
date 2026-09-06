using MediatR;

namespace CommerceBridge.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id)
    : IRequest<GetProductByIdResponse?>;