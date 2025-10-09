using Application.BaseRates.Common;
using Application.BaseRates.GetById;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.BaseRatesUnitTests
{
    public class GetByIdTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly BaseRatesController _controller;

        public GetByIdTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new BaseRatesController(_mediator.Object);
        }

        [Fact]
        public async Task GetById_WhenBaseRateExists_ReturnsOkWithBaseRate()
        {
            // Arrange
            var baseRateId = Guid.NewGuid();
            var expectedBaseRate = new BaseRateResponse(baseRateId, DateTime.Now, "52.36");


            _mediator
                .Setup(m => m.Send(It.Is<GetBaseRateByIdQuery>(q => q.BaseRateId == baseRateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedBaseRate);

            // Act
            var result = await _controller.GetById(baseRateId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRate = Assert.IsType<BaseRateResponse>(okResult.Value);

            Assert.Equal(expectedBaseRate.BaseRateId, returnedBaseRate.BaseRateId);
            Assert.Equal(expectedBaseRate.CreationDate, returnedBaseRate.CreationDate);
            Assert.Equal(expectedBaseRate.Amount, returnedBaseRate.Amount);

            _mediator.Verify(m => m.Send(It.Is<GetBaseRateByIdQuery>(q => q.BaseRateId == baseRateId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenBaseRateNotFound_ReturnsProblemWithNotFoundError()
        {
            // Arrange
            var baseRateId = Guid.NewGuid();
            var notFoundError = Error.NotFound("BaseRate.NotFound", "Base rate not found");
            var errorResult = ErrorOr<BaseRateResponse>.From(new List<Error> { notFoundError });

            _mediator
                .Setup(m => m.Send(It.Is<GetBaseRateByIdQuery>(q => q.BaseRateId == baseRateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(errorResult);

            // Act
            var result = await _controller.GetById(baseRateId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode); 

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Base rate not found", problemDetails.Detail);

            _mediator.Verify(m => m.Send(It.Is<GetBaseRateByIdQuery>(q => q.BaseRateId == baseRateId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetById_WithEmptyGuid_ReturnsProblem()
        {
            // Arrange
            var emptyId = Guid.Empty;
            var validationError = Error.Validation("BaseRate.InvalidId", "Base rate ID cannot be empty");
            var errorResult = ErrorOr<BaseRateResponse>.From(new List<Error> { validationError });

            _mediator
                .Setup(m => m.Send(It.Is<GetBaseRateByIdQuery>(q => q.BaseRateId == emptyId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(errorResult);

            // Act
            var result = await _controller.GetById(emptyId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);

            Assert.Contains("Base rate ID cannot be empty", problemDetails.Detail);
        }

    }
}
