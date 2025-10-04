using FluentValidation;

namespace Application.RatesPerCubicMeter.GetById
{
    public class GetRatePerCubicMeterByIdQueryValidator : AbstractValidator<GetRatePerCubicMeterByIdQuery>
    {
        public GetRatePerCubicMeterByIdQueryValidator()
        {
            RuleFor(r => r.RatePerCubicMeterId)
                .NotEmpty();
        }
    }
}
