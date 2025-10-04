using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.Update
{
    public record UpdateServiceAddressCommand(
        Guid ServiceAddressId,
        string StreetName,
        string HouseNumber,
        RatePlan RatePlan) : IRequest<ErrorOr<Unit>>;
}
