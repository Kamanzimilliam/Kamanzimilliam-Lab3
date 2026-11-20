using SmartRecipeBook.Controllers;
using SmartRecipeBook.Views;

namespace SmartRecipeBook;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        while (true)
        {
            Session.Current = null;
            using (var login = new LoginForm())
                if (login.ShowDialog() != DialogResult.OK) return;

            using var main = new MainForm();
            if (main.ShowDialog() != DialogResult.Retry) return;   // Retry = Logout
        }
    }
}
