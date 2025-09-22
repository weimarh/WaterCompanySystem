using Domain.Customers;
using Domain.Invoices;
using Domain.Primitives;
using Domain.Readings;
using Domain.ServiceAddresses;

namespace Domain.WaterMeters
{
    public sealed class WaterMeter : AggregateRoot
    {
        private WaterMeter() { }

        public WaterMeter(WaterMeterId waterMeterId, string model, DateTime installationDate, ServiceAddressId serviceAddressId, ServiceAddress serviceAddress, CustomerId customerId, Customer customer)
        {
            WaterMeterId = waterMeterId;
            Model = model;
            InstallationDate = installationDate;
            ServiceAddressId = serviceAddressId;
            ServiceAddress = serviceAddress;
            CustomerId = customerId;
            Customer = customer;
        }

        public WaterMeterId WaterMeterId { get; private set; } = null!;
        public string Model { get; private set; } = null!;
        public DateTime InstallationDate { get; private set; }
        public ServiceAddressId ServiceAddressId { get; private set; } = null!;
        public ServiceAddress ServiceAddress { get; private set; } = null!;
        public CustomerId CustomerId { get; private set; } = null!;
        public Customer Customer { get; private set; } = null!;
        public List<Reading> Readings { get; private set; } = null!;
        public ICollection<Invoice> Invoices { get; private set; } = null!;

        public static WaterMeter UpdateWaterMeter(WaterMeterId waterMeterId, string model, DateTime installationDate, ServiceAddressId serviceAddressId, ServiceAddress serviceAddress, CustomerId customerId, Customer customer)
        {
            return new WaterMeter(
                waterMeterId,
                model,
                installationDate,
                serviceAddressId,
                serviceAddress,
                customerId,
                customer);
        }
    }
}
