using FluentValidation;

namespace Application.WaterMeters.Update
{
    public class UpdateWaterMeterCommandValidator : AbstractValidator<UpdateWaterMeterCommand>
    {
        public UpdateWaterMeterCommandValidator()
        {
            RuleFor(x => x.WaterMeterId)
                .NotEmpty();

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
