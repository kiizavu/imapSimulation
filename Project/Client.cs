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

namespace Project
{
    public partial class Client : Form
    {
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        string readData = null;

        public Client()
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
                richTextBox1.Text += readData + "\n";
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


        private void btnSend_Click(object sender, EventArgs e)
        {

            if (tbMess.Text != string.Empty)
            {
                if (tbUserName.Text != string.Empty)
                {
                    string mess = tbUserName.Text + ": " + tbMess.Text + '\n';
                    Byte[] data = Encoding.UTF8.GetBytes(mess);
                    ns.Write(data, 0, data.Length);
                    ns.Flush();
                    richTextBox1.Text += mess;
                    tbMess.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Please enter USERNAME!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void Client_Load(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, 8080);
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
