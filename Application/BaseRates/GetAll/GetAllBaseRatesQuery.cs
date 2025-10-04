using Application.BaseRates.Common;
using ErrorOr;
using MediatR;

namespace Application.BaseRates.GetAll
{
    public record GetAllBaseRatesQuery() : IRequest<ErrorOr<IReadOnlyList<BaseRateResponse>>>;
}
