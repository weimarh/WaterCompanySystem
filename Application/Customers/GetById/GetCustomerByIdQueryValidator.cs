using FluentValidation;

namespace Application.Customers.GetById
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty();
        }
    }
}
