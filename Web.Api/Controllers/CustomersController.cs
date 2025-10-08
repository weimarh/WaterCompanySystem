using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.GetAll;
using Application.Customers.GetById;
using Application.Customers.Update;
using Domain.Customers;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("customers")]
    public class CustomersController : ApiController
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());

            return customers.Match(
                customers => Ok(customers),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));

            return customer.Match(
                customer => Ok(customer),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                customer => Ok(customer),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] UpdateCustomerCommand command)
        {
            if (command.CustomerId != id)
            {
                List<Error> errors = new List<Error>
                {
                    CustomerErrors.CustomerNotFound
                };

                return Problem(string.Join(", ", errors.Select(e => e.Description)));
            }

            var result = await _mediator.Send(command);

            return result.Match(
                customer => Ok(customer),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));

            return result.Match(
                customer => Ok(customer),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
