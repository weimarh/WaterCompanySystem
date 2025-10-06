using Domain.Enums;
using Domain.Invoices;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.ServiceAddresses
{
    public sealed class ServiceAddress : AggregateRoot
    {
        private ServiceAddress() { }

        public ServiceAddress(ServiceAddressId streetAddressId, string streetName, HouseNumber houseNumber, RatePlan ratePlan)
        {
            ServiceAddressId = streetAddressId;
            StreetName = streetName;
            HouseNumber = houseNumber;
            RatePlan = ratePlan;
        }

        public ServiceAddressId ServiceAddressId { get; private set; } = null!;
        public string StreetName { get; private set; } = null!;
        public HouseNumber HouseNumber { get; private set; } = null!;
        public RatePlan RatePlan { get; private set; }
        public List<Invoice> Invoices { get; private set; } = null!;


        public static ServiceAddress UpdateServiceAddress(ServiceAddressId serviceAddressId, string streetName, HouseNumber houseNumber, RatePlan ratePlan)
        {
            return new ServiceAddress(serviceAddressId, streetName, houseNumber, ratePlan);
        }
    }
}
