using Domain.BaseRates;
using Domain.DomainErrors;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.BaseRates.Delete
{
    public sealed class DeleteBaseRateCommandHandler : IRequestHandler<DeleteBaseRateCommand, ErrorOr<Unit>>
    {
        public readonly IBaseRateRepository _baseRateRepository;
        public readonly IUnitOfWork _unitOfWork;

        public DeleteBaseRateCommandHandler(IUnitOfWork unitOfWork, IBaseRateRepository baseRateRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _baseRateRepository = baseRateRepository ?? throw new ArgumentNullException(nameof(baseRateRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteBaseRateCommand command, CancellationToken cancellationToken)
        {
            if (await _baseRateRepository.GetByIdAsync(new BaseRateId(command.BaseRateId)) is not BaseRate baseRate)
                return BaseRateErrors.BaseRateNotFound;

            await _baseRateRepository.Delete(baseRate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
