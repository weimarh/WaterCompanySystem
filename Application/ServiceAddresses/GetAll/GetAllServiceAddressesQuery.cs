using Application.ServiceAddresses.Common;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.GetAll
{
    public record GetAllServiceAddressesQuery() : IRequest<ErrorOr<IReadOnlyList<ServiceAddressResponse>>>;
}
