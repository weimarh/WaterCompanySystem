using FluentValidation;

namespace Application.ServiceAddresses.GetById
{
    public class GetServiceAddressByIdQueryValidator : AbstractValidator<GetServiceAddressByIdQuery>
    {
        public GetServiceAddressByIdQueryValidator()
        {
            RuleFor(s => s.ServiceAddressId)
                .NotEmpty();
        }
    }
}
