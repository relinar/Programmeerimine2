using System.Collections.Generic;

namespace KooliProjekt.WinFormsApp
{
    public interface IAmountView
    {
        List<Amount> Amount { get; set; }
        int Id { get; set; }
        string Title { get; set; }
        Amount? SelectedItem { get; set; }
    }
}
