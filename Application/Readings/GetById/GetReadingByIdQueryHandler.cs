using Application.Readings.Common;
using Domain.DomainErrors;
using Domain.Readings;
using ErrorOr;
using MediatR;

namespace Application.Readings.GetById
{
    public sealed class GetReadingByIdQueryHandler : IRequestHandler<GetReadingByIdQuery, ErrorOr<ReadingResponse>>
    {
        private readonly IReadingRepository _readingRepository;

        public GetReadingByIdQueryHandler(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository ?? throw new ArgumentNullException(nameof(readingRepository));
        }

        public async Task<ErrorOr<ReadingResponse>> Handle(GetReadingByIdQuery query, CancellationToken cancellationToken)
        {
            if (await _readingRepository.GetByIdAsync(new ReadingId(query.ReadingId)) is not Reading reading)
                return ReadingErrors.ReadingNotFound;

            return new ReadingResponse(
                reading.ReadingId.Value,
                reading.ReadingDate,
                reading.ReadingValue.Value,
                reading.WaterMeterId.Value);
        }
    }
}
