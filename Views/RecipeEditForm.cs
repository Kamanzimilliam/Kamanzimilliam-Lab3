using SmartRecipeBook.Controllers;
using SmartRecipeBook.Models;

namespace SmartRecipeBook.Views
{
    public partial class RecipeEditForm : Form
    {
        private readonly RecipeController ctrl = new();
        private readonly Recipe recipe;

        // Parameterless constructor so the Visual Studio designer can open this form.
        public RecipeEditForm() : this(null) { }

        public RecipeEditForm(Recipe? existing)
        {
            InitializeComponent();
            recipe = existing ?? new Recipe();
            Text = existing == null ? "Add recipe" : "Edit recipe";

            txtTitle.Text = recipe.Title;
            txtPhoto.Text = recipe.PhotoPath ?? "";
            txtIngredients.Text = string.Join(Environment.NewLine,
                recipe.Ingredients.Select(i => $"{i.Name}, {i.Quantity}"));
            txtInstructions.Text = recipe.Instructions;
        }

        private void btnBrowse_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif" };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            var folder = Path.Combine(AppContext.BaseDirectory, "Images");
            Directory.CreateDirectory(folder);
            var name = Guid.NewGuid().ToString("N") + Path.GetExtension(dlg.FileName);
            File.Copy(dlg.FileName, Path.Combine(folder, name));
            txtPhoto.Text = name;
        }

        private void btnDraft_Click(object? sender, EventArgs e) => DoSave(false);

        private void btnSubmit_Click(object? sender, EventArgs e) => DoSave(true);

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DoSave(bool submit)
        {
            recipe.Title = txtTitle.Text;
            recipe.Instructions = txtInstructions.Text;
            recipe.PhotoPath = txtPhoto.Text;
            recipe.Ingredients = new List<Ingredient>();
            foreach (var line in txtIngredients.Lines)
            {
                var t = line.Trim();
                if (t.Length == 0) continue;
                var comma = t.IndexOf(",");
                var name = comma < 0 ? t : t[..comma].Trim();
                var qty = comma < 0 ? "" : t[(comma + 1)..].Trim();
                recipe.Ingredients.Add(new Ingredient { Name = name, Quantity = qty.Length == 0 ? "-" : qty });
            }
            try
            {
                var err = ctrl.Save(recipe, submit, Session.Current!);
                if (err != null) { MessageBox.Show(err, "Cannot save"); return; }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
    }
}
