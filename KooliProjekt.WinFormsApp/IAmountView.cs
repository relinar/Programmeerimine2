namespace KooliProjekt.WinFormsApp
{
    public interface IAmountView
    {
        IList<Amount> Amount { get; set; }
        Amount SelectedItem { get; set; }
        string Title { get; set; }
        int Id { get; set; }
        AmountPresenter Presenter { get; set; }
    }
}
