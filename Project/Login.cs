using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;

namespace Project
{
    public partial class Login : Form
    {
        const string IPADDRESS = "127.0.0.1";
        const int PORT = 8080;
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        string readData = null;
        public static string user;

        public Login()
        {
            InitializeComponent();
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                if (readData.Contains($"OK {tbUserName.Text} authenticated (Success)"))
                {
                    ClientMail clientMail = new ClientMail();
                    clientMail.Show();
                    clientMail.log = this;
                    this.Visible = false;
                }
                else if (readData.Contains("tag NO [AUTHENTICATIONFAILED] Invalid credentials (Failure)"))
                    MessageBox.Show("Username or password is incorrect!!!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (readData.Contains("The account are currently using by another person!!!"))
                    MessageBox.Show("The account are currently using by another person!!!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getMess()
        {
            string returnData;

            try
            {
                while (true)
                {
                    ns = tcpClient.GetStream();
                    var buffSize = tcpClient.ReceiveBufferSize;
                    byte[] data = new byte[buffSize];
                    ns.Read(data, 0, buffSize);
                    returnData = Encoding.UTF8.GetString(data);
                    readData = returnData;
                    msg();
                }
            }
            catch
            {
                tcpClient.Close();
            }
        }

        private string SendMess(string mess)
        {
            byte[] data = Encoding.UTF8.GetBytes(mess);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            return mess;
        }


        static string ComputeSha512Hash(string rawData)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] bytes = shaM.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            user = tbUserName.Text;
            if (tbPassword.Text != string.Empty && tbUserName.Text != string.Empty)
            {
                string mess = $"tag login {tbUserName.Text} {ComputeSha512Hash(tbPassword.Text)}\n";
                SendMess(mess);
            }

        }

        private void Client_Load(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse(IPADDRESS);
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, PORT);
            try
            {
                tcpClient.Connect(iPEndPoint);
                richTextBox1.Text += "You have joined the server successfully.\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            Thread thread = new Thread(getMess);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            ns.Close();
            tcpClient.Close();
        }
    }
}
