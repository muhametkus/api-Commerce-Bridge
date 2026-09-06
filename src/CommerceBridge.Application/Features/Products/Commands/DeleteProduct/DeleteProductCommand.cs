using MediatR;

namespace CommerceBridge.Application.Features.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id)
    : IRequest<bool>;