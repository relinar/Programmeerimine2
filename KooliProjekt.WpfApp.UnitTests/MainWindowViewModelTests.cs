using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WpfApp1;
using WpfApp1.Api;

namespace KooliProjekt.WpfApp1.UnitTests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public async Task Load_PopulatesListsFromApiClient()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(client => client.List())
                .ReturnsAsync(new Result<List<Amount>>
                {
                    Value = new List<Amount>
                    {
                        new Amount { Id = 1, Title = "Item 1" },
                        new Amount { Id = 2, Title = "Item 2" }
                    }
                });

            var viewModel = new MainWindowViewModel(mockApiClient.Object);

            // Act
            await viewModel.Load();

            // Assert
            Assert.Equal(2, viewModel.Lists.Count);
            Assert.Equal("Item 1", viewModel.Lists[0].Title);
            Assert.Equal("Item 2", viewModel.Lists[1].Title);
        }

        [Fact]
        public void SaveCommand_CanExecute_ReturnsTrueWhenSelectedItemIsNotNull()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            var viewModel = new MainWindowViewModel(mockApiClient.Object)
            {
                SelectedItem = new Amount()
            };

            // Act
            var canExecute = viewModel.SaveCommand.CanExecute(null);

            // Assert
            Assert.True(canExecute);
        }

        [Fact]
        public void SaveCommand_CanExecute_ReturnsFalseWhenSelectedItemIsNull()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            var viewModel = new MainWindowViewModel(mockApiClient.Object)
            {
                SelectedItem = null
            };

            // Act
            var canExecute = viewModel.SaveCommand.CanExecute(null);

            // Assert
            Assert.False(canExecute);
        }

        [Fact]
        public async Task DeleteCommand_ExecutesDeletesSelectedItem()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(client => client.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);
            var viewModel = new MainWindowViewModel(mockApiClient.Object)
            {
                SelectedItem = new Amount { Id = 1 }
            };

            // Act
            viewModel.DeleteCommand.Execute(null);

            // Assert
            Assert.Null(viewModel.SelectedItem);
        }
    }
}
