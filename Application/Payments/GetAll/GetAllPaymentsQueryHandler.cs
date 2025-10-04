using Application.Payments.Common;
using Domain.Customers;
using Domain.Payments;
using ErrorOr;
using MediatR;

namespace Application.Payments.GetAll
{
    public sealed class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, ErrorOr<IReadOnlyList<PaymentResponse>>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetAllPaymentsQueryHandler(IPaymentRepository paymentRepository, ICustomerRepository customerRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<PaymentResponse>>> Handle(GetAllPaymentsQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<Payment> payments = await _paymentRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();

            var paymentResponse = payments.Select(payment =>
            {
                var customer = customers.FirstOrDefault(x => x.CustomerId == payment.CustomerId);
                return new PaymentResponse(
                    payment.PaymentId.Value,
                    payment.BillingPeriod.ToString("MMMM"),
                    payment.PaymentDate,
                    payment.Amount.Value,
                    payment.PaymentMethod.ToString(),
                    payment.InvoiceNumber,
                    customer?.FirstName + " " + customer?.LastName);
            }).ToList();

            return paymentResponse;
        }
    }
}
