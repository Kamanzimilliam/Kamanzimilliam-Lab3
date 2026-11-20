namespace SmartRecipeBook.Views
{
    partial class MainForm
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
            topPanel = new FlowLayoutPanel();
            cboScope = new ComboBox();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnNotes = new Button();
            btnLogout = new Button();
            bottomPanel = new FlowLayoutPanel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnSubmit = new Button();
            btnDelete = new Button();
            btnFav = new Button();
            btnApprove = new Button();
            btnReject = new Button();
            split = new SplitContainer();
            grid = new DataGridView();
            txtDetail = new TextBox();
            pic = new PictureBox();
            topPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)split).BeginInit();
            split.Panel1.SuspendLayout();
            split.Panel2.SuspendLayout();
            split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            SuspendLayout();
            //
            // topPanel
            //
            topPanel.Controls.Add(cboScope);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnSearch);
            topPanel.Controls.Add(btnNotes);
            topPanel.Controls.Add(btnLogout);
            topPanel.Dock = DockStyle.Top;
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(6);
            topPanel.Size = new Size(984, 42);
            topPanel.TabIndex = 1;
            //
            // cboScope
            //
            cboScope.DropDownStyle = ComboBoxStyle.DropDownList;
            cboScope.FormattingEnabled = true;
            cboScope.Items.AddRange(new object[] { "Published recipes", "My recipes", "My favorites" });
            cboScope.Name = "cboScope";
            cboScope.Size = new Size(160, 23);
            cboScope.TabIndex = 0;
            cboScope.SelectedIndexChanged += cboScope_SelectedIndexChanged;
            //
            // txtSearch
            //
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search title or ingredient";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.KeyDown += txtSearch_KeyDown;
            //
            // btnSearch
            //
            btnSearch.AutoSize = true;
            btnSearch.Name = "btnSearch";
            btnSearch.Padding = new Padding(6, 2, 6, 2);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            //
            // btnNotes
            //
            btnNotes.AutoSize = true;
            btnNotes.Name = "btnNotes";
            btnNotes.Padding = new Padding(6, 2, 6, 2);
            btnNotes.TabIndex = 3;
            btnNotes.Text = "Notifications";
            btnNotes.UseVisualStyleBackColor = true;
            btnNotes.Click += btnNotes_Click;
            //
            // btnLogout
            //
            btnLogout.AutoSize = true;
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(6, 2, 6, 2);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            //
            // bottomPanel
            //
            bottomPanel.Controls.Add(btnAdd);
            bottomPanel.Controls.Add(btnEdit);
            bottomPanel.Controls.Add(btnSubmit);
            bottomPanel.Controls.Add(btnDelete);
            bottomPanel.Controls.Add(btnFav);
            bottomPanel.Controls.Add(btnApprove);
            bottomPanel.Controls.Add(btnReject);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Padding = new Padding(6);
            bottomPanel.Size = new Size(984, 46);
            bottomPanel.TabIndex = 2;
            //
            // btnAdd
            //
            btnAdd.AutoSize = true;
            btnAdd.Name = "btnAdd";
            btnAdd.Padding = new Padding(6, 2, 6, 2);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add recipe";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            //
            // btnEdit
            //
            btnEdit.AutoSize = true;
            btnEdit.Name = "btnEdit";
            btnEdit.Padding = new Padding(6, 2, 6, 2);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            //
            // btnSubmit
            //
            btnSubmit.AutoSize = true;
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Padding = new Padding(6, 2, 6, 2);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Submit for review";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            //
            // btnDelete
            //
            btnDelete.AutoSize = true;
            btnDelete.Name = "btnDelete";
            btnDelete.Padding = new Padding(6, 2, 6, 2);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            //
            // btnFav
            //
            btnFav.AutoSize = true;
            btnFav.Name = "btnFav";
            btnFav.Padding = new Padding(6, 2, 6, 2);
            btnFav.TabIndex = 4;
            btnFav.Text = "Favorite / Unfavorite";
            btnFav.UseVisualStyleBackColor = true;
            btnFav.Click += btnFav_Click;
            //
            // btnApprove
            //
            btnApprove.AutoSize = true;
            btnApprove.Name = "btnApprove";
            btnApprove.Padding = new Padding(6, 2, 6, 2);
            btnApprove.TabIndex = 5;
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = true;
            btnApprove.Click += btnApprove_Click;
            //
            // btnReject
            //
            btnReject.AutoSize = true;
            btnReject.Name = "btnReject";
            btnReject.Padding = new Padding(6, 2, 6, 2);
            btnReject.TabIndex = 6;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = true;
            btnReject.Click += btnReject_Click;
            //
            // split
            //
            split.Dock = DockStyle.Fill;
            split.Name = "split";
            //
            // split.Panel1
            //
            split.Panel1.Controls.Add(grid);
            //
            // split.Panel2
            //
            split.Panel2.Controls.Add(txtDetail);
            split.Panel2.Controls.Add(pic);
            split.Size = new Size(984, 532);
            split.SplitterDistance = 540;
            split.TabIndex = 0;
            //
            // grid
            //
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Dock = DockStyle.Fill;
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.TabIndex = 0;
            grid.SelectionChanged += grid_SelectionChanged;
            //
            // txtDetail
            //
            txtDetail.Dock = DockStyle.Fill;
            txtDetail.Multiline = true;
            txtDetail.Name = "txtDetail";
            txtDetail.ReadOnly = true;
            txtDetail.ScrollBars = ScrollBars.Vertical;
            txtDetail.TabIndex = 1;
            //
            // pic
            //
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Dock = DockStyle.Top;
            pic.Name = "pic";
            pic.Size = new Size(440, 200);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.TabStop = false;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 620);
            Controls.Add(split);
            Controls.Add(topPanel);
            Controls.Add(bottomPanel);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Smart Recipe Book";
            Load += MainForm_Load;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            split.Panel1.ResumeLayout(false);
            split.Panel2.ResumeLayout(false);
            split.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)split).EndInit();
            split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel topPanel;
        private ComboBox cboScope;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnNotes;
        private Button btnLogout;
        private FlowLayoutPanel bottomPanel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnSubmit;
        private Button btnDelete;
        private Button btnFav;
        private Button btnApprove;
        private Button btnReject;
        private SplitContainer split;
        private DataGridView grid;
        private TextBox txtDetail;
        private PictureBox pic;
    }
}
