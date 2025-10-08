using Application.RatesPerCubicMeter.Create;
using Application.RatesPerCubicMeter.Delete;
using Application.RatesPerCubicMeter.GetAll;
using Application.RatesPerCubicMeter.GetById;
using Application.RatesPerCubicMeter.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("rates")]
    public class RatesPerCubicMeterRepository : ApiController
    {
        private readonly IMediator _mediator;

        public RatesPerCubicMeterRepository(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rates = await _mediator.Send(new GatAllRatePerCubicMeterQuery());

            return rates.Match(
                rates => Ok(rates),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rate = await _mediator.Send(new GetRatePerCubicMeterByIdQuery(id));

            return rate.Match(
                rate => Ok(rate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRatePerCubicMeter([FromBody] CreateRatePerCubicMeterCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                rate => Ok(rate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRatePerCubicMeter(Guid id, [FromBody] UpdateRatePerCubicMeterCommand command)
        {
            if (command.RatePerCubicMeterId != id)
            {
                List<Error> errors = new List<Error>
                {
                    RatePerCubicMeterErrors.RatePerCubicMeterNotFound
                };

                return Problem(string.Join(", ", errors.Select(e => e.Description)));
            }

            var result = await _mediator.Send(command);

            return result.Match(
                rate => Ok(rate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatePerCubicMeter(Guid id)
        {
            var result = await _mediator.Send(new DeleteRatePerCubicMeterCommand(id));

            return result.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
