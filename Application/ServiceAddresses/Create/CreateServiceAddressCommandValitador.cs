using FluentValidation;

namespace Application.ServiceAddresses.Create
{
    public class CreateServiceAddressCommandValitador : AbstractValidator<CreateServiceAddressCommand>
    {
        public CreateServiceAddressCommandValitador()
        {
            RuleFor(s => s.StreetName)
                .NotEmpty();

            RuleFor(s => s.HouseNumber)
                .NotEmpty();

            RuleFor(s => s.RatePlan)
                .NotEmpty();
        }
    }
}
