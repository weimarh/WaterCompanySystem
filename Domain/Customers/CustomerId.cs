using System.ComponentModel.DataAnnotations;

namespace Domain.Customers
{
    public record CustomerId([RegularExpression(@"^\d{6,8}$", ErrorMessage = "Customer Id must be a string of 6 to 8 digits.")] string Id);
}
