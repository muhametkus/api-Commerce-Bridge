using FluentValidation;

namespace CommerceBridge.Application.Features.Payments.Commands.CreatePayment;

public sealed class CreatePaymentCommandValidator
    : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
        RuleFor(x => x.CustomerName)
            .NotEmpty();

        RuleFor(x => x.CustomerSurname)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}