using Application.Readings.Common;
using Domain.Readings;
using ErrorOr;
using MediatR;

namespace Application.Readings.GetAll
{
    public sealed class GetAllReadingsQueryHandler : IRequestHandler<GetAllReadingsQuery, ErrorOr<IReadOnlyList<ReadingResponse>>>
    {
        private readonly IReadingRepository _readingRepository;

        public GetAllReadingsQueryHandler(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository ?? throw new ArgumentNullException(nameof(readingRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<ReadingResponse>>> Handle(GetAllReadingsQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyList<Reading> readings = await _readingRepository.GetAllAsync();

            return readings.Select(reading => new ReadingResponse(reading.ReadingId.Value,
                reading.ReadingDate,
                reading.ReadingValue.Value,
                reading.WaterMeterId.Value)).ToList();
        }
    }
}
