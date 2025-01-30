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
        public void Index_should_return_index_view()
        {
            var result = _controller.Index() as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact]
        public void Privacy_should_return_privacy_view()
        {
            var result = _controller.Privacy() as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Privacy");
        }
        [Fact]
        public void Error_should_return_error_view_with_model()
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

    }
}