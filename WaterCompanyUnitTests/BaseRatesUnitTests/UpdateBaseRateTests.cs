using Application.BaseRates.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.BaseRatesUnitTests
{
    public class UpdateBaseRateTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly BaseRatesController _controller;

        public UpdateBaseRateTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new BaseRatesController(_mediator.Object);
        }

        [Fact]
        public async Task UpdateBaseRate_WithValidCommandAndMatchingIds_ReturnsOkWithUpdatedBaseRate()
        {
            //Arrange
            var baseRateId = Guid.NewGuid();
            var command = new UpdateBaseRateCommand(baseRateId, DateTime.Now, "10.50");

            _mediator.Setup(m => m.Send(It.Is<UpdateBaseRateCommand>(c => c.BaseRateId == baseRateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            //Act
            var result = await _controller.UpdateBaseRate(baseRateId, command);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRate = Assert.IsType<Unit>(okResult.Value);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBaseRate_WithMismatchedIds_ReturnsProblemWithNotFoundError()
        {
            //Arrange
            var routeId = Guid.NewGuid();
            var commandId = Guid.NewGuid();
            var command = new UpdateBaseRateCommand(commandId, DateTime.Now, "10.50");

            //Act
            var result = await _controller.UpdateBaseRate(routeId, command);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains(BaseRateErrors.BaseRateNotFound.Description, problemDetails.Detail);

            _mediator.Verify(m => m.Send(It.IsAny<UpdateBaseRateCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateBaseRate_WithValidationErrors_ReturnsProblemWithErrorDescriptions()
        {
            //Arrange
            var baseRateId = Guid.NewGuid();
            var command = new UpdateBaseRateCommand(baseRateId, DateTime.Now, "");

            var error = Error.Validation("BaseRate.BadAmountFormat", "Bad format in Amount Date");

            _mediator.Setup(m => m.Send(It.Is<UpdateBaseRateCommand>(c => c.BaseRateId == baseRateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            //Act
            var result = await _controller.UpdateBaseRate(baseRateId, command);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in Amount Date", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
