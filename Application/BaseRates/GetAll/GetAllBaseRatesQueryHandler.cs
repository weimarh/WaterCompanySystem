using Application.BaseRates.Common;
using Domain.BaseRates;
using ErrorOr;
using MediatR;

namespace Application.BaseRates.GetAll
{
    public sealed class GetAllBaseRatesQueryHandler : IRequestHandler<GetAllBaseRatesQuery, ErrorOr<IReadOnlyList<BaseRateResponse>>>
    {
        public readonly IBaseRateRepository _baseRateRepository;

        public GetAllBaseRatesQueryHandler(IBaseRateRepository baseRateRepository)
        {
            _baseRateRepository = baseRateRepository ?? throw new ArgumentNullException(nameof(baseRateRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<BaseRateResponse>>> Handle(GetAllBaseRatesQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<BaseRate> baseRates = await _baseRateRepository.GetAllAsync();

            return baseRates.Select(baseRate => new BaseRateResponse(
                baseRate.BaseRateId.Value,
                baseRate.CreationDate,
                baseRate.Amount.Value
                )).ToList();
        }
    }
}
