using Application.ServiceAddresses.Create;
using Application.ServiceAddresses.Delete;
using Application.ServiceAddresses.GetAll;
using Application.ServiceAddresses.GetById;
using Application.ServiceAddresses.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Api.Controllers
{
    [Route("addresses")]
    public class ServiceAddressesRepository : ApiController
    {
        private readonly IMediator _mediator;

        public ServiceAddressesRepository(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _mediator.Send(new GetAllServiceAddressesQuery());

            return addresses.Match(
                addresses => Ok(addresses),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var address = await _mediator.Send(new GetServiceAddressByIdQuery(id));

            return address.Match(
                address => Ok(address),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateServiceAddressCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                address => Ok(address),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateServiceAddressCommand command)
        {
            if (command.ServiceAddressId != id)
            {
                List<Error> errors = new List<Error>
                {
                    ServiceAddressErrors.ServiceAddressNotFound
                };

                return Problem(string.Join(", ", errors.Select(e => e.Description)));
            }

            var result = await _mediator.Send(command);

            return result.Match(
                address => Ok(address),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var result = await _mediator.Send(new DeleteServiceAddressCommand(id));

            return result.Match(
                address => Ok(address),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
