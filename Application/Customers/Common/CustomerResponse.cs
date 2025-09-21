namespace Application.Customers.Common
{
    public record CustomerResponse
    (
        string CustomerId,
        string CustomerName,
        string PhoneNumber
    );
}
