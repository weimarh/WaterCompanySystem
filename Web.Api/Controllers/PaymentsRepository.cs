using Application.Payments.Create;
using Application.Payments.Delete;
using Application.Payments.GetAll;
using Application.Payments.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [ApiController]
    public class PaymentsRepository : ApiController
    {
        private readonly IMediator _mediator;

        public PaymentsRepository(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _mediator.Send(new GetAllPaymentsQuery());

            return payments.Match(
                payments => Ok(payments),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _mediator.Send(new GetPaymentByIdQuery(id));

            return payment.Match(
                payment => Ok(payment),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                payment => Ok(payment),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            var payment = await _mediator.Send(new DeletePaymentCommand(id));

            return payment.Match(
                payment => Ok(payment),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
