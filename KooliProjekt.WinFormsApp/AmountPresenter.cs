using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.PublicAPI;


namespace KooliProjekt.WinFormsApp
{
    public class AmountPresenter
    {
        private readonly IAmountView _view;
        private readonly IApiClient _apiClient;

        public AmountPresenter(IAmountView view, IApiClient apiClient)
        {
            _view = view;
            _apiClient = apiClient;
        }

        public async Task Load()
        {
            var result = await _apiClient.List();

            if (result != null && !result.HasError && result.Value != null)
            {
                // result.Value is List<Api.Amount>
                var apiAmounts = result.Value;

                // Map Api.Amount to local Amount
                var localAmounts = apiAmounts.Select(a => new Amount
                {
                    AmountID = a.AmountID,
                    AmountTitle = a.AmountTitle
                    // map other properties if needed
                }).ToList();

                _view.Amount = localAmounts;
            }
            else
            {
                _view.Amount = new List<Amount>();
            }
        }





        public void UpdateView(Amount? item)
        {
            if (item == null)
            {
                _view.Id = 0;
                _view.Title = "";
                return;
            }

            _view.Id = item.AmountID;
            _view.Title = item.AmountTitle;
            _view.SelectedItem = item;
        }

        public void AddNewAmount(Amount newAmount)
        {
            var list = _view.Amount ?? new List<Amount>();
            list.Add(newAmount);
            _view.Amount = list;
        }

        public void DeleteAmount(Amount? toDelete)
        {
            if (toDelete == null || _view.Amount == null)
                return;

            _view.Amount.RemoveAll(a => a.AmountID == toDelete.AmountID);
            _view.Amount = new List<Amount>(_view.Amount);
        }
    }
}
