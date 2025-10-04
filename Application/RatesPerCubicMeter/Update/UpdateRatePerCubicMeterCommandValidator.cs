using FluentValidation;

namespace Application.RatesPerCubicMeter.Update
{
    public class UpdateRatePerCubicMeterCommandValidator : AbstractValidator<UpdateRatePerCubicMeterCommand>
    {
        public UpdateRatePerCubicMeterCommandValidator()
        {
            RuleFor(r => r.RatePerCubicMeterId)
                .NotEmpty();

            RuleFor(r => r.Amount)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(5);

            RuleFor(r => r.CreationDate)
                .NotEmpty();
        }
    }
}
