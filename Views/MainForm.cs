using SmartRecipeBook.Controllers;
using SmartRecipeBook.Models;

namespace SmartRecipeBook.Views
{
    public partial class MainForm : Form
    {
        private readonly RecipeController recipes = new();
        private readonly User me = Session.Current!;
        private List<Recipe> shown = new();

        public MainForm()
        {
            InitializeComponent();
            Text = $"Smart Recipe Book - {me.FullName} ({me.Role})";

            if (me.IsAdmin) cboScope.Items.Add("Pending review");
            btnApprove.Visible = btnReject.Visible = me.IsAdmin;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            split.SplitterDistance = 540;
            cboScope.SelectedIndex = 0;   // fires SelectedIndexChanged, which loads the list
        }

        // ---------- helpers ----------

        private string ScopeKey => cboScope.SelectedIndex switch { 1 => "mine", 2 => "favorites", 3 => "pending", _ => "published" };

        private Recipe? Selected =>
            grid.CurrentRow != null && grid.CurrentRow.Index < shown.Count ? shown[grid.CurrentRow.Index] : null;

        private static void Run(Action a)
        {
            try { a(); }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Smart Recipe Book"); }
        }

        private static void Report(string? error) =>
            MessageBox.Show(error ?? "Done.", error == null ? "Smart Recipe Book" : "Problem");

        private void Reload()
        {
            shown = recipes.List(ScopeKey, txtSearch.Text, me.Id);
            grid.DataSource = shown.Select(r => new { r.Id, r.Title, Owner = r.OwnerName, r.Status }).ToList();
            ShowSelected();
        }

        private void ShowSelected()
        {
            pic.Image?.Dispose();
            pic.Image = null;
            var sel = Selected;
            if (sel == null) { txtDetail.Text = ""; UpdateButtons(null); return; }

            var full = recipes.Get(sel.Id);
            if (full != null)
            {
                txtDetail.Text = $"{full.Title}\r\nBy {full.OwnerName}  -  {full.Status}\r\n\r\nINGREDIENTS\r\n" +
                    string.Join("\r\n", full.Ingredients.Select(i => $"- {i.Name}: {i.Quantity}")) +
                    "\r\n\r\nINSTRUCTIONS\r\n" + full.Instructions;
                if (!string.IsNullOrEmpty(full.PhotoPath))
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "Images", full.PhotoPath);
                    if (File.Exists(path))
                    {
                        try { using var tmp = Image.FromFile(path); pic.Image = new Bitmap(tmp); } catch { }
                    }
                }
            }
            UpdateButtons(sel);
        }

        private void UpdateButtons(Recipe? r)
        {
            bool editable = r != null && r.OwnerId == me.Id && (r.Status == "DRAFT" || r.Status == "REJECTED");
            btnEdit.Enabled = btnSubmit.Enabled = editable;
            btnDelete.Enabled = r != null && (r.OwnerId == me.Id || me.IsAdmin);
            btnFav.Enabled = r?.Status == "PUBLISHED";
            btnApprove.Enabled = btnReject.Enabled = me.IsAdmin && r?.Status == "PENDING";
        }

        // ---------- event handlers (wired in MainForm.Designer.cs) ----------

        private void cboScope_SelectedIndexChanged(object? sender, EventArgs e) => Run(Reload);

        private void btnSearch_Click(object? sender, EventArgs e) => Run(Reload);

        private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Run(Reload); }
        }

        private void grid_SelectionChanged(object? sender, EventArgs e) => Run(ShowSelected);

        private void btnAdd_Click(object? sender, EventArgs e) => Run(() =>
        {
            using var f = new RecipeEditForm(null);
            if (f.ShowDialog(this) == DialogResult.OK) Reload();
        });

        private void btnEdit_Click(object? sender, EventArgs e) => Run(() =>
        {
            var full = recipes.Get(Selected!.Id);
            using var f = new RecipeEditForm(full);
            if (f.ShowDialog(this) == DialogResult.OK) Reload();
        });

        private void btnSubmit_Click(object? sender, EventArgs e) => Run(() =>
        {
            var full = recipes.Get(Selected!.Id)!;
            Report(recipes.Save(full, true, me));
            Reload();
        });

        private void btnDelete_Click(object? sender, EventArgs e) => Run(() =>
        {
            var r = Selected!;
            if (MessageBox.Show($"Delete \"{r.Title}\"?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            Report(recipes.Delete(r.Id, me));
            Reload();
        });

        private void btnFav_Click(object? sender, EventArgs e) => Run(() =>
        {
            MessageBox.Show(recipes.ToggleFavorite(me.Id, Selected!.Id));
            Reload();
        });

        private void btnApprove_Click(object? sender, EventArgs e) => Run(() =>
        {
            Report(recipes.Review(Selected!.Id, true, me));
            Reload();
        });

        private void btnReject_Click(object? sender, EventArgs e) => Run(() =>
        {
            Report(recipes.Review(Selected!.Id, false, me));
            Reload();
        });

        private void btnNotes_Click(object? sender, EventArgs e) => Run(() =>
        {
            var msgs = recipes.TakeNotifications(me.Id);
            MessageBox.Show(msgs.Count == 0 ? "No new notifications." : string.Join(Environment.NewLine, msgs), "Notifications");
        });

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;   // Program.cs treats Retry as "log out"
            Close();
        }
    }
}
