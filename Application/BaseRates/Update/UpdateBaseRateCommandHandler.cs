using Domain.BaseRates;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.BaseRates.Update
{
    public sealed class UpdateBaseRateCommandHandler : IRequestHandler<UpdateBaseRateCommand, ErrorOr<Unit>>
    {
        public readonly IBaseRateRepository _baseRateRepository;
        public readonly IUnitOfWork _unitOfWork;

        public UpdateBaseRateCommandHandler(IUnitOfWork unitOfWork, IBaseRateRepository baseRateRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _baseRateRepository = baseRateRepository ?? throw new ArgumentNullException(nameof(baseRateRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateBaseRateCommand command, CancellationToken cancellationToken)
        {
            if (!await _baseRateRepository.ExistsAsync(new BaseRateId(command.BaseRateId)))
                return BaseRateErrors.BaseRateNotFound;

            if (command.CreationDate == DateTime.MinValue)
                return BaseRateErrors.BadCreatingRateFormat;

            if (Money.Create(command.Amount) is not Money amount)
                return BaseRateErrors.BadAmountFormat;

            BaseRate baseRate = BaseRate.UpdateBaseRate(
                new BaseRateId(command.BaseRateId),
                command.CreationDate,
                amount);

            _baseRateRepository.Update(baseRate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
