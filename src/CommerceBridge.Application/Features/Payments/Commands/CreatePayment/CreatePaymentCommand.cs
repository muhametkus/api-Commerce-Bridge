using MediatR;

namespace CommerceBridge.Application.Features.Payments.Commands.CreatePayment;

public sealed record CreatePaymentCommand(
    Guid OrderId
) : IRequest<CreatePaymentResponse>;