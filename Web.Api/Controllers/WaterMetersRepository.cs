using Application.WaterMeters.Create;
using Application.WaterMeters.Delete;
using Application.WaterMeters.GetAll;
using Application.WaterMeters.GetById;
using Application.WaterMeters.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Api.Controllers
{
    [Route("meters")]
    public class WaterMetersRepository : ApiController
    {
        private readonly IMediator _mediator;

        public WaterMetersRepository(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meters = await _mediator.Send(new GetAllWaterMetersQuery());

            return meters.Match(
                meters => Ok(meters),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var meter = await _mediator.Send(new GetWaterMeterByIdQuery(id));

            return meter.Match(
                meter => Ok(meter),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> CreateWaterMeter([FromBody] CreateWaterMeterCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                meter => Ok(meter),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWaterMeter(Guid id, [FromBody] UpdateWaterMeterCommand command)
        {
            if (command.WaterMeterId != id)
            {
                List<Error> errors = new List<Error>
                {
                    WaterMeterErrors.WaterMeterNotFound
                };

                return Problem(string.Join(", ", errors.Select(e => e.Description)));
            }

            var result = await _mediator.Send(command);

            return result.Match(
                meter => Ok(meter),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaterMeter(Guid id)
        {
            var result = await _mediator.Send(new DeleteWaterMeterCommand(id));

            return result.Match(
                meter => Ok(meter),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
