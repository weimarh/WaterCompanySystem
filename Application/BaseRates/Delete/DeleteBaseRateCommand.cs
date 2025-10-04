using ErrorOr;
using MediatR;

namespace Application.BaseRates.Delete
{
    public record DeleteBaseRateCommand(Guid BaseRateId) : IRequest<ErrorOr<Unit>>;
}
