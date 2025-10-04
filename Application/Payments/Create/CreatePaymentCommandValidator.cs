using FluentValidation;

namespace Application.Payments.Create
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(p => p.InvoiceNumber)
                .NotEmpty();

            RuleFor(p => p.PaymentMethod)
                .NotEmpty();
        }
    }
}
