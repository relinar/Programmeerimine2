using KooliProjekt.Controllers;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _controller = new HomeController();

            // ✅ Mock HttpContext to prevent NullReferenceException
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = "test-trace-id"; // Set a fixed trace ID for test coverage

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public void Index_Should_Return_Index_View()
        {
            var result = _controller.Index() as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void Privacy_Should_Return_Privacy_View()
        {
            var result = _controller.Privacy() as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Privacy");
        }

        [Fact]
        public void Error_Should_Return_Error_View_With_Model()
        {
            // Arrange
            var activity = new Activity("TestActivity");
            activity.Start();
            Activity.Current = activity;

            // Act
            var result = _controller.Error() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Error");

            var model = result.Model as ErrorViewModel;
            Assert.NotNull(model);
            Assert.False(string.IsNullOrEmpty(model.RequestId));

            // Clean up
            activity.Stop();
            Activity.Current = null;
        }

        [Fact]
        public void Error_Should_Return_Error_View_With_Model_When_Activity_Is_Null()
        {
            // Arrange
            Activity.Current = null;

            // Act
            var result = _controller.Error() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Error");

            var model = result.Model as ErrorViewModel;
            Assert.NotNull(model);
            Assert.Equal("test-trace-id", model.RequestId);
        }
    }
}
