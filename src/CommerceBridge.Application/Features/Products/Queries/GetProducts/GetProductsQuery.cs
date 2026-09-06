using MediatR;

namespace CommerceBridge.Application.Features.Products.Queries.GetProducts;

public sealed record GetProductsQuery()
    : IRequest<List<GetProductsResponse>>;