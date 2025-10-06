using Domain.Services;
using ErrorOr;

namespace Infrastructure.Services
{
    public class DueDateCalculator : IDueDateCalculator
    {
        public ErrorOr<DateTime> CalculateDueDate(DateTime ReadingDate)
        {
            return ReadingDate.AddDays(15);
        }
    }
}
