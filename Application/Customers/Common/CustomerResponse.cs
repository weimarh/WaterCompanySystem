namespace Application.Customers.Common
{
    public record CustomerResponse
    (
        Guid CustomerId,
        string CustomerName,
        string PhoneNumber
    );
}
