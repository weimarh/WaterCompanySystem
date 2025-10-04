using FluentValidation;

namespace Application.Readings.Delete
{
    public class DeleteReadingCommandValidator : AbstractValidator<DeleteReadingCommand>
    {
        public DeleteReadingCommandValidator()
        {
            RuleFor(r => r.ReadingId)
                .NotEmpty();
        }
    }
}
