using Domain.Enums;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.Create
{
    public record CreateServiceAddressCommand(
        string StreetName,
        string HouseNumber,
        RatePlan RatePlan) : IRequest<ErrorOr<Unit>>;
}
