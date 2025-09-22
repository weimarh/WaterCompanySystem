using ErrorOr;
using MediatR;

namespace Application.ServiceAddresses.Delete
{
    public record DeleteServiceAddressCommand(Guid ServiceAddressId) : IRequest<ErrorOr<Unit>>;
}
