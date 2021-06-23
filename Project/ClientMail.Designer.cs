
namespace Project
{
    partial class ClientMail
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAllMail = new System.Windows.Forms.Button();
            this.btnDraft = new System.Windows.Forms.Button();
            this.btnFlagged = new System.Windows.Forms.Button();
            this.btnImportant = new System.Windows.Forms.Button();
            this.btnSent = new System.Windows.Forms.Button();
            this.btnStarred = new System.Windows.Forms.Button();
            this.btnTrash = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(138, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(823, 486);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mails";
            this.columnHeader1.Width = 1000;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(83, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(387, 22);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Path";
            // 
            // btnAllMail
            // 
            this.btnAllMail.Location = new System.Drawing.Point(11, 68);
            this.btnAllMail.Name = "btnAllMail";
            this.btnAllMail.Size = new System.Drawing.Size(121, 36);
            this.btnAllMail.TabIndex = 5;
            this.btnAllMail.Text = "All Mail";
            this.btnAllMail.UseVisualStyleBackColor = true;
            this.btnAllMail.Click += new System.EventHandler(this.btnAllMail_Click);
            // 
            // btnDraft
            // 
            this.btnDraft.Location = new System.Drawing.Point(11, 110);
            this.btnDraft.Name = "btnDraft";
            this.btnDraft.Size = new System.Drawing.Size(121, 36);
            this.btnDraft.TabIndex = 5;
            this.btnDraft.Text = "Draft";
            this.btnDraft.UseVisualStyleBackColor = true;
            this.btnDraft.Click += new System.EventHandler(this.btnDraft_Click);
            // 
            // btnFlagged
            // 
            this.btnFlagged.Location = new System.Drawing.Point(11, 152);
            this.btnFlagged.Name = "btnFlagged";
            this.btnFlagged.Size = new System.Drawing.Size(121, 36);
            this.btnFlagged.TabIndex = 5;
            this.btnFlagged.Text = "Flagged";
            this.btnFlagged.UseVisualStyleBackColor = true;
            this.btnFlagged.Click += new System.EventHandler(this.btnFlagged_Click);
            // 
            // btnImportant
            // 
            this.btnImportant.Location = new System.Drawing.Point(11, 194);
            this.btnImportant.Name = "btnImportant";
            this.btnImportant.Size = new System.Drawing.Size(121, 36);
            this.btnImportant.TabIndex = 5;
            this.btnImportant.Text = "Important";
            this.btnImportant.UseVisualStyleBackColor = true;
            this.btnImportant.Click += new System.EventHandler(this.btnImportant_Click);
            // 
            // btnSent
            // 
            this.btnSent.Location = new System.Drawing.Point(11, 236);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(121, 36);
            this.btnSent.TabIndex = 5;
            this.btnSent.Text = "Sent";
            this.btnSent.UseVisualStyleBackColor = true;
            this.btnSent.Click += new System.EventHandler(this.btnSent_Click);
            // 
            // btnStarred
            // 
            this.btnStarred.Location = new System.Drawing.Point(11, 278);
            this.btnStarred.Name = "btnStarred";
            this.btnStarred.Size = new System.Drawing.Size(121, 36);
            this.btnStarred.TabIndex = 5;
            this.btnStarred.Text = "Starred";
            this.btnStarred.UseVisualStyleBackColor = true;
            this.btnStarred.Click += new System.EventHandler(this.btnStarred_Click);
            // 
            // btnTrash
            // 
            this.btnTrash.Location = new System.Drawing.Point(11, 320);
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Size = new System.Drawing.Size(121, 36);
            this.btnTrash.TabIndex = 5;
            this.btnTrash.Text = "Trash";
            this.btnTrash.UseVisualStyleBackColor = true;
            this.btnTrash.Click += new System.EventHandler(this.btnTrash_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "INBOX";
            // 
            // ClientMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 559);
            this.Controls.Add(this.btnTrash);
            this.Controls.Add(this.btnStarred);
            this.Controls.Add(this.btnSent);
            this.Controls.Add(this.btnImportant);
            this.Controls.Add(this.btnFlagged);
            this.Controls.Add(this.btnDraft);
            this.Controls.Add(this.btnAllMail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView1);
            this.Name = "ClientMail";
            this.Text = "ClientMail";
            this.Load += new System.EventHandler(this.ClienMail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAllMail;
        private System.Windows.Forms.Button btnDraft;
        private System.Windows.Forms.Button btnFlagged;
        private System.Windows.Forms.Button btnImportant;
        private System.Windows.Forms.Button btnSent;
        private System.Windows.Forms.Button btnStarred;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.Label label3;
    }
}