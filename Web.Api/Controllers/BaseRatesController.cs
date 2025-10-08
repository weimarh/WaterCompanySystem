using Application.BaseRates.Create;
using Application.BaseRates.Delete;
using Application.BaseRates.GetAll;
using Application.BaseRates.GetById;
using Application.BaseRates.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("baserates")]
    public class BaseRatesController : ApiController
    {
        private readonly IMediator _mediator;

        public BaseRatesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baseRates = await _mediator.Send(new GetAllBaseRatesQuery());

            return baseRates.Match(
                baseRates => Ok(baseRates), 
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var baseRate = await _mediator.Send(new GetBaseRateByIdQuery(id));

            return baseRate.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBaseRate([FromBody] CreateBaseRateCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBaseRate(Guid id, [FromBody] UpdateBaseRateCommand command)
        {
            if (command.BaseRateId != id)
            {
                List<Error> errors = new List<Error>
                {
                    BaseRateErrors.BaseRateNotFound
                };

                return Problem(string.Join(", ", errors.Select(e => e.Description)));
            }

            var result = await _mediator.Send(command);

            return result.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaseRate(Guid id)
        {
            var result = await _mediator.Send(new DeleteBaseRateCommand(id));

            return result.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
