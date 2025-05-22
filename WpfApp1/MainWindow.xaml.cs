using System.Windows;
using System.Windows.Controls;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var viewModel = new MainWindowViewModel();
        viewModel.ConfirmDelete = _ =>
        {
            var result = MessageBox.Show(
                            "Are you sure you want to delete selected item?",
                            "Delete list",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Stop
                            );
            return (result == MessageBoxResult.Yes);
        };

        viewModel.OnError = (error) =>
        {
            MessageBox.Show(
                    error,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        };

        DataContext = viewModel;

        await viewModel.Load();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}