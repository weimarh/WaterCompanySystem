using FluentValidation;

namespace Application.BaseRates.GetById
{
    public class GetBaseRateByIdQueryValidator : AbstractValidator<GetBaseRateByIdQuery>
    {
        public GetBaseRateByIdQueryValidator()
        {
            RuleFor(b => b.BaseRateId)
                .NotEmpty();
        }
    }
}
