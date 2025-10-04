using FluentValidation;

namespace Application.BaseRates.Create
{
    public class CreateBaseRateCommandValidator : AbstractValidator<CreateBaseRateCommand>
    {
        public CreateBaseRateCommandValidator()
        {
            RuleFor(b => b.Amount)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(5);

            RuleFor(b => b.CreationDate)
                .NotEmpty();
        }
    }
}
