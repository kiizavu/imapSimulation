using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using S22.Imap;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Project
{
    public partial class Login : Form
    {
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        string readData = null;

        public static string user;
        public static string pass;

        protected static ImapClient client { get; set; }

        public Login()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            user = tbUser.Text;
            pass = tbPass.Text;
            client = new ImapClient("imap.gmail.com", 993, true);

            //check username, password
            try
            {
                client.Login(user, pass, AuthMethod.Auto);
            }
            catch
            {
                MessageBox.Show("Username or Password is incorrect!!!\nPlease try again.",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            truyền dữ liệu qua form ClientMail
            ClientMail c = new ClientMail();
            c.log = this; 

            new Thread(() => new ClientMail().ShowDialog()).Start();
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            this.tbPass.PasswordChar = '*';
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                tbUser.Text += readData + "\n";
            }
        }

    }
}
