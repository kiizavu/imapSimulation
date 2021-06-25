using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            Server s = new Server();
            s.Show();
            btnServer.Enabled = false;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            Client c = new Client();
            c.Show();
        }

        private void btnClientMail_Click(object sender, EventArgs e)
        {
            ClientMail cm = new ClientMail();
            cm.Show();
        }
    }
}
