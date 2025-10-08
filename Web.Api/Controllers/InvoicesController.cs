using Application.Invoices.GetAll;
using Application.Invoices.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("invoices")]
    public class InvoicesController : ApiController
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _mediator.Send(new GetAllInvoicesQuery());

            return invoices.Match(
                invoices => Ok(invoices),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery(id));

            return invoice.Match(
                invoice => Ok(invoice),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
