using Application.Customers.Common;
using Application.Customers.Create;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.CustomersTests
{
    public class CreateCustomerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CustomersController _controller;

        public CreateCustomerTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new CustomersController(_mediator.Object);
        }

        [Fact]
        public async Task CreateCustomer_WithValidCommand_ReturnsOkWithCreatedCustomer()
        {
            //Arrange
            var command = new CreateCustomerCommand("5207907", "Weimar", "Barea", "70792462");
            var createdCustomer = new CustomerResponse("5207907", "Weimar Barea", "70792462");

            _mediator.Setup(m => m.Send(It.Is<CreateCustomerCommand>(
                c => c.CustomerId == command.CustomerId && c.FirstName == command.FirstName && c.LastName == command.PhoneNumber), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            //Act
            var result = await _controller.CreateCustomer(command);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBaseRate = Assert.IsType<Unit>(okResult.Value);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateBaseRate_WithBadIdFormat_ReturnsProblemWithErrorDescriptions()
        {
            var command = new CreateCustomerCommand("", "Weimar", "Barea", "70792462");

            var error = Error.Validation("Customer.BadIdFormat", "Bad format in ID");

            _mediator.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            // Act
            var result = await _controller.CreateCustomer(command);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in ID", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateBaseRate_WithBadFirstNameFormat_ReturnsProblemWithErrorDescriptions()
        {
            var command = new CreateCustomerCommand("5207907", "", "Barea", "70792462");

            var error = Error.Validation("Customer.BadFirstNameFormat", "Bad format in first name");

            _mediator.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            // Act
            var result = await _controller.CreateCustomer(command);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in first name", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateBaseRate_WithBadLastNameFormat_ReturnsProblemWithErrorDescriptions()
        {
            var command = new CreateCustomerCommand("5207907", "Weimar", "", "70792462");

            var error = Error.Validation("Customer.BadLastNameFormat", "Bad format in last name");

            _mediator.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            // Act
            var result = await _controller.CreateCustomer(command);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in last name", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateBaseRate_WithBadPhoneNumberFormat_ReturnsProblemWithErrorDescriptions()
        {
            var command = new CreateCustomerCommand("5207907", "Weimar", "Barea", "");

            var error = Error.Validation("Customer.BadPhoneNumberFormat", "Bad format in phone number");

            _mediator.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(error);

            // Act
            var result = await _controller.CreateCustomer(command);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);

            var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
            Assert.Contains("Bad format in phone number", problemDetails.Detail);

            _mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
