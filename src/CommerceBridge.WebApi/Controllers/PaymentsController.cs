using CommerceBridge.Application.Features.Payments.Commands.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommerceBridge.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PaymentsController : ControllerBase
{
    private readonly ISender _sender;

    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePaymentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            result);
    }
}