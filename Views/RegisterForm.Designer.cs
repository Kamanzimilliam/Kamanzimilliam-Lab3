namespace SmartRecipeBook.Views
{
    partial class RegisterForm
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
            lblName = new Label();
            txtName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPass = new Label();
            txtPass = new TextBox();
            lblConfirm = new Label();
            txtConfirm = new TextBox();
            btnRegister = new Button();
            SuspendLayout();
            //
            // lblName
            //
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 15);
            lblName.Name = "lblName";
            lblName.TabIndex = 0;
            lblName.Text = "Full name";
            //
            // txtName
            //
            txtName.Location = new Point(20, 37);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 23);
            txtName.TabIndex = 1;
            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 77);
            lblEmail.Name = "lblEmail";
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            //
            // txtEmail
            //
            txtEmail.Location = new Point(20, 99);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 23);
            txtEmail.TabIndex = 3;
            //
            // lblPass
            //
            lblPass.AutoSize = true;
            lblPass.Location = new Point(20, 139);
            lblPass.Name = "lblPass";
            lblPass.TabIndex = 4;
            lblPass.Text = "Password (min 8 characters)";
            //
            // txtPass
            //
            txtPass.Location = new Point(20, 161);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(300, 23);
            txtPass.TabIndex = 5;
            txtPass.UseSystemPasswordChar = true;
            //
            // lblConfirm
            //
            lblConfirm.AutoSize = true;
            lblConfirm.Location = new Point(20, 201);
            lblConfirm.Name = "lblConfirm";
            lblConfirm.TabIndex = 6;
            lblConfirm.Text = "Confirm password";
            //
            // txtConfirm
            //
            txtConfirm.Location = new Point(20, 223);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.Size = new Size(300, 23);
            txtConfirm.TabIndex = 7;
            txtConfirm.UseSystemPasswordChar = true;
            //
            // btnRegister
            //
            btnRegister.Location = new Point(20, 268);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(300, 34);
            btnRegister.TabIndex = 8;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            //
            // RegisterForm
            //
            AcceptButton = btnRegister;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 320);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPass);
            Controls.Add(txtPass);
            Controls.Add(lblConfirm);
            Controls.Add(txtConfirm);
            Controls.Add(btnRegister);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create account";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPass;
        private TextBox txtPass;
        private Label lblConfirm;
        private TextBox txtConfirm;
        private Button btnRegister;
    }
}
