using Domain.Enums;
using Domain.Invoices;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.ServiceAddresses
{
    public sealed class ServiceAddress : AggregateRoot
    {
        public ServiceAddress() { }

        public ServiceAddress(string streetName, HouseNumber houseNumber, RatePlan ratePlan)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            RatePlan = ratePlan;
        }

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
        public ICollection<Invoice> Invoices { get; private set; } = null!;


        public static ServiceAddress UpdateServiceAddress(string streetName, HouseNumber houseNumber, RatePlan ratePlan)
        {
            return new ServiceAddress(streetName, houseNumber, ratePlan);
        }
    }
}
