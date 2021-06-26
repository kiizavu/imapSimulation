
namespace Project
{
    partial class Terminal
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
            this.rtbCommunication = new System.Windows.Forms.RichTextBox();
            this.tbRequest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rtbCommunication
            // 
            this.rtbCommunication.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCommunication.Location = new System.Drawing.Point(12, 12);
            this.rtbCommunication.Name = "rtbCommunication";
            this.rtbCommunication.Size = new System.Drawing.Size(776, 426);
            this.rtbCommunication.TabIndex = 0;
            this.rtbCommunication.Text = "";
            this.rtbCommunication.TextChanged += new System.EventHandler(this.rtbCommunication_TextChanged);
            // 
            // tbRequest
            // 
            this.tbRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRequest.Location = new System.Drawing.Point(12, 469);
            this.tbRequest.Name = "tbRequest";
            this.tbRequest.Size = new System.Drawing.Size(776, 29);
            this.tbRequest.TabIndex = 1;
            this.tbRequest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRequest_KeyDown);
            // 
            // Terminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 529);
            this.Controls.Add(this.tbRequest);
            this.Controls.Add(this.rtbCommunication);
            this.Name = "Terminal";
            this.Text = "Terminal";
            this.Load += new System.EventHandler(this.Terminal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbCommunication;
        private System.Windows.Forms.TextBox tbRequest;
    }
}