using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgileTaskMaster.Controllers;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Services;

namespace AgileTaskMaster.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private AuthController _authController;

        [SetUp]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Test]
        public async Task Login_ValidLoginDTO_ReturnsOkObjectResult()
        {
            // Arrange
            var loginDTO = new LoginDTO();
            var authResponse = new AuthResponseDTO();
            _authServiceMock.Setup(s => s.Login(loginDTO)).ReturnsAsync(authResponse);

            // Act
            var result = await _authController.Login(loginDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Login_InvalidLoginDTO_ReturnsUnauthorizedResult()
        {
            // Arrange
            var loginDTO = new LoginDTO();
            _authServiceMock.Setup(s => s.Login(loginDTO)).ReturnsAsync((AuthResponseDTO)null);

            // Act
            var result = await _authController.Login(loginDTO);

            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result.Result);
        }
    }
}