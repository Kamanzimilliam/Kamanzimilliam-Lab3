using SmartRecipeBook.Controllers;

namespace SmartRecipeBook.Views
{
    public partial class RegisterForm : Form
    {
        private readonly AuthController auth = new();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object? sender, EventArgs e)
        {
            if (txtPass.Text != txtConfirm.Text) { MessageBox.Show("Passwords do not match."); return; }
            try
            {
                var err = auth.Register(txtName.Text, txtEmail.Text, txtPass.Text);
                if (err != null) { MessageBox.Show(err, "Register"); return; }
                MessageBox.Show("Account created. You can now log in.", "Register");
                Close();
            }
            catch (Exception ex) { MessageBox.Show("Cannot reach the database:\n" + ex.Message, "Error"); }
        }
    }
}
