using ErrorOr;

namespace Domain.Services
{
    public interface IDueDateCalculator
    {
        ErrorOr<DateTime> CalculateDueDate(DateTime ReadingDate);
    }
}
