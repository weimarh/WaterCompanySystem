using Application.Customers.Common;
using Application.Customers.GetById;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.CustomersTests
{
    public class GetByIdTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CustomersController _controller;

        public GetByIdTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new CustomersController(_mediator.Object);
        }

        [Fact]
        public async Task GetById_WhenCustomerExists_ReturnsOkWithCustomer()
        {
            //Arrange
            var customerId = "5207907";
            var expectedCustomer = new CustomerResponse(customerId, "Weimar Barea", "70792462");

            _mediator.Setup(m => m.Send(It.Is<GetCustomerByIdQuery>(q => q.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCustomer);

            //Act
            var result = await _controller.GetById(customerId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomer = Assert.IsType<CustomerResponse>(okResult.Value);

            Assert.Equal(expectedCustomer.CustomerId, returnedCustomer.CustomerId);
            Assert.Equal(expectedCustomer.CustomerName, returnedCustomer.CustomerName);
            Assert.Equal(expectedCustomer.PhoneNumber, returnedCustomer.PhoneNumber);

            _mediator.Verify(m => m.Send(It.Is<GetCustomerByIdQuery>(q => q.CustomerId == customerId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenCustomerNotFound_ReturnsProblemWithNotFoundError()
        {
            //Arrange
            var customerId = "5207907";
            var notFoundError = Error.NotFound("Customer.NotFound", "Customer not found");
            var errorResult =  ErrorOr<CustomerResponse>.From(new List<Error> {notFoundError});

            _mediator.Setup(m => m.Send(It.Is<GetCustomerByIdQuery>(q => q.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(errorResult);

            //Act
            var result = await _controller.GetById(customerId);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Customer not found", problemDetails.Detail);

            _mediator.Verify(m => m.Send(It.Is<GetCustomerByIdQuery>(q => q.CustomerId == customerId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetById_WithEmptyGuid_ReturnsProblem()
        {
            //Arrange
            var customerId = string.Empty;
            var validationError = Error.Validation("Customer.BadIdFormat", "Bad format in ID");
            var errorResult = ErrorOr<CustomerResponse>.From(new List<Error> { validationError });

            _mediator.Setup(m => m.Send(It.Is<GetCustomerByIdQuery>(q => q.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(errorResult);

            //Act
            var result = await _controller.GetById(customerId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);

            Assert.Contains("Bad format in ID", problemDetails.Detail);

        }
    }
}
