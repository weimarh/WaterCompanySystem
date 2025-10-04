using Application.Invoices.Common;
using Domain.Customers;
using Domain.Invoices;
using Domain.ServiceAddresses;
using ErrorOr;
using MediatR;

namespace Application.Invoices.GetAll
{
    public sealed class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, ErrorOr<IReadOnlyList<InvoiceResponse>>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceAddressRepository _serviceAddressRepository;

        public GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository, IServiceAddressRepository serviceAddressRepository)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _serviceAddressRepository = serviceAddressRepository ?? throw new ArgumentNullException(nameof(serviceAddressRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<InvoiceResponse>>> Handle(GetAllInvoicesQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<Invoice> invoices = await _invoiceRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();
            var serviceAddresses = await _serviceAddressRepository.GetAllAsync();

            var invoicesResponse = invoices.Select(invoiceResponse =>
            {
                var customer = customers.FirstOrDefault(x => x.CustomerId == invoiceResponse.CustomerId);
                var serviceAddress = serviceAddresses.FirstOrDefault(x => x.ServiceAddressId == invoiceResponse.ServiceAddressId);
                return new InvoiceResponse(
                    invoiceResponse.InvoiceId.Value,
                    invoiceResponse.InvoiceNumber,
                    invoiceResponse.BillingPeriod.ToString("MMMM"),
                    invoiceResponse.TotalAmountDue.Value,
                    invoiceResponse.DueDate,
                    invoiceResponse.IsPaid,
                    customer?.FirstName + " " + customer?.LastName,
                    serviceAddress?.StreetName + " No. " + serviceAddress?.HouseNumber.Value);
               
            }).ToList();

            return invoicesResponse;
        }
    }
}
