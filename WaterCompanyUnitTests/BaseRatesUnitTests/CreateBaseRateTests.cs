using Application.BaseRates.Common;
using Application.BaseRates.Create;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.BaseRatesUnitTests
{
    public class CreateBaseRateTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly BaseRatesController _controller;

        public CreateBaseRateTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new BaseRatesController(_mediator.Object);
        }

        [Fact]
        public async Task CreateBaseRate_WithValidCommand_ReturnsOkWithCreatedBaseRate()
        {
            //Arrange
            var command = new CreateBaseRateCommand(DateTime.UtcNow, "13.45");

            var createdBaseRate = new BaseRateResponse(Guid.NewGuid(), command.CreationDate, command.Amount);

            _mediator
                .Setup(m => m.Send(It.Is<CreateBaseRateCommand>(c =>
                    c.CreationDate == command.CreationDate &&
                    c.Amount == command.Amount),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.CreateBaseRate(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRate = Assert.IsType<Unit>(okResult.Value);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateBaseRate_WithBadAmountFormat_ReturnsProblemWithErrorDescriptions()
        {
            //Arrange
            var command = new CreateBaseRateCommand(DateTime.UtcNow, "");

            var error = Error.Validation("BaseRate.BadAmountFormat", "Bad format in Amount Date");

            _mediator.Setup(m => m.Send(It.IsAny<CreateBaseRateCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            // Act
            var result = await _controller.CreateBaseRate(command);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in Amount Date", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
