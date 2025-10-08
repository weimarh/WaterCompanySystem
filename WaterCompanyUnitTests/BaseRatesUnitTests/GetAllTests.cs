using Application.BaseRates.Common;
using Application.BaseRates.GetAll;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.BaseRatesUnitTests
{
    public class GetAllTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly BaseRatesController _controller;

        public GetAllTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new BaseRatesController(_mediator.Object);
        }

        [Fact]
        public async Task GetAll_WhenBaseRatesExist_ReturnsOkWithBaseRates()
        {
            //Arrange
            var baseRateResponse = new List<BaseRateResponse>
            {
                new BaseRateResponse (Guid.NewGuid(), DateTime.Now, "52.36"),
                new BaseRateResponse (Guid.NewGuid(), DateTime.Now, "10.50"),
            }.AsReadOnly();

            _mediator.Setup(m => m.Send(It.IsAny<GetAllBaseRatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(baseRateResponse);

            //Act
            var result = await _controller.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRates = Assert.IsAssignableFrom<IEnumerable<BaseRateResponse>>(okResult.Value);
            Assert.Equal(2, returnedBaseRates.Count());
            Assert.Equal("52.36", returnedBaseRates.First().Amount);
            Assert.Equal("10.50", returnedBaseRates.Last().Amount);
        }

        [Fact]
        public async Task GetAll_WhenNoBaseRatesExist_ReturnsOkWithEmptyList()
        {
            //Assert
            var emptyList = new List<BaseRateResponse>();

            _mediator.Setup(m => m.Send(It.IsAny<GetAllBaseRatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(emptyList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRates = Assert.IsAssignableFrom<IEnumerable<BaseRateResponse>>(okResult.Value);
            Assert.Empty(returnedBaseRates);
        }

    }
}
