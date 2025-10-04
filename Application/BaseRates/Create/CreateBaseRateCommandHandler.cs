using Domain.BaseRates;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.BaseRates.Create
{
    public sealed class CreateBaseRateCommandHandler : IRequestHandler<CreateBaseRateCommand, ErrorOr<Unit>>
    {
        public readonly IBaseRateRepository _baseRateRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateBaseRateCommandHandler(IUnitOfWork unitOfWork, IBaseRateRepository baseRateRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _baseRateRepository = baseRateRepository ?? throw new ArgumentNullException(nameof(baseRateRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateBaseRateCommand command, CancellationToken cancellationToken)
        {
            if (command.CreationDate == DateTime.MinValue)
                return BaseRateErrors.BadCreatingRateFormat;

            if (Money.Create(command.Amount) is not Money amount)
                return BaseRateErrors.BadAmountFormat;

            var baseRate = new BaseRate
            (
                new BaseRateId(Guid.NewGuid()),
                command.CreationDate,
                amount
            );

            await _baseRateRepository.Add(baseRate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
