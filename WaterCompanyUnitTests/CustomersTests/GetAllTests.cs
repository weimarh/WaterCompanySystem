using Application.Customers.Common;
using Application.Customers.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.CustomersTests
{
    public class GetAllTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CustomersController _controller;

        public GetAllTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new CustomersController(_mediator.Object);
        }

        [Fact]
        public async Task GetAll_WhenCustomersExists_ReturnsOkWithCustomers()
        {
            //Arrange
            var customers = new List<CustomerResponse>
            {
                new CustomerResponse("5207907", "Weimar Barea", "70792462"),
                new CustomerResponse("5296854", "Hermilene Sanchez", "70725926"),
            }.AsReadOnly();

            _mediator.Setup(m => m.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customers);

            //Act
            var result = await _controller.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomers = Assert.IsAssignableFrom<IEnumerable<CustomerResponse>>(okResult.Value);
            Assert.Equal(2, returnedCustomers.Count());
        }

        [Fact]
        public async Task GetAll_WhenNoCustomersExists_ReturnsOkWithEmptyList()
        {
            //Arrange
            var emptyList = new List<CustomerResponse>();

            _mediator.Setup(m => m.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(emptyList);

            //Act
            var result = await _controller.GetAll();

            //Arrange
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomers = Assert.IsAssignableFrom<IEnumerable<CustomerResponse>>(okResult.Value);
            Assert.Empty(returnedCustomers);
        }
    }
}
