using FluentValidation;

namespace Application.Customers.Update
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotEmpty()
                .MaximumLength(8)
                .MinimumLength(6);

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15);

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(8);
        }
    }
}
