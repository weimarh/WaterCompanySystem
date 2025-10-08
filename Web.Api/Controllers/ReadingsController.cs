using Application.Readings.Create;
using Application.Readings.Delete;
using Application.Readings.GetAll;
using Application.Readings.GetById;
using Application.Readings.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Api.Controllers
{
    [Route("{readings}")]
    public class ReadingsController : ApiController
    {
        private readonly IMediator _mediator;
        public ReadingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var readings = await _mediator.Send(new GetAllReadingsQuery());

            return readings.Match(
                readings => Ok(readings),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reading = await _mediator.Send(new GetReadingByIdQuery(id));

            return reading.Match(
                reading => Ok(reading),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReading([FromBody] CreateReadingCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReading(Guid id, [FromBody] UpdateReadingCommand command)
        {
            if (command.ReadingId != id)
            {
                List<Error> errors = new List<Error>
                {
                    ReadingErrors.ReadingNotFound
                };

                return Problem(string.Join(", ", errors.Select(e => e.Description)));
            }

            var result = await _mediator.Send(command);

            return result.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReading(Guid id)
        {
            var result = await _mediator.Send(new DeleteReadingCommand(id));

            return result.Match(
                baseRate => Ok(baseRate),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description))));
        }
    }
}
