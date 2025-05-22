using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IAmountView
    {
        private List<Amount> amountsList = new List<Amount>();
        private int currentAmountID = 1;

        public IAmountView AmountView => this;

        public IList<Amount> Amount
        {
            get => amountsList;
            set
            {
                amountsList = value != null ? new List<Amount>(value) : new List<Amount>();
                AmountGrid.DataSource = null;
                AmountGrid.DataSource = amountsList;

                AmountGrid.Columns["AmountID"].HeaderText = "Amount ID";
                AmountGrid.Columns["NutrientsID"].HeaderText = "Nutrients ID";
                AmountGrid.Columns["AmountDate"].HeaderText = "Amount Date";
                AmountGrid.Columns["AmountTitle"].HeaderText = "Amount Title";
            }
        }

        public Amount SelectedItem
        {
            get
            {
                if (AmountGrid.CurrentRow == null)
                    return null;

                return AmountGrid.CurrentRow.DataBoundItem as Amount;
            }
            set
            {
                if (value == null)
                {
                    IdField.Text = "";
                    NutrientsField.Text = "";
                    TitleField.Text = "";
                    DateField.Value = DateTime.Now;
                    return;
                }

                IdField.Text = value.AmountID.ToString();
                NutrientsField.Text = value.NutrientsID.ToString();
                TitleField.Text = value.AmountTitle;
                DateField.Value = value.AmountDate;
            }
        }

        public string Title
        {
            get => TitleField.Text;
            set => TitleField.Text = value;
        }

        public int Id
        {
            get
            {
                if (int.TryParse(IdField.Text, out int id))
                    return id;
                return 0;
            }
            set => IdField.Text = value.ToString();
        }

        public AmountPresenter Presenter { get; set; }

        private readonly IApiClient apiClient = new ApiClient();

        public Form1()
        {
            InitializeComponent();

            Presenter = new AmountPresenter(this, apiClient);

            Load += async (s, e) =>
            {
                await Presenter.Load();
            };

            AmountGrid.SelectionChanged += (s, e) =>
            {
                SelectedItem = AmountGrid.CurrentRow?.DataBoundItem as Amount;
            };
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            IdField.Clear();
            NutrientsField.Clear();
            TitleField.Clear();
            DateField.Value = DateTime.Now;
            AmountGrid.ClearSelection();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NutrientsField.Text) || string.IsNullOrWhiteSpace(TitleField.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (!int.TryParse(NutrientsField.Text, out int nutrientsId))
            {
                MessageBox.Show("NutrientsID must be a number.");
                return;
            }

            var newAmount = new Amount
            {
                AmountID = currentAmountID++,
                NutrientsID = nutrientsId,
                AmountDate = DateField.Value,
                AmountTitle = TitleField.Text
            };

            Presenter.AddNewAmount(newAmount);

            MessageBox.Show("Data saved successfully!");
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var toDelete = SelectedItem;
            if (toDelete == null)
            {
                MessageBox.Show("Please select an amount to delete.");
                return;
            }

            Presenter.DeleteAmount(toDelete);
            MessageBox.Show("Amount deleted successfully!");

            NewButton_Click(sender, e);
        }
    }

    public class Amount
    {
        public int AmountID { get; set; }
        public int NutrientsID { get; set; }
        public DateTime AmountDate { get; set; }
        public string AmountTitle { get; set; }
    }
}
