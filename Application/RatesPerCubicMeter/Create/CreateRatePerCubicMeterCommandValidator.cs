using FluentValidation;

namespace Application.RatesPerCubicMeter.Create
{
    public class CreateRatePerCubicMeterCommandValidator : AbstractValidator<CreateRatePerCubicMeterCommand>
    {
        public CreateRatePerCubicMeterCommandValidator()
        {
            RuleFor(r => r.Amount)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(5);

            RuleFor(r => r.CreationDate)
                .NotEmpty();
        }
    }
}
