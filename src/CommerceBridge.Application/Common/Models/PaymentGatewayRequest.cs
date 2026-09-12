namespace CommerceBridge.Application.Common.Models;

public sealed record PaymentGatewayRequest(
    Guid OrderId,
    string OrderNumber,
    decimal Amount
);