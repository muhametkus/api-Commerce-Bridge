using CommerceBridge.Application.Common.Models;

namespace CommerceBridge.Application.Common.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentGatewayResult> CreatePaymentAsync(
        PaymentGatewayRequest request,
        CancellationToken cancellationToken);
}