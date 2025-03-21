
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            TodoListsGrid.SelectionChanged += TodoListsGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            // Küsi kustutamise kinnitust
            // Kui vastus oli "Yes", siis kustuta element ja lae andmed uuesti
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            // Kontrolli ID-d:
            //  - kui IDField on tühi või 0, siis tee uus objekt
            //  - kui IDField ei ole tühi, siis küsi käesolev objekt gridi käest
            // Salvesta API kaudu
            // Lae andmed API-st uuesti
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            // Tühjenda väljad
        }

        private void TodoListsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (TodoListsGrid.SelectedRows.Count == 0)
            {
                return;
            }

            var todoList = (Amount)TodoListsGrid.SelectedRows[0].DataBoundItem;

            if(todoList == null)
            {
                IdField.Text = string.Empty;
                TitleField.Text = string.Empty;
            }
            else
            {
                IdField.Text = todoList.Id.ToString();
                TitleField.Text = todoList.Title;
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var apiClient = new ApiClient();
            var response = await apiClient.List();

            TodoListsGrid.AutoGenerateColumns = true;
            TodoListsGrid.DataSource = response.Value;
            
        }
    }
}
