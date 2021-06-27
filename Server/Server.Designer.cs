
namespace Server
{
    partial class Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            this.rtbCommunication = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.btnClose = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnMinimize = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton3 = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbCommunication
            // 
            this.rtbCommunication.BackColor = System.Drawing.Color.White;
            this.rtbCommunication.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCommunication.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCommunication.Location = new System.Drawing.Point(11, 43);
            this.rtbCommunication.Margin = new System.Windows.Forms.Padding(2);
            this.rtbCommunication.Name = "rtbCommunication";
            this.rtbCommunication.ReadOnly = true;
            this.rtbCommunication.Size = new System.Drawing.Size(769, 481);
            this.rtbCommunication.TabIndex = 0;
            this.rtbCommunication.Text = "";
            this.rtbCommunication.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Communication between Server and Client";
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 17;
            this.bunifuElipse1.TargetControl = this;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Image = global::Server.Properties.Resources.dry_clean;
            this.btnClose.ImageActive = null;
            this.btnClose.Location = new System.Drawing.Point(762, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(17, 17);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = false;
            this.btnClose.Zoom = 10;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.Image = global::Server.Properties.Resources.dry_clean__2_;
            this.btnMinimize.ImageActive = null;
            this.btnMinimize.Location = new System.Drawing.Point(716, 10);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(17, 17);
            this.btnMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Zoom = 10;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // bunifuImageButton3
            // 
            this.bunifuImageButton3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton3.Image = global::Server.Properties.Resources.dry_clean__1_;
            this.bunifuImageButton3.ImageActive = null;
            this.bunifuImageButton3.Location = new System.Drawing.Point(739, 10);
            this.bunifuImageButton3.Name = "bunifuImageButton3";
            this.bunifuImageButton3.Size = new System.Drawing.Size(17, 17);
            this.bunifuImageButton3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton3.TabIndex = 5;
            this.bunifuImageButton3.TabStop = false;
            this.bunifuImageButton3.Zoom = 10;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 535);
            this.Controls.Add(this.bunifuImageButton3);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbCommunication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Server_FormClosed);
            this.Load += new System.EventHandler(this.Server_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this._MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this._MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this._MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbCommunication;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton3;
        private Bunifu.Framework.UI.BunifuImageButton btnMinimize;
        private Bunifu.Framework.UI.BunifuImageButton btnClose;
    }
}

