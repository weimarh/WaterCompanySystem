using FluentValidation;

namespace Application.RatesPerCubicMeter.Delete
{
    public class DeleteRatePerCubicMeterCommandValidator : AbstractValidator<DeleteRatePerCubicMeterCommand>
    {
        public DeleteRatePerCubicMeterCommandValidator()
        {
            RuleFor(r => r.RatePerCubicMeterId)
                .NotEmpty();
        }
    }
}
