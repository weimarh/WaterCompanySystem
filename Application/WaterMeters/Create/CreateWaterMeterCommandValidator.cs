using FluentValidation;

namespace Application.WaterMeters.Create
{
    public class CreateWaterMeterCommandValidator : AbstractValidator<CreateWaterMeterCommand>
    {
        public CreateWaterMeterCommandValidator()
        {
            RuleFor(w => w.Model)
                .NotEmpty()
                .MinimumLength(3)
                .MinimumLength(15);

            RuleFor(w => w.InstallationDate)
                .NotEmpty();

            RuleFor(w => w.ServiceAddressId)
                .NotEmpty();

            RuleFor(w => w.CustomerId)
                .NotEmpty();
        }
    }
}
