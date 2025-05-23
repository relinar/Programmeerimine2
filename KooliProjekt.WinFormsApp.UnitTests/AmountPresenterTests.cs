using KooliProjekt.WinFormsApp;
using Moq;
using KooliProjekt.PublicAPI;

// Alias the Amount classes to avoid confusion
using LocalAmount = KooliProjekt.WinFormsApp.Amount;
using ApiAmount = KooliProjekt.PublicAPI.Amount; // adjust this if the real namespace is different

public class AmountPresenterTests
{
    private readonly Mock<IAmountView> _mockView;
    private readonly Mock<IApiClient> _mockApiClient;
    private readonly AmountPresenter _presenter;

    public AmountPresenterTests()
    {
        _mockView = new Mock<IAmountView>();
        _mockApiClient = new Mock<IApiClient>();
        _presenter = new AmountPresenter(_mockView.Object, _mockApiClient.Object);
    }

    [Fact]
    public async Task Load_ShouldSetEmptyList_WhenApiReturnsNull()
    {
        _mockApiClient
            .Setup(api => api.List())
            .ReturnsAsync((Result<List<ApiAmount>>)null);

        await _presenter.Load();

        _mockView.VerifySet(v => v.Amount = It.Is<List<LocalAmount>>(list => list.Count == 0));
    }

    [Fact]
    public async Task Load_ShouldSetAmounts_WhenApiReturnsData()
    {
        var apiData = new List<ApiAmount>
        {
            new ApiAmount { AmountID = 1, AmountTitle = "A" },
            new ApiAmount { AmountID = 2, AmountTitle = "B" }
        };

        _mockApiClient
            .Setup(api => api.List())
            .ReturnsAsync(new Result<List<ApiAmount>> { Value = apiData });

        await _presenter.Load();

        _mockView.VerifySet(v => v.Amount = It.Is<List<LocalAmount>>(list =>
            list.Count == 2 && list[0].AmountTitle == "A"));
    }

    [Fact]
    public void UpdateView_ShouldClearView_WhenItemIsNull()
    {
        _presenter.UpdateView(null);

        _mockView.VerifySet(v => v.Title = "");
        _mockView.VerifySet(v => v.Id = 0);
    }

    [Fact]
    public void UpdateView_ShouldSetViewProperties_WhenItemIsNotNull()
    {
        var amount = new LocalAmount { AmountID = 10, AmountTitle = "Test" };

        _presenter.UpdateView(amount);

        _mockView.VerifySet(v => v.Id = 10);
        _mockView.VerifySet(v => v.Title = "Test");
        _mockView.VerifySet(v => v.SelectedItem = amount);
    }

    [Fact]
    public void AddNewAmount_ShouldAddAmountToViewList()
    {
        var existingList = new List<LocalAmount>
        {
            new LocalAmount { AmountID = 1, AmountTitle = "Old" }
        };
        _mockView.SetupGet(v => v.Amount).Returns(existingList);

        var newAmount = new LocalAmount { AmountID = 2, AmountTitle = "New" };

        _presenter.AddNewAmount(newAmount);

        _mockView.VerifySet(v => v.Amount = It.Is<List<LocalAmount>>(list =>
            list.Count == 2 && list.Any(a => a.AmountID == 2 && a.AmountTitle == "New")));
    }

    [Fact]
    public void AddNewAmount_ShouldInitializeList_WhenViewAmountIsNull()
    {
        _mockView.SetupGet(v => v.Amount).Returns(() => null);
        var newAmount = new LocalAmount { AmountID = 1, AmountTitle = "First" };

        _presenter.AddNewAmount(newAmount);

        _mockView.VerifySet(v => v.Amount = It.Is<List<LocalAmount>>(list =>
            list.Count == 1 && list[0].AmountID == 1));
    }

    [Fact]
    public void DeleteAmount_ShouldRemoveAmountFromViewList()
    {
        var amount1 = new LocalAmount { AmountID = 1 };
        var amount2 = new LocalAmount { AmountID = 2 };
        var list = new List<LocalAmount> { amount1, amount2 };
        _mockView.SetupGet(v => v.Amount).Returns(list);

        _presenter.DeleteAmount(amount1);

        _mockView.VerifySet(v => v.Amount = It.Is<List<LocalAmount>>(l =>
            l.Count == 1 && l[0].AmountID == 2));
    }

    [Fact]
    public void DeleteAmount_ShouldDoNothing_WhenToDeleteIsNull()
    {
        var list = new List<LocalAmount> { new LocalAmount { AmountID = 1 } };
        _mockView.SetupGet(v => v.Amount).Returns(list);

        _presenter.DeleteAmount(null);

        _mockView.VerifySet(v => v.Amount = It.IsAny<List<LocalAmount>>(), Times.Never);
    }

    [Fact]
    public void DeleteAmount_ShouldDoNothing_WhenListIsNull()
    {
        _mockView.SetupGet(v => v.Amount).Returns(() => null);

        _presenter.DeleteAmount(new LocalAmount { AmountID = 1 });

        _mockView.VerifySet(v => v.Amount = It.IsAny<List<LocalAmount>>(), Times.Never);
    }
}
