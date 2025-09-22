using FluentValidation;

namespace Application.ServiceAddresses.Delete
{
    public class DeleteServiceAddressCommandValidator : AbstractValidator<DeleteServiceAddressCommand>
    {
        public DeleteServiceAddressCommandValidator()
        {
            RuleFor(s => s.ServiceAddressId)
                .NotEmpty();
        }
    }
}
