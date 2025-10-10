using Application.Invoices.Common;
using Application.Invoices.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;

namespace WaterCompanyUnitTests.InvoicesTests
{
    public class GetAllTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly InvoicesController _controller;

        public GetAllTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new InvoicesController(_mediator.Object);
        }

        [Fact]
        public async Task GetAll_WhenInvoicesExists_ReturnsOkWithInvoices()
        {
            //Arrange
            var invoices = new List<InvoiceResponse>
            {
                new InvoiceResponse(Guid.NewGuid(), 4567893, "August", "50.50", DateTime.Now, false, "Juan Perez", "Av. Papa"),
                new InvoiceResponse(Guid.NewGuid(), 4597300, "January", "20.50", DateTime.Now, false, "Julian Alvarez", "Av. 23"),
            }.AsReadOnly();

            _mediator.Setup(m => m.Send(It.IsAny<GetAllInvoicesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(invoices);

            //Act
            var result = await _controller.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomers = Assert.IsAssignableFrom<IEnumerable<InvoiceResponse>>(okResult.Value);
            Assert.Equal(2, returnedCustomers.Count());
        }
    }
}
