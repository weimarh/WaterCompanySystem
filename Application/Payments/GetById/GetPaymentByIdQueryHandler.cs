using Application.Payments.Common;
using Domain.Customers;
using Domain.DomainErrors;
using Domain.Payments;
using ErrorOr;
using MediatR;

namespace Application.Payments.GetById
{
    public sealed class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, ErrorOr<PaymentResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetPaymentByIdQueryHandler(ICustomerRepository customerRepository, IPaymentRepository paymentRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        public async Task<ErrorOr<PaymentResponse>> Handle(GetPaymentByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _paymentRepository.GetByIdAsync(new PaymentId(query.PaymentId)) is not Payment payment)
                return PaymentErrors.PaymentNotFound;

            if (await _customerRepository.GetByIdAsync(payment.CustomerId) is not Customer customer)
                return PaymentErrors.CustomerNotFound;

            var response = new PaymentResponse(
                payment.PaymentId.Value,
                payment.BillingPeriod.ToString("MMM"),
                payment.PaymentDate,
                payment.Amount.Value,
                payment.PaymentMethod.ToString(),
                payment.InvoiceNumber,
                customer?.FirstName + " " + customer?.LastName
            );

            return response;
        }
    }
}
