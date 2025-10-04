using FluentValidation;

namespace Application.Readings.Create
{
    public class CreateReadingCommandValidator : AbstractValidator<CreateReadingCommand>
    {
        public CreateReadingCommandValidator()
        {
            RuleFor(r => r.ReadingValue)
                .NotEmpty()
                .MaximumLength(7)
                .MinimumLength(1);

            RuleFor(r => r.WaterMeterId)
                .NotEmpty();
        }
    }
}
