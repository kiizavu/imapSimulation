
namespace Client
{
    partial class MailItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailItem));
            this.lbFrom = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbSubject = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbDate = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbBody = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbUid = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbUnSeen = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.checkBox = new Bunifu.Framework.UI.BunifuCheckbox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFrom
            // 
            this.lbFrom.AutoSize = true;
            this.lbFrom.Enabled = false;
            this.lbFrom.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFrom.ForeColor = System.Drawing.Color.Silver;
            this.lbFrom.Location = new System.Drawing.Point(91, 23);
            this.lbFrom.Name = "lbFrom";
            this.lbFrom.Size = new System.Drawing.Size(51, 21);
            this.lbFrom.TabIndex = 0;
            this.lbFrom.Text = "Form";
            this.lbFrom.MouseEnter += new System.EventHandler(this.MailItem_MouseEnter);
            this.lbFrom.MouseLeave += new System.EventHandler(this.MailItem_MouseLeave);
            // 
            // lbSubject
            // 
            this.lbSubject.AutoEllipsis = true;
            this.lbSubject.Enabled = false;
            this.lbSubject.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubject.ForeColor = System.Drawing.Color.White;
            this.lbSubject.Location = new System.Drawing.Point(89, 50);
            this.lbSubject.Name = "lbSubject";
            this.lbSubject.Size = new System.Drawing.Size(445, 22);
            this.lbSubject.TabIndex = 2;
            this.lbSubject.Text = "Subject";
            this.lbSubject.UseCompatibleTextRendering = true;
            this.lbSubject.MouseEnter += new System.EventHandler(this.MailItem_MouseEnter);
            this.lbSubject.MouseLeave += new System.EventHandler(this.MailItem_MouseLeave);
            // 
            // lbDate
            // 
            this.lbDate.Enabled = false;
            this.lbDate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDate.ForeColor = System.Drawing.Color.Silver;
            this.lbDate.Location = new System.Drawing.Point(467, 23);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(115, 17);
            this.lbDate.TabIndex = 4;
            this.lbDate.Text = "Date";
            this.lbDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbDate.UseCompatibleTextRendering = true;
            this.lbDate.MouseEnter += new System.EventHandler(this.MailItem_MouseEnter);
            this.lbDate.MouseLeave += new System.EventHandler(this.MailItem_MouseLeave);
            // 
            // lbBody
            // 
            this.lbBody.AutoEllipsis = true;
            this.lbBody.Enabled = false;
            this.lbBody.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBody.ForeColor = System.Drawing.Color.Silver;
            this.lbBody.Location = new System.Drawing.Point(89, 89);
            this.lbBody.Name = "lbBody";
            this.lbBody.Size = new System.Drawing.Size(445, 64);
            this.lbBody.TabIndex = 5;
            this.lbBody.Text = "Body";
            this.lbBody.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbBody.UseCompatibleTextRendering = true;
            this.lbBody.Click += new System.EventHandler(this.lbBody_Click);
            this.lbBody.MouseEnter += new System.EventHandler(this.MailItem_MouseEnter);
            this.lbBody.MouseLeave += new System.EventHandler(this.MailItem_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.panel1.Location = new System.Drawing.Point(0, 175);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 2);
            this.panel1.TabIndex = 6;
            // 
            // lbUid
            // 
            this.lbUid.AutoSize = true;
            this.lbUid.Enabled = false;
            this.lbUid.Location = new System.Drawing.Point(13, 14);
            this.lbUid.Name = "lbUid";
            this.lbUid.Size = new System.Drawing.Size(27, 17);
            this.lbUid.TabIndex = 7;
            this.lbUid.Text = "uid";
            this.lbUid.Visible = false;
            // 
            // lbUnSeen
            // 
            this.lbUnSeen.AutoSize = true;
            this.lbUnSeen.Enabled = false;
            this.lbUnSeen.Location = new System.Drawing.Point(195, 6);
            this.lbUnSeen.Name = "lbUnSeen";
            this.lbUnSeen.Size = new System.Drawing.Size(59, 17);
            this.lbUnSeen.TabIndex = 9;
            this.lbUnSeen.Text = "UnSeen";
            this.lbUnSeen.Visible = false;
            // 
            // checkBox
            // 
            this.checkBox.BackColor = System.Drawing.Color.White;
            this.checkBox.ChechedOffColor = System.Drawing.Color.White;
            this.checkBox.Checked = false;
            this.checkBox.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(168)))), ((int)(((byte)(244)))));
            this.checkBox.ForeColor = System.Drawing.Color.White;
            this.checkBox.Location = new System.Drawing.Point(36, 73);
            this.checkBox.Margin = new System.Windows.Forms.Padding(5);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(20, 20);
            this.checkBox.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.moveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 82);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(72)))), ((int)(((byte)(77)))));
            this.copyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.MouseHover += new System.EventHandler(this.copyToolStripMenuItem_MouseHover);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(72)))), ((int)(((byte)(77)))));
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(72)))), ((int)(((byte)(77)))));
            this.moveToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.moveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("moveToolStripMenuItem.Image")));
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(126, 26);
            this.moveToolStripMenuItem.Text = "Move";
            this.moveToolStripMenuItem.MouseHover += new System.EventHandler(this.moveToolStripMenuItem_MouseHover);
            // 
            // MailItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(52)))), ((int)(((byte)(57)))));
            this.Controls.Add(this.lbUnSeen);
            this.Controls.Add(this.lbUid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbBody);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lbSubject);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.lbFrom);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MailItem";
            this.Size = new System.Drawing.Size(584, 176);
            this.Click += new System.EventHandler(this.MailItem_Click);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MailItem_MouseClick);
            this.MouseEnter += new System.EventHandler(this.MailItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MailItem_MouseLeave);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel lbFrom;
        private Bunifu.Framework.UI.BunifuCustomLabel lbSubject;
        private Bunifu.Framework.UI.BunifuCustomLabel lbDate;
        private Bunifu.Framework.UI.BunifuCustomLabel lbBody;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuCustomLabel lbUid;
        private Bunifu.Framework.UI.BunifuCustomLabel lbUnSeen;
        private Bunifu.Framework.UI.BunifuCheckbox checkBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
    }
}
