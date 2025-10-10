using Application.Customers.Create;
using Application.Customers.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.CustomersTests
{
    public class UpdateCustomerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CustomersController _controller;

        public UpdateCustomerTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new CustomersController(_mediator.Object);
        }

        [Fact]
        public async Task UpdateCustomer_WithValidCommandAndMatchingIds_ReturnsOkWithUpdatedCustomer()
        {
            //Arrange
            var customerId = "5039807";
            var command = new UpdateCustomerCommand(customerId, "Juan", "Perez", "78965423");

            _mediator.Setup(m => m.Send(It.Is<UpdateCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            //Act
            var result = await _controller.UpdateCustomer(customerId, command);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRate = Assert.IsType<Unit>(okResult.Value);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomer_WithMismatchedIds_ReturnsProblemWithNotFoundError()
        {
            //Arrange
            var routeId = "5207907";
            var commandId = "2596874";
            var command = new UpdateCustomerCommand(commandId, "Juan", "Perez", "78965423");

            //Act
            var result = await _controller.UpdateCustomer(routeId, command);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains(CustomerErrors.CustomerNotFound.Description, problemDetails.Detail);

            _mediator.Verify(m => m.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateBaseRate_WithBadFirstNameFormat_ReturnsProblemWithErrorDescriptions()
        {
            //Arrange
            var customerId = "5039807";
            var command = new UpdateCustomerCommand(customerId, "", "Perez", "78965423");

            var error = Error.Validation("Customer.BadFirstNameFormat", "Bad format in first name");

            _mediator.Setup(m => m.Send(It.Is<UpdateCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            //Act
            var result = await _controller.UpdateCustomer(customerId, command);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in first name", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBaseRate_WithBadLastNameFormat_ReturnsProblemWithErrorDescriptions()
        {
            //Arrange
            var customerId = "5039807";
            var command = new UpdateCustomerCommand(customerId, "Juan", "", "78965423");

            var error = Error.Validation("Customer.BadLastNameFormat", "Bad format in last name");

            _mediator.Setup(m => m.Send(It.Is<UpdateCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            //Act
            var result = await _controller.UpdateCustomer(customerId, command);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in last name", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBaseRate_WithBadPhoneNumnberFormat_ReturnsProblemWithErrorDescriptions()
        {
            //Arrange
            var customerId = "5039807";
            var command = new UpdateCustomerCommand(customerId, "Juan", "Perez", "");

            var error = Error.Validation("Customer.BadPhoneNumberFormat", "Bad format in phone number");

            _mediator.Setup(m => m.Send(It.Is<UpdateCustomerCommand>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            //Act
            var result = await _controller.UpdateCustomer(customerId, command);

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in phone number", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
