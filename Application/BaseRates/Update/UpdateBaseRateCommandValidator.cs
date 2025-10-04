using FluentValidation;

namespace Application.BaseRates.Update
{
    public class UpdateBaseRateCommandValidator : AbstractValidator<UpdateBaseRateCommand>
    {
        public UpdateBaseRateCommandValidator()
        {
            RuleFor(b => b.BaseRateId)
                .NotEmpty();
        }
    }
}
