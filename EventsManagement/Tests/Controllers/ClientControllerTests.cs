using API.Controllers;
using BusinessLogic.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controllers
{
    [TestFixture]
    public class ClientControllerTests
    {
        private Mock<IClientService> _clientServiceMock;
        private ClientController _controller;

        [SetUp]
        public void Setup()
        {
            _clientServiceMock = new Mock<IClientService>();
            _controller = new ClientController(_clientServiceMock.Object);
        }

        [Test]
        public async Task CreateAsync_WithValidClientDto_ReturnsOkResult()
        {
            // Arrange
            var clientDto = new ClientDto { Name = "John Doe", Email = "john.doe@example.com" };
            var expectedResult = Result<ClientDto>.Success(new ClientDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com" });
            _clientServiceMock.Setup(x => x.CreateAsync(clientDto)).ReturnsAsync(expectedResult);
            _controller = new ClientController(_clientServiceMock.Object);

            // Act
            var actionResult = await _controller.CreateAsync(clientDto);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.That(okResult.Value, Is.EqualTo(expectedResult.Data));
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CreateAsync_WhenInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var clientDto = new ClientDto { Name = "John Doe", Email = "invalid_email" };
            var expectedResult = Result<ClientDto>.Failure(new List<string> { "The Email field is not a valid e-mail address." });
            _clientServiceMock.Setup(x => x.CreateAsync(clientDto)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CreateAsync(clientDto) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Value, Is.EqualTo(expectedResult.ErrorMessage));
        }

        [Test]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var expectedResult = Result<ClientDto>.Failure("Client not found.");
            _clientServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedResult);
            _controller = new ClientController(_clientServiceMock.Object);

            // Act
            var actionResult = await _controller.GetByIdAsync(id);
            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.Value, Is.EqualTo(expectedResult.ErrorMessage));
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetByIdAsync_WithValidId_ReturnsOkResultWithClientDto()
        {
            // Arrange
            int id = 1;
            var expectedResult = Result<ClientDto>.Success(new ClientDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com" });
            _clientServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(expectedResult);
            _controller = new ClientController(_clientServiceMock.Object);

            // Act
            var actionResult = await _controller.GetByIdAsync(id);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.That(okResult.Value, Is.EqualTo(expectedResult.Data));
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var expectedResult = Result<List<ClientDto>>.Success(new List<ClientDto>
            {
                new ClientDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new ClientDto { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            });
            _clientServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedResult);
            _controller = new ClientController(_clientServiceMock.Object);

            // Act
            var actionResult = await _controller.GetAllAsync();
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.That(okResult.Value, Is.EqualTo(expectedResult.Data));
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetAllAsync_WhenServiceFails_ReturnsBadRequestResult()
        {
            // Arrange
            var expectedResult = Result<List<ClientDto>>.Failure("An error occurred.");
            _clientServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedResult);
            _controller = new ClientController(_clientServiceMock.Object);

            // Act
            var actionResult = await _controller.GetAllAsync();
            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.Value, Is.EqualTo(expectedResult.ErrorMessage));
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task GetClientsByStatusAsync_WithValidStatus_ReturnsOkResultWithClients()
        {
            // Arrange
            var status = ClientStatusEnum.AVAILABLE;
            var expectedResult = Result<List<ClientDto>>.Success(new List<ClientDto>
            {
                new ClientDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com", StatusId = ClientStatusEnum.AVAILABLE },
                new ClientDto { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", StatusId = ClientStatusEnum.AVAILABLE },
            });
            _clientServiceMock.Setup(x => x.GetClientsByStatusAsync(status)).ReturnsAsync(expectedResult);

            // Act
            var actionResult = await _controller.GetClientsByStatusAsync(status);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.That(okResult.Value, Is.EqualTo(expectedResult.Data));
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetClientsByStatusAsync_WithInvalidStatus_ReturnsBadRequestResultWithErrorMessage()
        {
            // Arrange
            ClientStatusEnum status = default;
            var expectedResult = Result<List<ClientDto>>.Failure("Invalid status");
            _clientServiceMock.Setup(x => x.GetClientsByStatusAsync(status)).ReturnsAsync(expectedResult);

            // Act
            var actionResult = await _controller.GetClientsByStatusAsync(status);
            var badRequestResult = actionResult.Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.Value, Is.EqualTo(expectedResult.ErrorMessage));
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task UpdateAsync_WithMatchingIds_ReturnsNoContent()
        {
            // Arrange
            var clientDto = new ClientDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            _clientServiceMock.Setup(x => x.UpdateAsync(clientDto)).ReturnsAsync(Result<ClientDto>.Success(clientDto));
            var id = clientDto.Id;

            // Act
            var result = await _controller.UpdateAsync(id, clientDto);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateAsync_WithMismatchingIds_ReturnsBadRequest()
        {
            // Arrange
            var clientDto = new ClientDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            var id = 2;

            // Act
            var result = await _controller.UpdateAsync(id, clientDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.Value, Is.EqualTo("Client ID in URL does not match Client ID in body."));
        }

        [Test]
        public async Task DeleteAsync_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var clientDto = new ClientDto { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            _clientServiceMock.Setup(x => x.DeleteAsync(clientDto.Id)).ReturnsAsync(Result<ClientDto>.Success(clientDto));

            // Act
            var result = await _controller.DeleteAsync(clientDto.Id);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteAsync_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var expectedResult = Result<ClientDto>.Failure("Client not found.");
            _clientServiceMock.Setup(x => x.DeleteAsync(1)).ReturnsAsync(expectedResult);
            _controller = new ClientController(_clientServiceMock.Object);

            // Act
            var result = await _controller.DeleteAsync(1);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.Value, Is.EqualTo(expectedResult.ErrorMessage));
        }
    }
}