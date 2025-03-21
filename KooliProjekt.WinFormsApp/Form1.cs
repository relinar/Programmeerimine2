using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            AmountGrid.SelectionChanged += AmountGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            // K�si kustutamise kinnitust
            var result = MessageBox.Show("Kas olete kindel, et soovite kustutada?", "Kustutamine", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (int.TryParse(IdField.Text, out int id))
                {
                    var apiClient = new ApiClient();
                    var deleteResult = await apiClient.Delete(id);
                    if (deleteResult.Value)
                    {
                        MessageBox.Show("Kustutamine �nnestus!");
                        await LoadData(); // Lae andmed uuesti
                    }
                    else
                    {
                        MessageBox.Show("Kustutamine eba�nnestus: " + deleteResult.Error);
                    }
                }
            }
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(IdField.Text, out int id) && !string.IsNullOrEmpty(TitleField.Text))
            {
                var amount = new Amount
                {
                    AmountID = id,
                    AmountDate = DateTime.Parse(TitleField.Text)
                };

                var apiClient = new ApiClient();
                var result = await apiClient.Save(amount);
                if (result.Value)
                {
                    MessageBox.Show("Salvestamine �nnestus!");
                    await LoadData(); // Lae andmed uuesti
                }
                else
                {
                    MessageBox.Show("Salvestamine eba�nnestus: " + result.Error);
                }
            }
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            IdField.Clear();
            TitleField.Clear();
        }

        private void AmountGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (AmountGrid.SelectedRows.Count == 0)
            {
                return;
            }

            var selectedAmount = (Amount)AmountGrid.SelectedRows[0].DataBoundItem;

            if (selectedAmount == null)
            {
                IdField.Clear();
                TitleField.Clear();
            }
            else
            {
                IdField.Text = selectedAmount.AmountID.ToString();
                TitleField.Text = selectedAmount.AmountDate.ToString();
            }
        }

        private async Task LoadData()
        {
            var apiClient = new ApiClient();
            var result = await apiClient.List();

            if (result.Value != null)
            {
                AmountGrid.DataSource = result.Value;
                AmountGrid.AutoGenerateColumns = true;
            }
            else
            {
                MessageBox.Show("Andmete laadimine eba�nnestus: " + result.Error, "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadData(); // Lae andmed esmakordselt
        }
    }
}
