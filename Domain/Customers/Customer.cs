using Domain.Invoices;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.WaterMeters;

namespace Domain.Customers
{
    public sealed class Customer : AggregateRoot
    {
        private Customer() { }

        public Customer(string firstName, string lastName, PhoneNumber phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public Customer(CustomerId customerId, string firstName, string lastName, PhoneNumber phoneNumber)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public CustomerId CustomerId { get; private set; } = null!;
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public ICollection<WaterMeter> WaterMeters { get; private set; } = null!;
        public ICollection<Invoice> Invoices { get; private set; } = null!;

        public static Customer UpdateCustomer(string firstName, string lastName, PhoneNumber phoneNumber)
        {
            return new Customer(firstName, lastName, phoneNumber);
        }
    }
}
