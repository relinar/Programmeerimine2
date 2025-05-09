namespace KooliProjekt.WinFormsApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView AmountGrid;
        private System.Windows.Forms.Label IdLabel;
        private System.Windows.Forms.TextBox IdField;
        private System.Windows.Forms.Label NutrientsLabel;
        private System.Windows.Forms.TextBox NutrientsField;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.DateTimePicker DateField;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.TextBox TitleField;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DeleteButton;

        private void InitializeComponent()
        {
            this.AmountGrid = new System.Windows.Forms.DataGridView();
            this.IdLabel = new System.Windows.Forms.Label();
            this.IdField = new System.Windows.Forms.TextBox();
            this.NutrientsLabel = new System.Windows.Forms.Label();
            this.NutrientsField = new System.Windows.Forms.TextBox();
            this.DateLabel = new System.Windows.Forms.Label();
            this.DateField = new System.Windows.Forms.DateTimePicker();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TitleField = new System.Windows.Forms.TextBox();
            this.NewButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AmountGrid)).BeginInit();
            this.SuspendLayout();

            // 
            // AmountGrid
            // 
            this.AmountGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AmountGrid.Location = new System.Drawing.Point(5, 6);
            this.AmountGrid.MultiSelect = false;
            this.AmountGrid.Name = "AmountGrid";
            this.AmountGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AmountGrid.Size = new System.Drawing.Size(419, 432);
            this.AmountGrid.TabIndex = 0;

            // 
            // IdLabel
            // 
            this.IdLabel.AutoSize = true;
            this.IdLabel.Location = new System.Drawing.Point(460, 16);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(70, 15);
            this.IdLabel.TabIndex = 1;
            this.IdLabel.Text = "AmountID:";
            // 
            // IdField
            // 
            this.IdField.Location = new System.Drawing.Point(550, 13);
            this.IdField.Name = "IdField";
            this.IdField.Size = new System.Drawing.Size(250, 23);
            this.IdField.TabIndex = 2;
            // 
            // NutrientsLabel
            // 
            this.NutrientsLabel.AutoSize = true;
            this.NutrientsLabel.Location = new System.Drawing.Point(460, 56);
            this.NutrientsLabel.Name = "NutrientsLabel";
            this.NutrientsLabel.Size = new System.Drawing.Size(80, 15);
            this.NutrientsLabel.TabIndex = 3;
            this.NutrientsLabel.Text = "NutrientsID:";
            // 
            // NutrientsField
            // 
            this.NutrientsField.Location = new System.Drawing.Point(550, 53);
            this.NutrientsField.Name = "NutrientsField";
            this.NutrientsField.Size = new System.Drawing.Size(250, 23);
            this.NutrientsField.TabIndex = 4;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(460, 96);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(79, 15);
            this.DateLabel.TabIndex = 5;
            this.DateLabel.Text = "AmountDate:";
            // 
            // DateField
            // 
            this.DateField.Location = new System.Drawing.Point(550, 93);
            this.DateField.Name = "DateField";
            this.DateField.Size = new System.Drawing.Size(250, 23);
            this.DateField.TabIndex = 6;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(460, 136);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(83, 15);
            this.TitleLabel.TabIndex = 7;
            this.TitleLabel.Text = "AmountTitle:";
            // 
            // TitleField
            // 
            this.TitleField.Location = new System.Drawing.Point(550, 133);
            this.TitleField.Name = "TitleField";
            this.TitleField.Size = new System.Drawing.Size(250, 23);
            this.TitleField.TabIndex = 8;
            // 
            // NewButton
            // 
            this.NewButton.Location = new System.Drawing.Point(550, 180);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(75, 23);
            this.NewButton.TabIndex = 9;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(640, 180);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(730, 180);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 11;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.TitleField);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.DateField);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.NutrientsField);
            this.Controls.Add(this.NutrientsLabel);
            this.Controls.Add(this.IdField);
            this.Controls.Add(this.IdLabel);
            this.Controls.Add(this.AmountGrid);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.AmountGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
