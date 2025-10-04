using Domain.DomainErrors;
using Domain.Primitives;
using Domain.WaterMeters;
using ErrorOr;
using MediatR;

namespace Application.WaterMeters.Delete
{
    public sealed class DeleteWaterMeterCommandHandler : IRequestHandler<DeleteWaterMeterCommand, ErrorOr<Unit>>
    {
        public readonly IWaterMeterRepository _waterMeterRepository;
        public readonly IUnitOfWork _unitOfWork;

        public DeleteWaterMeterCommandHandler(IUnitOfWork unitOfWork, IWaterMeterRepository waterMeterRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _waterMeterRepository = waterMeterRepository ?? throw new ArgumentNullException(nameof(waterMeterRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteWaterMeterCommand command, CancellationToken cancellationToken)
        {
            if (await _waterMeterRepository.GetByIdAsync(new WaterMeterId(command.WaterMeterId)) is not WaterMeter waterMeter)
                return WaterMeterErrors.WaterMeterNotFound;

            await _waterMeterRepository.Delete(waterMeter);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
