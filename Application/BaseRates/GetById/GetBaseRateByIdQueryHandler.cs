using Application.BaseRates.Common;
using Domain.BaseRates;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;

namespace Application.BaseRates.GetById
{
    public sealed class GetBaseRateByIdQueryHandler : IRequestHandler<GetBaseRateByIdQuery, ErrorOr<BaseRateResponse>>
    {
        public readonly IBaseRateRepository _baseRateRepository;

        public GetBaseRateByIdQueryHandler(IBaseRateRepository baseRateRepository)
        {
            _baseRateRepository = baseRateRepository ?? throw new ArgumentNullException(nameof(baseRateRepository));
        }

        public async Task<ErrorOr<BaseRateResponse>> Handle(GetBaseRateByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _baseRateRepository.GetByIdAsync(new BaseRateId(query.BaseRateId)) is not BaseRate baseRate)
                return BaseRateErrors.BaseRateNotFound;

            return new BaseRateResponse(
                baseRate.BaseRateId.Value,
                baseRate.CreationDate,
                baseRate.Amount.Value);
        }
    }
}
