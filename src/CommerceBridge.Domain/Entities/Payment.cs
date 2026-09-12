using CommerceBridge.Domain.Common;
using CommerceBridge.Domain.Enums;

namespace CommerceBridge.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public string Provider { get; set; } = null!;

    public string? ProviderTransactionId { get; set; }

    public string? FailureReason { get; set; }

    public Order Order { get; set; } = null!;
}