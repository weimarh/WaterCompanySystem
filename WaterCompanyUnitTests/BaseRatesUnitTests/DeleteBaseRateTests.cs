using Application.BaseRates.Delete;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.BaseRatesUnitTests
{
    public class DeleteBaseRateTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly BaseRatesController _controller;

        public DeleteBaseRateTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new BaseRatesController(_mediator.Object);
        }

        [Fact]
        public async Task DeleteBaseRate_WithValidId_ReturnsOkWithDeletedBaseRate()
        {
            var baseRateId = Guid.NewGuid();

            _mediator.Setup(m => m.Send(It.Is<DeleteBaseRateCommand>(c => c.BaseRateId == baseRateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            
            //Act
            var result = await _controller.DeleteBaseRate(baseRateId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRate = Assert.IsType<Unit>(okResult.Value);

            _mediator.Verify(m => m.Send(It.Is<DeleteBaseRateCommand>(c => c.BaseRateId == baseRateId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBaseRate_WhenBaseRateNotFound_ReturnsProblemWithNotFoundError()
        {
            // Arrange
            var baseRateId = Guid.NewGuid();
            var notFoundError = Error.NotFound("BaseRate.NotFound", "Base rate not found");

            _mediator.Setup(m => m.Send(It.Is<DeleteBaseRateCommand>(c => c.BaseRateId == baseRateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(notFoundError);

            //Act
            var result = await _controller.DeleteBaseRate(baseRateId);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Base rate not found", problemDetails.Detail);

            _mediator.Verify(m => m.Send(It.Is<DeleteBaseRateCommand>(c => c.BaseRateId == baseRateId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
