using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MailItem : UserControl
    {
        public Main main { get; set; }
        public Login log { get; set; }

        public MailItem()
        {
            InitializeComponent();
            (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItemClicked +=
                    new ToolStripItemClickedEventHandler(copy);
            (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItemClicked +=
                    new ToolStripItemClickedEventHandler(move);
        }

        public Label Uid
        {
            get { return this.lbUid; }
        }

        public Label From
        {
            get { return this.lbFrom; }
        }

        public Label Subject
        {
            get { return this.lbSubject; }
        }

        public Label Date
        {
            get { return this.lbDate; }
        }

        public Label Body
        {
            get { return this.lbBody; }
        }

        public Label UnSeen
        {
            get { return this.lbUnSeen; }
        }


        private void MailItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(110, 110, 110);
        }

        private void MailItem_MouseLeave(object sender, EventArgs e)
        {
            if (lbUnSeen.Text == "True")
                this.BackColor = Color.FromArgb(59, 72, 77);
            else
                this.BackColor = Color.FromArgb(46, 52, 57);
        }

        public void MailItem_Click(object sender, EventArgs e)
        {
            
            main.isMailSelected = 1;
            string mailselected = lbUid.Text;
            string mess = $"{Login.user} uid fetch " + mailselected + '\n';
            main.SendMess(mess);


            main.NoMailSelected.Visible = false;
            main.NoMailSelected.Enabled = false;
        }

        private void MailItem_MouseClick(object sender, MouseEventArgs e)
        {
            //bool match = false;
            if (e.Button == MouseButtons.Right)
            { 
            this.ContextMenuStrip = contextMenuStrip1;
            this.ContextMenuStrip.Show(new Point(e.X, e.Y));
            }
            //else
            //{ }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = lbUid.Text;
            string command = $"{Login.user} store " + selected + @" +flags (\Delete)" + "\n";
            main.SendMess(command);
        }

        //Event copy mail
        void copy(object sender, ToolStripItemClickedEventArgs e)
        {
            string msg = String.Format(e.ClickedItem.Text);
            string selected = lbUid.Text;
            string command = $"{Login.user} copy " + selected + " " + msg + "\n";
            main.SendMess(command);
        }

        //rê chuột hiển thị
        private void copyToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems.Clear();

            for (var i = 0; i < main.FolderAdd.Count; i++)
            {
                (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems.Add(main.FolderAdd[i]);
                (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems[i].BackColor = Color.FromArgb(59, 72, 77);
                (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems[i].ForeColor = Color.White;
            }
        }

        //Event move mail
        void move(object sender, ToolStripItemClickedEventArgs e)
        {
            string msg = String.Format(e.ClickedItem.Text);
            string selected = lbUid.Text;
            string command = $"{Login.user} move " + selected + " " + msg + "\n";
            main.SendMess(command);
        }

        //rê chuột hiển thị
        private void moveToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItems.Clear();

            for (var i = 0; i < main.FolderAdd.Count; i++)
            {
                (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItems.Add(main.FolderAdd[i]);
                (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItems[i].BackColor = Color.FromArgb(59, 72, 77);
                (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItems[i].ForeColor = Color.White;
            }
        }
    }
}
