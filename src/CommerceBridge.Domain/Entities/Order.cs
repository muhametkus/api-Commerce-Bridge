using CommerceBridge.Domain.Common;
using CommerceBridge.Domain.Enums;

namespace CommerceBridge.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItem> Items { get; set; } = new();
    
    public Payment? Payment { get; set; }

}