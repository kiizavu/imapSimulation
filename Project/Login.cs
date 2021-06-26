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
        public static string user;
        string serverResponse = null;

        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);

        public Login()
        {
            InitializeComponent();
            initData();
        }

        private void msg()                                  // Get respone from server and do something
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                if (serverResponse.Contains($"OK {tbUserName.Text} authenticated (Success)"))         // If user and password is correct, open client mail form
                {
                    Checked();
                    ClientMail clientMail = new ClientMail();
                    clientMail.Show();
                    clientMail.log = this;
                    this.Visible = false;
                }
                else if (serverResponse.Contains("tag NO [AUTHENTICATIONFAILED] Invalid credentials (Failure)"))      // If user and password is incorrect, show message box to infrom user
                    MessageBox.Show("Username or password is incorrect!!!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (serverResponse.Contains("This account are currently using by another person!!!"))             // If the account is being used by another person
                    MessageBox.Show("This account are currently using by another person!!!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    serverResponse = returnData;
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


        static string ComputeSha512Hash(string rawData)         // Hash Sha512
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

        public void initData()
        {
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                if (Properties.Settings.Default.Remme == "yes")
                {
                    tbUserName.Text = Properties.Settings.Default.UserName;
                    tbPassword.Text = Properties.Settings.Default.Password;
                    cbRemember.Checked = true;
                }
                else
                {
                    tbUserName.Text = Properties.Settings.Default.UserName;
                    tbPassword.Text = "";
                }
            }
        }

        public void Checked() //Kiểm tra đã đánh dấu Remmeber me chưa ?
        {
            if (!cbRemember.Checked)
            {
                Properties.Settings.Default.UserName = tbUserName.Text;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Remme = "no";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserName = tbUserName.Text;
                Properties.Settings.Default.Password = tbPassword.Text;
                Properties.Settings.Default.Remme = "yes";
                Properties.Settings.Default.Save();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            user = tbUserName.Text;
            if (tbPassword.Text != string.Empty && tbUserName.Text != string.Empty)
            {
                string mess = $"tag login {tbUserName.Text} {ComputeSha512Hash(tbPassword.Text)}\n";        // Hash password before send to server
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

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            string mess = $"tag logout\n";
            SendMess(mess);
        }
    }
}
