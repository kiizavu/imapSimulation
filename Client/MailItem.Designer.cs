
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
            this.lbFrom = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbSubject = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbDate = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbBody = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbUid = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbUnSeen = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.checkBox = new Bunifu.Framework.UI.BunifuCheckbox();
            this.SuspendLayout();
            // 
            // lbFrom
            // 
            this.lbFrom.AutoSize = true;
            this.lbFrom.Enabled = false;
            this.lbFrom.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFrom.ForeColor = System.Drawing.Color.Silver;
            this.lbFrom.Location = new System.Drawing.Point(68, 19);
            this.lbFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFrom.Name = "lbFrom";
            this.lbFrom.Size = new System.Drawing.Size(39, 17);
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
            this.lbSubject.Location = new System.Drawing.Point(67, 41);
            this.lbSubject.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSubject.Name = "lbSubject";
            this.lbSubject.Size = new System.Drawing.Size(334, 18);
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
            this.lbDate.Location = new System.Drawing.Point(350, 19);
            this.lbDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(86, 14);
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
            this.lbBody.Location = new System.Drawing.Point(67, 72);
            this.lbBody.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbBody.Name = "lbBody";
            this.lbBody.Size = new System.Drawing.Size(334, 52);
            this.lbBody.TabIndex = 5;
            this.lbBody.Text = "Body";
            this.lbBody.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbBody.UseCompatibleTextRendering = true;
            this.lbBody.MouseEnter += new System.EventHandler(this.MailItem_MouseEnter);
            this.lbBody.MouseLeave += new System.EventHandler(this.MailItem_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.panel1.Location = new System.Drawing.Point(0, 142);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 2);
            this.panel1.TabIndex = 6;
            // 
            // lbUid
            // 
            this.lbUid.AutoSize = true;
            this.lbUid.Enabled = false;
            this.lbUid.Location = new System.Drawing.Point(10, 11);
            this.lbUid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUid.Name = "lbUid";
            this.lbUid.Size = new System.Drawing.Size(21, 13);
            this.lbUid.TabIndex = 7;
            this.lbUid.Text = "uid";
            this.lbUid.Visible = false;
            // 
            // lbUnSeen
            // 
            this.lbUnSeen.AutoSize = true;
            this.lbUnSeen.Enabled = false;
            this.lbUnSeen.Location = new System.Drawing.Point(146, 5);
            this.lbUnSeen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUnSeen.Name = "lbUnSeen";
            this.lbUnSeen.Size = new System.Drawing.Size(46, 13);
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
            this.checkBox.Location = new System.Drawing.Point(27, 59);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(20, 20);
            this.checkBox.TabIndex = 1;
            // 
            // MailItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MailItem";
            this.Size = new System.Drawing.Size(438, 143);
            this.Click += new System.EventHandler(this.MailItem_Click);
            this.MouseEnter += new System.EventHandler(this.MailItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MailItem_MouseLeave);
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
    }
}
