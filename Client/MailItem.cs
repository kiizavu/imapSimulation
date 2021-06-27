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
    }
}
