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
    public partial class Terminal : Form
    {
        const string IPADDRESS = "127.0.0.1";
        const int PORT = 8080;
        public static string user;
        string serverResponse = null;

        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);

        public Terminal()
        {
            InitializeComponent();
        }

        private void msg()                                  // Get respone from server and do something
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                rtbCommunication.Text += "S: " + serverResponse;
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

        private void InputRequest()
        {
            if (tbRequest.Text != string.Empty)
            {
                string request = tbRequest.Text + "\n";
                rtbCommunication.Text += "C: " + request;

                if(tbRequest.Text.Contains("login"))
                {
                    string[] words = tbRequest.Text.Split(' ');
                    request = $"{words[0]} {words[1]} {words[2]} {ComputeSha512Hash(words[3])}\n";
                }

                SendMess(request);
                tbRequest.Clear();
            }
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse(IPADDRESS);
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, PORT);
            try
            {
                tcpClient.Connect(iPEndPoint);
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

        private void tbRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                InputRequest();
        }

        private void rtbCommunication_TextChanged(object sender, EventArgs e)
        {
            rtbCommunication.SelectionStart = rtbCommunication.Text.Length;
            rtbCommunication.ScrollToCaret();
        }
    }
}
