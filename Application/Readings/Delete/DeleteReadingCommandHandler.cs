using Domain.DomainErrors;
using Domain.Events;
using Domain.Invoices;
using Domain.Primitives;
using Domain.Readings;
using ErrorOr;
using MediatR;

namespace Application.Readings.Delete
{
    public class DeleteReadingCommandHandler : IRequestHandler<DeleteReadingCommand, ErrorOr<Unit>>
    {
        private readonly IReadingRepository _readingRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public DeleteReadingCommandHandler(IUnitOfWork unitOfWork, IReadingRepository readingRepository, IPublisher publisher, IInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _readingRepository = readingRepository ?? throw new ArgumentNullException(nameof(readingRepository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteReadingCommand command, CancellationToken cancellationToken)
        {
            if (await _readingRepository.GetByIdAsync(new ReadingId(command.ReadingId)) is not Reading reading)
                return ReadingErrors.ReadingNotFound;

            if (await _invoiceRepository.GetByReadingIdAsync(reading.ReadingId) is not Invoice invoice)
                return ReadingErrors.NoInvoiceFound;

            await _readingRepository.Delete(reading);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new ReadingDeletedEvent(invoice.InvoiceId));

            return Unit.Value;
        }
    }
}
