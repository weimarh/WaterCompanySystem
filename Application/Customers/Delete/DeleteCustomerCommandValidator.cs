using FluentValidation;

namespace Application.Customers.Delete
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty();
        }
    }
}
