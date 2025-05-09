using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form
    {
        // List to hold Amount objects
        private List<Amount> amountsList = new List<Amount>();
        private IApiClient apiClient = new ApiClient();

        private int currentAmountID = 1; // Ensuring unique IDs for each Amount

        public Form1()
        {
            InitializeComponent();
            LoadData();  // Call LoadData when the form is initialized

            Load += Form1_Load;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            var result = await apiClient.List();

            AmountGrid.DataSource = result.Value;
        }

        // Method to load data and bind it to the DataGridView
        private void LoadData()
        {
            // Debugging: Check if the amountsList has data
            Console.WriteLine("Loading data...");
            foreach (var amount in amountsList)
            {
                Console.WriteLine($"AmountID: {amount.AmountID}, NutrientsID: {amount.NutrientsID}, Title: {amount.AmountTitle}, Date: {amount.AmountDate}");
            }

            // Set the data source for the DataGridView
            AmountGrid.DataSource = null;  // Clear previous data
            AmountGrid.DataSource = amountsList; // Bind new data

            // Set column headers for clarity (optional)
            AmountGrid.Columns["AmountID"].HeaderText = "Amount ID";
            AmountGrid.Columns["NutrientsID"].HeaderText = "Nutrients ID";
            AmountGrid.Columns["AmountDate"].HeaderText = "Amount Date";
            AmountGrid.Columns["AmountTitle"].HeaderText = "Amount Title";

            // Debugging: Confirm DataGridView refresh
            Console.WriteLine("DataGrid bound successfully.");
        }

        // Button event for "New"
        private void NewButton_Click(object sender, EventArgs e)
        {
            // Clear all input fields when the "New" button is clicked
            IdField.Clear();
            NutrientsField.Clear();
            TitleField.Clear();
            DateField.Value = DateTime.Now; // Set default date to current date
        }

        // Button event for "Save"
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Check if all fields are filled in
            if (string.IsNullOrWhiteSpace(NutrientsField.Text) || string.IsNullOrWhiteSpace(TitleField.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            // Create a new Amount object with the current data from the fields
            var newAmount = new Amount
            {
                AmountID = currentAmountID++,  // Increment ID for each new entry
                NutrientsID = int.Parse(NutrientsField.Text),  // Convert NutrientsID to integer
                AmountDate = DateField.Value,  // Get selected date
                AmountTitle = TitleField.Text  // Get entered title
            };

            // Debugging: Output the new amount to confirm it's correct
            Console.WriteLine($"New Amount Added: ID = {newAmount.AmountID}, NutrientsID = {newAmount.NutrientsID}, Title = {newAmount.AmountTitle}, Date = {newAmount.AmountDate}");

            // Add the new Amount object to the list
            amountsList.Add(newAmount);

            // Reload data to update the DataGridView
            LoadData();

            // Clear fields after saving
            IdField.Clear();
            NutrientsField.Clear();
            TitleField.Clear();
            DateField.Value = DateTime.Now;

            // Optional: Show confirmation message
            MessageBox.Show("Data saved successfully!");
        }

        // Button event for "Delete"
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // Logic to delete the selected row from the grid
            if (AmountGrid.SelectedRows.Count > 0)
            {
                // Get the selected row's AmountID
                var selectedRow = AmountGrid.SelectedRows[0];
                var selectedAmountID = (int)selectedRow.Cells["AmountID"].Value;

                // Find the object in the list that matches the AmountID and remove it
                var amountToDelete = amountsList.Find(a => a.AmountID == selectedAmountID);
                if (amountToDelete != null)
                {
                    amountsList.Remove(amountToDelete);
                    LoadData();  // Refresh the grid after deletion
                    MessageBox.Show("Amount deleted successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select an amount to delete.");
            }

            // Clear the fields after deletion
            IdField.Clear();
            NutrientsField.Clear();
            TitleField.Clear();
            DateField.Value = DateTime.Now;
        }
    }

    // Class to represent Amount
    public class Amount
    {
        public int AmountID { get; set; }
        public int NutrientsID { get; set; }
        public DateTime AmountDate { get; set; }
        public string AmountTitle { get; set; }
    }
}
