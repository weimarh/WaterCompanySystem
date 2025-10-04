using ErrorOr;
using MediatR;

namespace Application.BaseRates.Update
{
    public record UpdateBaseRateCommand(Guid BaseRateId, DateTime CreationDate, string Amount) : IRequest<ErrorOr<Unit>>;
}
