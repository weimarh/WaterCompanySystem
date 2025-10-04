using Application.ServiceAddresses.Common;
using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.GetById
{
    public record GetServiceAddressByIdQuery(Guid ServiceAddressId) : IRequest<ErrorOr<ServiceAddressResponse>>;
}
