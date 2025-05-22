using KooliProjekt.WinFormsApp.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WinFormsApp
{
    public class AmountPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IAmountView _amountView;

        public AmountPresenter(IAmountView amountView, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _amountView = amountView;

            _amountView.Presenter = this;
        }

        public void UpdateView(Amount item)
        {
            if (item == null)
            {
                _amountView.Title = string.Empty;
                _amountView.Id = 0;
            }
            else
            {
                _amountView.Id = item.AmountID;
                _amountView.Title = item.AmountTitle;
                _amountView.SelectedItem = item;
            }
        }

        public async Task Load()
        {
            var result = await _apiClient.List();

            if (result == null || result.Value == null)
                _amountView.Amount = new List<Amount>();
            else
                _amountView.Amount = result.Value
                .Cast<Amount>()
                .ToList();

        }


        public void AddNewAmount(Amount newAmount)
        {
            var list = _amountView.Amount ?? new List<Amount>();
            list.Add(newAmount);
            _amountView.Amount = list;
        }

        public void DeleteAmount(Amount toDelete)
        {
            if (toDelete == null) return;
            var list = _amountView.Amount;
            if (list == null) return;

            list.Remove(toDelete);
            _amountView.Amount = list;
        }
    }
}
