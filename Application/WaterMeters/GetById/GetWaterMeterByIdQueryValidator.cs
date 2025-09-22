using FluentValidation;

namespace Application.WaterMeters.GetById
{
    public class GetWaterMeterByIdQueryValidator : AbstractValidator<GetWaterMeterByIdQuery>
    {
        public GetWaterMeterByIdQueryValidator()
        {
            RuleFor(w => w.WaterMeterId)
                .NotEmpty();
        }
    }
}
