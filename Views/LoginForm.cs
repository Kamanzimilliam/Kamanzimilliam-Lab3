using SmartRecipeBook.Controllers;

namespace SmartRecipeBook.Views
{
    public partial class LoginForm : Form
    {
        private readonly AuthController auth = new();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            try
            {
                var user = auth.Login(txtEmail.Text, txtPassword.Text);
                if (user == null) { MessageBox.Show("Wrong email or password.", "Login"); return; }
                Session.Current = user;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot reach the database:\n" + ex.Message, "Error");
            }
        }

        private void btnRegister_Click(object? sender, EventArgs e)
        {
            using var f = new RegisterForm();
            f.ShowDialog(this);
        }
    }
}
