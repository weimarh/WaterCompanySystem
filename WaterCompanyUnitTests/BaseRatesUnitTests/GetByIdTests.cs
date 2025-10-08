using MediatR;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.BaseRatesUnitTests
{
    public class GetByIdTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly BaseRatesController _controller;

        public GetByIdTests(BaseRatesController controller, Mock<IMediator> mediator)
        {
            _mediator = new Mock<IMediator>();
            _controller = new BaseRatesController(_mediator.Object);
        }
    }
}
