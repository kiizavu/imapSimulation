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

namespace Client
{
    public partial class Terminal : Form
    {
        int mov;
        int movX;
        int movY;

        public static string user;
        string serverResponse = null;

        public Dasboard dasboard { get; set; }
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);

        public Terminal()
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

        private void msg()                                  // Get respone from server and do something
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                if (serverResponse.Contains("-;:{}"))
                {
                    string[] words = serverResponse.Split(new[] { "-;:{}" }, StringSplitOptions.None);
                    string date = words[3];
                    string from = words[1];
                    string subject = words[2];
                    string body = words[4];
                    rtbCommunication.SelectionColor = Color.Green;
                    rtbCommunication.AppendText($"Date: {date}\nFrom: {from}\nSubject: {subject}\nBody:\n{body}\n");
                }
                else if (serverResponse.Contains("OK LOGOUT completed"))
                {
                    rtbCommunication.SelectionColor = Color.Green;
                    rtbCommunication.AppendText(serverResponse);
                    ns.Close();
                    tcpClient.Close();
                    rtbCommunication.AppendText("The terminal will close in 3 second.\n");
                    Task.Delay(3000).Wait();
                    this.Close();
                    this.dasboard.Visible = true;
                }
                else
                {
                    rtbCommunication.SelectionColor = Color.Green;
                    rtbCommunication.AppendText(serverResponse);
                }

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

                rtbCommunication.SelectionColor = Color.WhiteSmoke;
                rtbCommunication.AppendText(request);
                //rtbCommunication.Text += request;

                if(tbRequest.Text.Contains("login"))
                {
                    string[] words = tbRequest.Text.Split(' ');
                    if (words.Length == 3)
                    {
                        user = words[1];
                        request = $"tag {words[0]} {user} {ComputeSha512Hash(words[2])}\n";
                    }
                    else
                    {
                        MessageBox.Show("Wrong command!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    request = $"{user} {tbRequest.Text}\n";
                }

                SendMess(request);
                tbRequest.Clear();
            }
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse(Dasboard.IPADDRESS);
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, Dasboard.PORT);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.dasboard.Visible = true;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
