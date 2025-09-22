using FluentValidation;

namespace Application.WaterMeters.Delete
{
    public class DeleteWaterMeterCommandValidator : AbstractValidator<DeleteWaterMeterCommand>
    {
        public DeleteWaterMeterCommandValidator()
        {
            RuleFor(w => w.WaterMeterId)
                .NotEmpty();
        }
    }
}
