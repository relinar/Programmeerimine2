using System.Collections.ObjectModel;
using System.Windows.Input;
using KooliProjekt.WpfApp1;
using WpfApp1.Api;

namespace WpfApp1
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<Amount> Lists { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<Amount> ConfirmDelete { get; set; }
        public Action<string> OnError { get; set; }

        private readonly IApiClient _apiClient;

        public MainWindowViewModel() : this(new ApiClient())
        {
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            Lists = new ObservableCollection<Amount>();

            NewCommand = new RelayCommand<Amount>(
                // Execute
                list =>
                {
                    SelectedItem = new Amount();
                }
            );

            SaveCommand = new RelayCommand<Amount>(
                // Execute
                async list =>
                {
                    await _apiClient.Save(SelectedItem);
                    await Load();
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );

            DeleteCommand = new RelayCommand<Amount>(
                // Execute
                async list =>
                {
                    if (ConfirmDelete != null)
                    {
                        var result = ConfirmDelete(SelectedItem);
                        if (!result)
                        {
                            return;
                        }
                    }

                    await _apiClient.Delete(SelectedItem.Id);
                    Lists.Remove(SelectedItem);
                    SelectedItem = null;
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );
        }

        public async Task Load()
        {
            Lists.Clear();

            var lists = await _apiClient.List();

            if (lists.HasError)
            {
                if (OnError != null)
                {
                    OnError(lists.Error);
                }

                return;
            }

            foreach (var list in lists.Value)
            {
                Lists.Add(list);
            }
        }

        private Amount _selectedItem;
        public Amount SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }
}
