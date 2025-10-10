using Application.BaseRates.Delete;
using Application.Customers.Delete;
using Domain.BaseRates;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.CustomersTests
{
    public class DeleteCustomerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CustomersController _controller;

        public DeleteCustomerTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new CustomersController(_mediator.Object);
        }

        [Fact]
        public async Task DeleteCustomer_WithValidId_ReturnsOkWithDeletedCustomer()
        {
            var customerId = "5285697";

            _mediator.Setup(m => m.Send(It.Is<DeleteCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            //Act
            var result = await _controller.DeleteCustomer(customerId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRate = Assert.IsType<Unit>(okResult.Value);

            _mediator.Verify(m => m.Send(It.Is<DeleteCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomer_WhenCustomerNotFound_ReturnsProblemWithNotFoundError()
        {
            //Assert
            var customerId = "5285697";
            var notFoundError = Error.NotFound("Customer.CustomerNotFound", "Customer not found");

            _mediator.Setup(m => m.Send(It.Is<DeleteCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(notFoundError);

            //Act
            var result = await _controller.DeleteCustomer(customerId);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Customer not found", problemDetails.Detail);

            _mediator.Verify(m => m.Send(It.Is<DeleteCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
