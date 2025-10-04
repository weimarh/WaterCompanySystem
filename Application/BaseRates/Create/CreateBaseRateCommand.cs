using ErrorOr;
using MediatR;

namespace Application.BaseRates.Create
{
    public record CreateBaseRateCommand(DateTime CreationDate, string Amount) : IRequest<ErrorOr<Unit>>;
}
