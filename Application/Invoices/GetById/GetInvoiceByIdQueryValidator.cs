using Application.Invoices.GetAll;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Invoices.GetById
{
    public class GetInvoiceByIdQueryValidator : AbstractValidator<GetInvoiceByIdQuery>
    {
        public GetInvoiceByIdQueryValidator()
        {
            RuleFor(i => i.InvoiceId)
                .NotEmpty();
        }
    }
}
