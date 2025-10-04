using Application.Invoices.Common;
using Domain.Customers;
using Domain.DomainErrors;
using Domain.Invoices;
using Domain.ServiceAddresses;
using ErrorOr;
using MediatR;

namespace Application.Invoices.GetById
{
    public sealed class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, ErrorOr<InvoiceResponse>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceAddressRepository _serviceAddressRepository;

        public GetInvoiceByIdQueryHandler(IServiceAddressRepository serviceAddressRepository, ICustomerRepository customerRepository, IInvoiceRepository invoiceRepository)
        {
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        }

        public async Task<ErrorOr<InvoiceResponse>> Handle(GetInvoiceByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _invoiceRepository.GetByIdAsync(new InvoiceId(query.InvoiceId)) is not Invoice invoice)
                return InvoiceErrors.InvoiceNotFound;

            if (await _customerRepository.GetByIdAsync(invoice.CustomerId) is not Customer customer)
                return InvoiceErrors.CustomerNotFound;

            if (await _serviceAddressRepository.GetByIdAsync(invoice.ServiceAddressId) is not ServiceAddress serviceAddress)
                return InvoiceErrors.ServiceAddressNotFound;

            return new InvoiceResponse(
                invoice.InvoiceId.Value,
                invoice.InvoiceNumber,
                invoice.BillingPeriod.ToString("MMMM"),
                invoice.TotalAmountDue.Value,
                invoice.DueDate,
                invoice.IsPaid,
                customer?.FirstName + " " + customer?.LastName,
                serviceAddress?.StreetName + " No. " + serviceAddress?.HouseNumber.Value);
        }
    }
}
