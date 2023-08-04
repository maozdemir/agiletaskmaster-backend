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
    public class DependencyControllerTests
    {
        private DependencyController _dependencyController;
        private Mock<IDependencyService> _dependencyServiceMock;

        [SetUp]
        public void Setup()
        {
            _dependencyServiceMock = new Mock<IDependencyService>();
            _dependencyController = new DependencyController(_dependencyServiceMock.Object);
        }

        [Test]
        public async Task GetDependenciesByTaskId_ValidTaskId_ReturnsOkResult()
        {
            // Arrange
            string taskId = "task1";
            var dependencyDTO = new DependencyDTO { TaskId = taskId };
            _dependencyServiceMock.Setup(service => service.GetDependenciesByTaskId(taskId))
                .ReturnsAsync(new List<DependencyDTO> { dependencyDTO });

            // Act
            var result = await _dependencyController.GetDependenciesByTaskId(taskId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetDependenciesByTaskId_InvalidTaskId_ReturnsNotFoundResult()
        {
            // Arrange
            string taskId = "task1";
            _dependencyServiceMock.Setup(service => service.GetDependenciesByTaskId(taskId))
                .ReturnsAsync((List<DependencyDTO>)null);

            // Act
            var result = await _dependencyController.GetDependenciesByTaskId(taskId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task CreateDependency_ValidDependency_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var dependencyDTO = new DependencyDTO { TaskId = "task1" };
            _dependencyServiceMock.Setup(service => service.CreateDependency(dependencyDTO))
                .ReturnsAsync(dependencyDTO);

            // Act
            var result = await _dependencyController.CreateDependency(dependencyDTO);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        }

        [Test]
        public async Task DeleteDependency_ExistingDependency_ReturnsNoContentResult()
        {
            // Arrange
            string dependencyId = "dependency1";
            _dependencyServiceMock.Setup(service => service.DeleteDependency(dependencyId))
                .ReturnsAsync(true);

            // Act
            var result = await _dependencyController.DeleteDependency(dependencyId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteDependency_NonExistingDependency_ReturnsNotFoundResult()
        {
            // Arrange
            string dependencyId = "dependency1";
            _dependencyServiceMock.Setup(service => service.DeleteDependency(dependencyId))
                .ReturnsAsync(false);

            // Act
            var result = await _dependencyController.DeleteDependency(dependencyId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}