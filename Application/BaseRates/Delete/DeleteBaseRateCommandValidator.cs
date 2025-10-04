using FluentValidation;

namespace Application.BaseRates.Delete
{
    public class DeleteBaseRateCommandValidator : AbstractValidator<DeleteBaseRateCommand>
    {
        public DeleteBaseRateCommandValidator()
        {
            RuleFor(b => b.BaseRateId)
                .NotEmpty();
        }
    }
}
