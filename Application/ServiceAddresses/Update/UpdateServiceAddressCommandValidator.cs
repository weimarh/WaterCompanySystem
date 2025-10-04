using FluentValidation;

namespace Application.ServiceAddresses.Update
{
    public class UpdateServiceAddressCommandValidator : AbstractValidator<UpdateServiceAddressCommand>
    {
        public UpdateServiceAddressCommandValidator()
        {
            RuleFor(s => s.ServiceAddressId)
                .NotEmpty();

            RuleFor(s => s.StreetName)
                .NotEmpty();

            RuleFor(s => s.HouseNumber)
                .NotEmpty();

            RuleFor(s => s.RatePlan)
                .NotEmpty();
        }
    }
}
