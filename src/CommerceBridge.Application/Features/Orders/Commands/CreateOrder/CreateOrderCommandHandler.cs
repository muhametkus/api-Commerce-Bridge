using CommerceBridge.Application.Common.Interfaces;
using CommerceBridge.Domain.Entities;
using CommerceBridge.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateOrderResponse> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var productIds = request.Items
            .Select(x => x.ProductId)
            .Distinct()
            .ToList();

        var products = await _context.Products
            .Where(x =>
                productIds.Contains(x.Id) &&
                x.IsActive)
            .ToListAsync(cancellationToken);

        if (products.Count != productIds.Count)
            throw new InvalidOperationException(
                "One or more products could not be found.");

        var order = new Order
        {
            OrderNumber = GenerateOrderNumber(),
            Status = OrderStatus.Pending
        };

        foreach (var requestItem in request.Items)
        {
            var product = products
                .First(x => x.Id == requestItem.ProductId);

            if (product.Stock < requestItem.Quantity)
                throw new InvalidOperationException(
                    $"Insufficient stock for product: {product.Name}");

            var totalPrice =
                product.Price * requestItem.Quantity;

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = requestItem.Quantity,
                TotalPrice = totalPrice
            });
        }

        order.TotalAmount = order.Items
            .Sum(x => x.TotalPrice);

        await _context.Orders.AddAsync(
            order,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        return new CreateOrderResponse(
            order.Id,
            order.OrderNumber,
            order.TotalAmount,
            order.Status.ToString(),
            order.CreatedAt,
            order.Items
                .Select(x => new CreateOrderItemResponse(
                    x.ProductId,
                    x.ProductName,
                    x.UnitPrice,
                    x.Quantity,
                    x.TotalPrice))
                .ToList());
    }

    private static string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
    }
}