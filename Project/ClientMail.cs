﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using S22.Imap;

namespace Project
{
    public partial class ClientMail : Form
    {
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        string readData = null;
        public ClientMail()
        {
            InitializeComponent();
        }

        const string path = @"D:\IMAP";//tùy vào máy mỗi ng
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
        private void ClientMail_Load(object sender, EventArgs e)
        {
            //connect to server
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, 8080);
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
            thread.IsBackground = false;
            thread.Start();

            //lấy ổ đĩa
            DriveInfo[] drives = DriveInfo.GetDrives();
            Fill(path);
            textBox1.Text = path;
        }

        //điền vào listview
        void Fill(string path)
        {
            DirectoryInfo DI = new DirectoryInfo(@path);
            DirectoryInfo[] directories = DI.GetDirectories();
            FileInfo[] files = DI.GetFiles();

            //duyet folder
            foreach (DirectoryInfo di in directories)
            {
                ListViewItem lvi = new ListViewItem(di.Name);
                listView1.Items.Add(lvi);
            }

            //duyet file
            foreach (FileInfo fi in files)
            {
                ListViewItem lvi = new ListViewItem(fi.Name);
                listView1.Items.Add(lvi);
            }
        }
        private string sendMess(string mess)
        {
            Byte[] data = Encoding.UTF8.GetBytes(mess);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            richTextBox1.Text += mess;
            return mess;
        }

        ///////////////Logout////////////////
        CancellationTokenSource source = null;
        public Login log { get; set; }

        public ImapClient client { get; set; }

        static private void showLoginForm()
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure ?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (source != null)
                {
                    source.Cancel();
                    source.Dispose();
                }
                client.Logout();
                this.Close();
            }
        }
    }
}
