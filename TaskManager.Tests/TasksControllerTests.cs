using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.Controllers;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Tests
{
    public class TasksControllerTests
    {
        private readonly Mock<TaskService> _taskServiceMock;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _taskServiceMock = new Mock<TaskService>();
            _controller = new TasksController(_taskServiceMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfTasks()
        {
            // Arrange
            var tasks = new List<Tasks>
            {
                new Tasks { Id = 1, Title = "Test Task", Description = "Test Description", IsCompleted = false, CreatedAt = DateTime.Now }
            };
            _taskServiceMock.Setup(service => service.GetAll()).Returns(tasks);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName); // Explicitly check the view name
            var model = Assert.IsAssignableFrom<IEnumerable<Tasks>>(viewResult.Model);
            Assert.Single(model);
            Assert.Equal("Test Task", model.First().Title);
        }

        [Fact]
        public void Create_ValidTask_RedirectsToIndex()
        {
            // Arrange
            var task = new Tasks
            {
                Title = "New Task", // Title is required, so this should pass validation
                Description = "New Description",
                IsCompleted = false
            };
            // Ensure ModelState is clear to simulate a valid state
            _controller.ModelState.Clear();

            // Act
            var result = _controller.Create(task);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _taskServiceMock.Verify(service => service.Add(It.Is<Tasks>(t => t.Title == "New Task")), Times.Once());
        }

        [Fact]
        public void Create_InvalidTask_ReturnsView()
        {
            // Arrange
            var task = new Tasks(); // Invalid task (Title is required)
            _controller.ModelState.AddModelError("Title", "The Title field is required.");

            // Act
            var result = _controller.Create(task);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Tasks>(viewResult.Model);
            Assert.Equal(task, model);
        }
    }
}