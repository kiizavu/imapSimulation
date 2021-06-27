using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Dasboard : Form
    {
        int mov;
        int movX;
        int movY;

        public string IPADDRESS;
        public int PORT;

        public Dasboard()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ////////////////////////// move form possition//////////////////////////////////
        private void _MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            IPADDRESS = tbIPAddress.Text;
            PORT = int.Parse(tbPort.Text);

            Server server = new Server();
            server.Show();
        }
    }
}
