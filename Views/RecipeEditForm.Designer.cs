namespace SmartRecipeBook.Views
{
    partial class RecipeEditForm
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtTitle = new TextBox();
            lblPhoto = new Label();
            txtPhoto = new TextBox();
            btnBrowse = new Button();
            lblIngredients = new Label();
            txtIngredients = new TextBox();
            lblInstructions = new Label();
            txtInstructions = new TextBox();
            btnDraft = new Button();
            btnSubmit = new Button();
            btnCancel = new Button();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(15, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            //
            // txtTitle
            //
            txtTitle.Location = new Point(15, 38);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(490, 23);
            txtTitle.TabIndex = 1;
            //
            // lblPhoto
            //
            lblPhoto.AutoSize = true;
            lblPhoto.Location = new Point(15, 72);
            lblPhoto.Name = "lblPhoto";
            lblPhoto.TabIndex = 2;
            lblPhoto.Text = "Dish photo (optional)";
            //
            // txtPhoto
            //
            txtPhoto.Location = new Point(15, 95);
            txtPhoto.Name = "txtPhoto";
            txtPhoto.ReadOnly = true;
            txtPhoto.Size = new Size(400, 23);
            txtPhoto.TabIndex = 3;
            //
            // btnBrowse
            //
            btnBrowse.Location = new Point(421, 94);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(84, 27);
            btnBrowse.TabIndex = 4;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            //
            // lblIngredients
            //
            lblIngredients.AutoSize = true;
            lblIngredients.Location = new Point(15, 130);
            lblIngredients.Name = "lblIngredients";
            lblIngredients.TabIndex = 5;
            lblIngredients.Text = "Ingredients - one per line, written as: name, quantity";
            //
            // txtIngredients
            //
            txtIngredients.AcceptsReturn = true;
            txtIngredients.Location = new Point(15, 153);
            txtIngredients.Multiline = true;
            txtIngredients.Name = "txtIngredients";
            txtIngredients.ScrollBars = ScrollBars.Vertical;
            txtIngredients.Size = new Size(490, 100);
            txtIngredients.TabIndex = 6;
            //
            // lblInstructions
            //
            lblInstructions.AutoSize = true;
            lblInstructions.Location = new Point(15, 262);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.TabIndex = 7;
            lblInstructions.Text = "Instructions";
            //
            // txtInstructions
            //
            txtInstructions.AcceptsReturn = true;
            txtInstructions.Location = new Point(15, 285);
            txtInstructions.Multiline = true;
            txtInstructions.Name = "txtInstructions";
            txtInstructions.ScrollBars = ScrollBars.Vertical;
            txtInstructions.Size = new Size(490, 200);
            txtInstructions.TabIndex = 8;
            //
            // btnDraft
            //
            btnDraft.Location = new Point(15, 505);
            btnDraft.Name = "btnDraft";
            btnDraft.Size = new Size(130, 34);
            btnDraft.TabIndex = 9;
            btnDraft.Text = "Save draft";
            btnDraft.UseVisualStyleBackColor = true;
            btnDraft.Click += btnDraft_Click;
            //
            // btnSubmit
            //
            btnSubmit.Location = new Point(155, 505);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(150, 34);
            btnSubmit.TabIndex = 10;
            btnSubmit.Text = "Submit for review";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            //
            // btnCancel
            //
            btnCancel.Location = new Point(415, 505);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 34);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            //
            // RecipeEditForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 560);
            Controls.Add(lblTitle);
            Controls.Add(txtTitle);
            Controls.Add(lblPhoto);
            Controls.Add(txtPhoto);
            Controls.Add(btnBrowse);
            Controls.Add(lblIngredients);
            Controls.Add(txtIngredients);
            Controls.Add(lblInstructions);
            Controls.Add(txtInstructions);
            Controls.Add(btnDraft);
            Controls.Add(btnSubmit);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RecipeEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Recipe";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private TextBox txtTitle;
        private Label lblPhoto;
        private TextBox txtPhoto;
        private Button btnBrowse;
        private Label lblIngredients;
        private TextBox txtIngredients;
        private Label lblInstructions;
        private TextBox txtInstructions;
        private Button btnDraft;
        private Button btnSubmit;
        private Button btnCancel;
    }
}
