using Application.BaseRates.Common;
using ErrorOr;
using MediatR;

namespace Application.BaseRates.GetById
{
    public record GetBaseRateByIdQuery(Guid BaseRateId) : IRequest<ErrorOr<BaseRateResponse>>;
}
