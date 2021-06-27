
namespace Client
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.pnlFolder = new System.Windows.Forms.Panel();
            this.bunifuDropdown1 = new Bunifu.Framework.UI.BunifuDropdown();
            this.bunifuSeparator5 = new Bunifu.Framework.UI.BunifuSeparator();
            this.btnLogOut = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnTrash = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnSpam = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnStarred = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnImportant = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnDrafts = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnSent = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnAll = new Bunifu.Framework.UI.BunifuFlatButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuSeparator4 = new Bunifu.Framework.UI.BunifuSeparator();
            this.bunifuSeparator2 = new Bunifu.Framework.UI.BunifuSeparator();
            this.bunifuSeparator3 = new Bunifu.Framework.UI.BunifuSeparator();
            this.lbTilte = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lbNoMail = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pnlContain = new System.Windows.Forms.Panel();
            this.rtbFrom = new System.Windows.Forms.RichTextBox();
            this.lbNoMailSelect = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.rtbBody = new System.Windows.Forms.RichTextBox();
            this.lbSubject = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbDate = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btnClose = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnMinimize = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFoldderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CreateFolBtn = new Bunifu.Framework.UI.BunifuImageButton();
            this.pnlFolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.pnlContain.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CreateFolBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this;
            this.bunifuDragControl1.Vertical = true;
            // 
            // pnlFolder
            // 
            this.pnlFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.pnlFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFolder.Controls.Add(this.bunifuDropdown1);
            this.pnlFolder.Controls.Add(this.bunifuSeparator5);
            this.pnlFolder.Controls.Add(this.btnLogOut);
            this.pnlFolder.Controls.Add(this.btnTrash);
            this.pnlFolder.Controls.Add(this.btnSpam);
            this.pnlFolder.Controls.Add(this.btnStarred);
            this.pnlFolder.Controls.Add(this.btnImportant);
            this.pnlFolder.Controls.Add(this.btnDrafts);
            this.pnlFolder.Controls.Add(this.btnSent);
            this.pnlFolder.Controls.Add(this.btnAll);
            this.pnlFolder.Controls.Add(this.pictureBox1);
            this.pnlFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFolder.Location = new System.Drawing.Point(0, 0);
            this.pnlFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlFolder.Name = "pnlFolder";
            this.pnlFolder.Size = new System.Drawing.Size(783, 940);
            this.pnlFolder.TabIndex = 1;
            this.pnlFolder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlFolder_MouseClick);
            this.pnlFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this._MouseDown);
            this.pnlFolder.MouseMove += new System.Windows.Forms.MouseEventHandler(this._MouseMove);
            this.pnlFolder.MouseUp += new System.Windows.Forms.MouseEventHandler(this._MouseUp);
            // 
            // bunifuDropdown1
            // 
            this.bunifuDropdown1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuDropdown1.BorderRadius = 3;
            this.bunifuDropdown1.ForeColor = System.Drawing.Color.White;
            this.bunifuDropdown1.Items = new string[0];
            this.bunifuDropdown1.Location = new System.Drawing.Point(13, 722);
            this.bunifuDropdown1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuDropdown1.Name = "bunifuDropdown1";
            this.bunifuDropdown1.NomalColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuDropdown1.onHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bunifuDropdown1.selectedIndex = -1;
            this.bunifuDropdown1.Size = new System.Drawing.Size(174, 43);
            this.bunifuDropdown1.TabIndex = 20;
            // 
            // bunifuSeparator5
            // 
            this.bunifuSeparator5.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator5.ForeColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator5.LineThickness = 1;
            this.bunifuSeparator5.Location = new System.Drawing.Point(196, 0);
            this.bunifuSeparator5.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuSeparator5.Name = "bunifuSeparator5";
            this.bunifuSeparator5.Size = new System.Drawing.Size(1, 940);
            this.bunifuSeparator5.TabIndex = 19;
            this.bunifuSeparator5.Transparency = 255;
            this.bunifuSeparator5.Vertical = true;
            // 
            // btnLogOut
            // 
            this.btnLogOut.Activecolor = System.Drawing.Color.Red;
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnLogOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogOut.BorderRadius = 5;
            this.btnLogOut.ButtonText = "Log Out";
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.DisabledColor = System.Drawing.Color.Gray;
            this.btnLogOut.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Iconcolor = System.Drawing.Color.Transparent;
            this.btnLogOut.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnLogOut.Iconimage")));
            this.btnLogOut.Iconimage_right = null;
            this.btnLogOut.Iconimage_right_Selected = null;
            this.btnLogOut.Iconimage_Selected = null;
            this.btnLogOut.IconMarginLeft = 0;
            this.btnLogOut.IconMarginRight = 0;
            this.btnLogOut.IconRightVisible = true;
            this.btnLogOut.IconRightZoom = 0D;
            this.btnLogOut.IconVisible = true;
            this.btnLogOut.IconZoom = 55D;
            this.btnLogOut.IsTab = false;
            this.btnLogOut.Location = new System.Drawing.Point(27, 863);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnLogOut.OnHovercolor = System.Drawing.Color.Red;
            this.btnLogOut.OnHoverTextColor = System.Drawing.Color.White;
            this.btnLogOut.selected = false;
            this.btnLogOut.Size = new System.Drawing.Size(151, 39);
            this.btnLogOut.TabIndex = 18;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogOut.Textcolor = System.Drawing.Color.White;
            this.btnLogOut.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnTrash
            // 
            this.btnTrash.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.btnTrash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnTrash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTrash.BorderRadius = 5;
            this.btnTrash.ButtonText = "Trash";
            this.btnTrash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrash.DisabledColor = System.Drawing.Color.Gray;
            this.btnTrash.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrash.ForeColor = System.Drawing.Color.White;
            this.btnTrash.Iconcolor = System.Drawing.Color.Transparent;
            this.btnTrash.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnTrash.Iconimage")));
            this.btnTrash.Iconimage_right = null;
            this.btnTrash.Iconimage_right_Selected = null;
            this.btnTrash.Iconimage_Selected = null;
            this.btnTrash.IconMarginLeft = 0;
            this.btnTrash.IconMarginRight = 0;
            this.btnTrash.IconRightVisible = true;
            this.btnTrash.IconRightZoom = 0D;
            this.btnTrash.IconVisible = true;
            this.btnTrash.IconZoom = 55D;
            this.btnTrash.IsTab = false;
            this.btnTrash.Location = new System.Drawing.Point(27, 673);
            this.btnTrash.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnTrash.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnTrash.OnHoverTextColor = System.Drawing.Color.White;
            this.btnTrash.selected = false;
            this.btnTrash.Size = new System.Drawing.Size(151, 39);
            this.btnTrash.TabIndex = 17;
            this.btnTrash.Text = "Trash";
            this.btnTrash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTrash.Textcolor = System.Drawing.Color.White;
            this.btnTrash.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrash.Click += new System.EventHandler(this.btnTrash_Click);
            // 
            // btnSpam
            // 
            this.btnSpam.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.btnSpam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnSpam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpam.BorderRadius = 5;
            this.btnSpam.ButtonText = "Spam";
            this.btnSpam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpam.DisabledColor = System.Drawing.Color.Gray;
            this.btnSpam.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpam.ForeColor = System.Drawing.Color.White;
            this.btnSpam.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSpam.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnSpam.Iconimage")));
            this.btnSpam.Iconimage_right = null;
            this.btnSpam.Iconimage_right_Selected = null;
            this.btnSpam.Iconimage_Selected = null;
            this.btnSpam.IconMarginLeft = 0;
            this.btnSpam.IconMarginRight = 0;
            this.btnSpam.IconRightVisible = true;
            this.btnSpam.IconRightZoom = 0D;
            this.btnSpam.IconVisible = true;
            this.btnSpam.IconZoom = 55D;
            this.btnSpam.IsTab = false;
            this.btnSpam.Location = new System.Drawing.Point(27, 617);
            this.btnSpam.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSpam.Name = "btnSpam";
            this.btnSpam.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnSpam.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnSpam.OnHoverTextColor = System.Drawing.Color.White;
            this.btnSpam.selected = false;
            this.btnSpam.Size = new System.Drawing.Size(151, 39);
            this.btnSpam.TabIndex = 16;
            this.btnSpam.Text = "Spam";
            this.btnSpam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSpam.Textcolor = System.Drawing.Color.White;
            this.btnSpam.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpam.Click += new System.EventHandler(this.btnSpam_Click);
            // 
            // btnStarred
            // 
            this.btnStarred.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.btnStarred.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnStarred.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStarred.BorderRadius = 5;
            this.btnStarred.ButtonText = "Starred";
            this.btnStarred.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStarred.DisabledColor = System.Drawing.Color.Gray;
            this.btnStarred.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStarred.ForeColor = System.Drawing.Color.White;
            this.btnStarred.Iconcolor = System.Drawing.Color.Transparent;
            this.btnStarred.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnStarred.Iconimage")));
            this.btnStarred.Iconimage_right = null;
            this.btnStarred.Iconimage_right_Selected = null;
            this.btnStarred.Iconimage_Selected = null;
            this.btnStarred.IconMarginLeft = 0;
            this.btnStarred.IconMarginRight = 0;
            this.btnStarred.IconRightVisible = true;
            this.btnStarred.IconRightZoom = 0D;
            this.btnStarred.IconVisible = true;
            this.btnStarred.IconZoom = 55D;
            this.btnStarred.IsTab = false;
            this.btnStarred.Location = new System.Drawing.Point(27, 559);
            this.btnStarred.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnStarred.Name = "btnStarred";
            this.btnStarred.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnStarred.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnStarred.OnHoverTextColor = System.Drawing.Color.White;
            this.btnStarred.selected = false;
            this.btnStarred.Size = new System.Drawing.Size(151, 39);
            this.btnStarred.TabIndex = 15;
            this.btnStarred.Text = "Starred";
            this.btnStarred.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnStarred.Textcolor = System.Drawing.Color.White;
            this.btnStarred.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStarred.Click += new System.EventHandler(this.btnStarred_Click);
            // 
            // btnImportant
            // 
            this.btnImportant.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.btnImportant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnImportant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportant.BorderRadius = 5;
            this.btnImportant.ButtonText = "Important";
            this.btnImportant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportant.DisabledColor = System.Drawing.Color.Gray;
            this.btnImportant.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportant.ForeColor = System.Drawing.Color.White;
            this.btnImportant.Iconcolor = System.Drawing.Color.Transparent;
            this.btnImportant.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnImportant.Iconimage")));
            this.btnImportant.Iconimage_right = null;
            this.btnImportant.Iconimage_right_Selected = null;
            this.btnImportant.Iconimage_Selected = null;
            this.btnImportant.IconMarginLeft = 0;
            this.btnImportant.IconMarginRight = 0;
            this.btnImportant.IconRightVisible = true;
            this.btnImportant.IconRightZoom = 0D;
            this.btnImportant.IconVisible = true;
            this.btnImportant.IconZoom = 55D;
            this.btnImportant.IsTab = false;
            this.btnImportant.Location = new System.Drawing.Point(27, 502);
            this.btnImportant.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnImportant.Name = "btnImportant";
            this.btnImportant.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnImportant.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnImportant.OnHoverTextColor = System.Drawing.Color.White;
            this.btnImportant.selected = false;
            this.btnImportant.Size = new System.Drawing.Size(151, 39);
            this.btnImportant.TabIndex = 14;
            this.btnImportant.Text = "Important";
            this.btnImportant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnImportant.Textcolor = System.Drawing.Color.White;
            this.btnImportant.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportant.Click += new System.EventHandler(this.btnImportant_Click);
            // 
            // btnDrafts
            // 
            this.btnDrafts.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.btnDrafts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnDrafts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDrafts.BorderRadius = 5;
            this.btnDrafts.ButtonText = "Drafts";
            this.btnDrafts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDrafts.DisabledColor = System.Drawing.Color.Gray;
            this.btnDrafts.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrafts.ForeColor = System.Drawing.Color.White;
            this.btnDrafts.Iconcolor = System.Drawing.Color.Transparent;
            this.btnDrafts.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnDrafts.Iconimage")));
            this.btnDrafts.Iconimage_right = null;
            this.btnDrafts.Iconimage_right_Selected = null;
            this.btnDrafts.Iconimage_Selected = null;
            this.btnDrafts.IconMarginLeft = 0;
            this.btnDrafts.IconMarginRight = 0;
            this.btnDrafts.IconRightVisible = true;
            this.btnDrafts.IconRightZoom = 0D;
            this.btnDrafts.IconVisible = true;
            this.btnDrafts.IconZoom = 55D;
            this.btnDrafts.IsTab = false;
            this.btnDrafts.Location = new System.Drawing.Point(27, 446);
            this.btnDrafts.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnDrafts.Name = "btnDrafts";
            this.btnDrafts.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnDrafts.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnDrafts.OnHoverTextColor = System.Drawing.Color.White;
            this.btnDrafts.selected = false;
            this.btnDrafts.Size = new System.Drawing.Size(151, 39);
            this.btnDrafts.TabIndex = 13;
            this.btnDrafts.Text = "Drafts";
            this.btnDrafts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDrafts.Textcolor = System.Drawing.Color.White;
            this.btnDrafts.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrafts.Click += new System.EventHandler(this.btnDrafts_Click);
            // 
            // btnSent
            // 
            this.btnSent.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.btnSent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnSent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSent.BorderRadius = 5;
            this.btnSent.ButtonText = "Sent";
            this.btnSent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSent.DisabledColor = System.Drawing.Color.Gray;
            this.btnSent.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSent.ForeColor = System.Drawing.Color.White;
            this.btnSent.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSent.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnSent.Iconimage")));
            this.btnSent.Iconimage_right = null;
            this.btnSent.Iconimage_right_Selected = null;
            this.btnSent.Iconimage_Selected = null;
            this.btnSent.IconMarginLeft = 0;
            this.btnSent.IconMarginRight = 0;
            this.btnSent.IconRightVisible = true;
            this.btnSent.IconRightZoom = 0D;
            this.btnSent.IconVisible = true;
            this.btnSent.IconZoom = 55D;
            this.btnSent.IsTab = false;
            this.btnSent.Location = new System.Drawing.Point(27, 388);
            this.btnSent.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSent.Name = "btnSent";
            this.btnSent.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnSent.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnSent.OnHoverTextColor = System.Drawing.Color.White;
            this.btnSent.selected = false;
            this.btnSent.Size = new System.Drawing.Size(151, 39);
            this.btnSent.TabIndex = 12;
            this.btnSent.Text = "Sent";
            this.btnSent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSent.Textcolor = System.Drawing.Color.White;
            this.btnSent.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSent.Click += new System.EventHandler(this.btnSent_Click);
            // 
            // btnAll
            // 
            this.btnAll.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAll.BorderRadius = 5;
            this.btnAll.ButtonText = "All Mail";
            this.btnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAll.DisabledColor = System.Drawing.Color.Gray;
            this.btnAll.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll.ForeColor = System.Drawing.Color.White;
            this.btnAll.Iconcolor = System.Drawing.Color.Transparent;
            this.btnAll.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnAll.Iconimage")));
            this.btnAll.Iconimage_right = null;
            this.btnAll.Iconimage_right_Selected = null;
            this.btnAll.Iconimage_Selected = null;
            this.btnAll.IconMarginLeft = 0;
            this.btnAll.IconMarginRight = 0;
            this.btnAll.IconRightVisible = true;
            this.btnAll.IconRightZoom = 0D;
            this.btnAll.IconVisible = true;
            this.btnAll.IconZoom = 55D;
            this.btnAll.IsTab = false;
            this.btnAll.Location = new System.Drawing.Point(27, 331);
            this.btnAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAll.Name = "btnAll";
            this.btnAll.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.btnAll.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnAll.OnHoverTextColor = System.Drawing.Color.White;
            this.btnAll.selected = false;
            this.btnAll.Size = new System.Drawing.Size(151, 39);
            this.btnAll.TabIndex = 11;
            this.btnAll.Text = "All Mail";
            this.btnAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAll.Textcolor = System.Drawing.Color.White;
            this.btnAll.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 258);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.panel2.Controls.Add(this.CreateFolBtn);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.bunifuSeparator4);
            this.panel2.Controls.Add(this.bunifuSeparator2);
            this.panel2.Controls.Add(this.bunifuSeparator3);
            this.panel2.Controls.Add(this.lbTilte);
            this.panel2.Controls.Add(this.pnlContainer);
            this.panel2.Location = new System.Drawing.Point(196, 28);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(587, 912);
            this.panel2.TabIndex = 1;
            // 
            // bunifuSeparator4
            // 
            this.bunifuSeparator4.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator4.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator4.LineThickness = 1;
            this.bunifuSeparator4.Location = new System.Drawing.Point(0, 220);
            this.bunifuSeparator4.Margin = new System.Windows.Forms.Padding(5);
            this.bunifuSeparator4.Name = "bunifuSeparator4";
            this.bunifuSeparator4.Size = new System.Drawing.Size(587, 1);
            this.bunifuSeparator4.TabIndex = 4;
            this.bunifuSeparator4.Transparency = 255;
            this.bunifuSeparator4.Vertical = false;
            // 
            // bunifuSeparator2
            // 
            this.bunifuSeparator2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator2.ForeColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator2.LineThickness = 1;
            this.bunifuSeparator2.Location = new System.Drawing.Point(587, 0);
            this.bunifuSeparator2.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuSeparator2.Name = "bunifuSeparator2";
            this.bunifuSeparator2.Size = new System.Drawing.Size(1, 940);
            this.bunifuSeparator2.TabIndex = 3;
            this.bunifuSeparator2.Transparency = 255;
            this.bunifuSeparator2.Vertical = true;
            // 
            // bunifuSeparator3
            // 
            this.bunifuSeparator3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator3.ForeColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator3.LineThickness = 1;
            this.bunifuSeparator3.Location = new System.Drawing.Point(0, 0);
            this.bunifuSeparator3.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuSeparator3.Name = "bunifuSeparator3";
            this.bunifuSeparator3.Size = new System.Drawing.Size(1, 940);
            this.bunifuSeparator3.TabIndex = 2;
            this.bunifuSeparator3.Transparency = 255;
            this.bunifuSeparator3.Vertical = true;
            // 
            // lbTilte
            // 
            this.lbTilte.AutoSize = true;
            this.lbTilte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.lbTilte.Font = new System.Drawing.Font("Times New Roman", 40.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTilte.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbTilte.Location = new System.Drawing.Point(11, 62);
            this.lbTilte.Name = "lbTilte";
            this.lbTilte.Size = new System.Drawing.Size(202, 77);
            this.lbTilte.TabIndex = 1;
            this.lbTilte.Text = "Inbox";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.lbNoMail);
            this.pnlContainer.Location = new System.Drawing.Point(3, 223);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(584, 692);
            this.pnlContainer.TabIndex = 0;
            // 
            // lbNoMail
            // 
            this.lbNoMail.AutoSize = true;
            this.lbNoMail.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNoMail.ForeColor = System.Drawing.Color.DimGray;
            this.lbNoMail.Location = new System.Drawing.Point(197, 80);
            this.lbNoMail.Name = "lbNoMail";
            this.lbNoMail.Size = new System.Drawing.Size(189, 34);
            this.lbNoMail.TabIndex = 0;
            this.lbNoMail.Text = "No mails here!";
            // 
            // pnlContain
            // 
            this.pnlContain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.pnlContain.Controls.Add(this.rtbFrom);
            this.pnlContain.Controls.Add(this.lbNoMailSelect);
            this.pnlContain.Controls.Add(this.rtbBody);
            this.pnlContain.Controls.Add(this.lbSubject);
            this.pnlContain.Location = new System.Drawing.Point(789, 28);
            this.pnlContain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlContain.Name = "pnlContain";
            this.pnlContain.Size = new System.Drawing.Size(811, 914);
            this.pnlContain.TabIndex = 2;
            // 
            // rtbFrom
            // 
            this.rtbFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.rtbFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFrom.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbFrom.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rtbFrom.Location = new System.Drawing.Point(43, 207);
            this.rtbFrom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbFrom.Name = "rtbFrom";
            this.rtbFrom.Size = new System.Drawing.Size(740, 30);
            this.rtbFrom.TabIndex = 7;
            this.rtbFrom.Text = "From";
            // 
            // lbNoMailSelect
            // 
            this.lbNoMailSelect.AutoSize = true;
            this.lbNoMailSelect.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNoMailSelect.ForeColor = System.Drawing.Color.DarkGray;
            this.lbNoMailSelect.Location = new System.Drawing.Point(275, 300);
            this.lbNoMailSelect.Name = "lbNoMailSelect";
            this.lbNoMailSelect.Size = new System.Drawing.Size(251, 37);
            this.lbNoMailSelect.TabIndex = 6;
            this.lbNoMailSelect.Text = "No mails Selected";
            // 
            // rtbBody
            // 
            this.rtbBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.rtbBody.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbBody.Enabled = false;
            this.rtbBody.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbBody.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rtbBody.Location = new System.Drawing.Point(43, 263);
            this.rtbBody.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbBody.Name = "rtbBody";
            this.rtbBody.ReadOnly = true;
            this.rtbBody.Size = new System.Drawing.Size(736, 635);
            this.rtbBody.TabIndex = 2;
            this.rtbBody.Text = "Body";
            this.rtbBody.Visible = false;
            // 
            // lbSubject
            // 
            this.lbSubject.Enabled = false;
            this.lbSubject.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubject.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbSubject.Location = new System.Drawing.Point(35, 48);
            this.lbSubject.Name = "lbSubject";
            this.lbSubject.Size = new System.Drawing.Size(748, 139);
            this.lbSubject.TabIndex = 0;
            this.lbSubject.Text = "Subject";
            this.lbSubject.Visible = false;
            // 
            // lbDate
            // 
            this.lbDate.Enabled = false;
            this.lbDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDate.ForeColor = System.Drawing.Color.DimGray;
            this.lbDate.Location = new System.Drawing.Point(1323, 9);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(209, 23);
            this.lbDate.TabIndex = 5;
            this.lbDate.Text = "Date";
            this.lbDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbDate.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Activecolor = System.Drawing.Color.Red;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.BorderRadius = 0;
            this.btnClose.ButtonText = "";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DisabledColor = System.Drawing.Color.Gray;
            this.btnClose.Iconcolor = System.Drawing.Color.Transparent;
            this.btnClose.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnClose.Iconimage")));
            this.btnClose.Iconimage_right = null;
            this.btnClose.Iconimage_right_Selected = null;
            this.btnClose.Iconimage_Selected = null;
            this.btnClose.IconMarginLeft = 0;
            this.btnClose.IconMarginRight = 0;
            this.btnClose.IconRightVisible = true;
            this.btnClose.IconRightZoom = 0D;
            this.btnClose.IconVisible = true;
            this.btnClose.IconZoom = 25D;
            this.btnClose.IsTab = false;
            this.btnClose.Location = new System.Drawing.Point(1565, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnClose.OnHovercolor = System.Drawing.Color.Red;
            this.btnClose.OnHoverTextColor = System.Drawing.Color.White;
            this.btnClose.selected = false;
            this.btnClose.Size = new System.Drawing.Size(29, 30);
            this.btnClose.TabIndex = 10;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Textcolor = System.Drawing.Color.White;
            this.btnClose.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Activecolor = System.Drawing.Color.Gray;
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimize.BorderRadius = 0;
            this.btnMinimize.ButtonText = "";
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.DisabledColor = System.Drawing.Color.Gray;
            this.btnMinimize.Iconcolor = System.Drawing.Color.Transparent;
            this.btnMinimize.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Iconimage")));
            this.btnMinimize.Iconimage_right = null;
            this.btnMinimize.Iconimage_right_Selected = null;
            this.btnMinimize.Iconimage_Selected = null;
            this.btnMinimize.IconMarginLeft = 0;
            this.btnMinimize.IconMarginRight = 0;
            this.btnMinimize.IconRightVisible = true;
            this.btnMinimize.IconRightZoom = 0D;
            this.btnMinimize.IconVisible = true;
            this.btnMinimize.IconZoom = 25D;
            this.btnMinimize.IsTab = false;
            this.btnMinimize.Location = new System.Drawing.Point(1539, 2);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(5);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.btnMinimize.OnHovercolor = System.Drawing.Color.Gray;
            this.btnMinimize.OnHoverTextColor = System.Drawing.Color.White;
            this.btnMinimize.selected = false;
            this.btnMinimize.Size = new System.Drawing.Size(29, 30);
            this.btnMinimize.TabIndex = 11;
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMinimize.Textcolor = System.Drawing.Color.White;
            this.btnMinimize.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.ForeColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(781, 0);
            this.bunifuSeparator1.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(1, 940);
            this.bunifuSeparator1.TabIndex = 12;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFoldderToolStripMenuItem,
            this.deleteFolderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 56);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(72)))), ((int)(((byte)(77)))));
            this.deleteFolderToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteFolderToolStripMenuItem.Image")));
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.deleteFolderToolStripMenuItem.Text = "Delete Folder";
            this.deleteFolderToolStripMenuItem.MouseHover += new System.EventHandler(this.deleteFolderToolStripMenuItem_MouseHover);
            // 
            // createFoldderToolStripMenuItem
            // 
            this.createFoldderToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(72)))), ((int)(((byte)(77)))));
            this.createFoldderToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.createFoldderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createFoldderToolStripMenuItem.Image")));
            this.createFoldderToolStripMenuItem.Name = "createFoldderToolStripMenuItem";
            this.createFoldderToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.createFoldderToolStripMenuItem.Text = "Create Foldder";
            this.createFoldderToolStripMenuItem.Click += new System.EventHandler(this.createFoldderToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(129, 155);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(289, 30);
            this.textBox1.TabIndex = 5;
            this.textBox1.Visible = false;
            // 
            // CreateFolBtn
            // 
            this.CreateFolBtn.BackColor = System.Drawing.Color.Transparent;
            this.CreateFolBtn.Image = ((System.Drawing.Image)(resources.GetObject("CreateFolBtn.Image")));
            this.CreateFolBtn.ImageActive = ((System.Drawing.Image)(resources.GetObject("CreateFolBtn.ImageActive")));
            this.CreateFolBtn.Location = new System.Drawing.Point(55, 155);
            this.CreateFolBtn.Name = "CreateFolBtn";
            this.CreateFolBtn.Size = new System.Drawing.Size(36, 32);
            this.CreateFolBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CreateFolBtn.TabIndex = 6;
            this.CreateFolBtn.TabStop = false;
            this.CreateFolBtn.Visible = false;
            this.CreateFolBtn.Zoom = 10;
            this.CreateFolBtn.Click += new System.EventHandler(this.CreateFolBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(1600, 940);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlFolder);
            this.Controls.Add(this.pnlContain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.pnlFolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.pnlContain.ResumeLayout(false);
            this.pnlContain.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CreateFolBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlFolder;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel pnlContain;
        private System.Windows.Forms.Panel pnlContainer;
        private Bunifu.Framework.UI.BunifuFlatButton btnMinimize;
        private Bunifu.Framework.UI.BunifuFlatButton btnClose;
        private Bunifu.Framework.UI.BunifuCustomLabel lbTilte;
        private System.Windows.Forms.RichTextBox rtbBody;
        private Bunifu.Framework.UI.BunifuCustomLabel lbSubject;
        private Bunifu.Framework.UI.BunifuCustomLabel lbDate;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator3;
        private Bunifu.Framework.UI.BunifuFlatButton btnTrash;
        private Bunifu.Framework.UI.BunifuFlatButton btnSpam;
        private Bunifu.Framework.UI.BunifuFlatButton btnStarred;
        private Bunifu.Framework.UI.BunifuFlatButton btnImportant;
        private Bunifu.Framework.UI.BunifuFlatButton btnDrafts;
        private Bunifu.Framework.UI.BunifuFlatButton btnSent;
        private Bunifu.Framework.UI.BunifuFlatButton btnAll;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator2;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator4;
        private Bunifu.Framework.UI.BunifuFlatButton btnLogOut;
        private Bunifu.Framework.UI.BunifuCustomLabel lbNoMail;
        private Bunifu.Framework.UI.BunifuCustomLabel lbNoMailSelect;
        private System.Windows.Forms.RichTextBox rtbFrom;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator5;
        private Bunifu.Framework.UI.BunifuDropdown bunifuDropdown1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createFoldderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private Bunifu.Framework.UI.BunifuImageButton CreateFolBtn;
        private System.Windows.Forms.TextBox textBox1;
    }
}