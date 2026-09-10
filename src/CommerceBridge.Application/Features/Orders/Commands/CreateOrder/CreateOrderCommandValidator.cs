using FluentValidation;

namespace CommerceBridge.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandValidator
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Order must contain at least one item.");

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .NotEmpty();

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0);
            });
    }
}