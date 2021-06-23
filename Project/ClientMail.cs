using System;
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

namespace Project
{
    public partial class ClientMail : Form
    {
        TcpClient tcpClient = new TcpClient();
        public ClientMail()
        {
            InitializeComponent();
        }

        static string path = "";
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

            //lấy ổ đĩa
            DriveInfo[] drives = DriveInfo.GetDrives();
            path = @"E:\Bài Tập\Visual Studio\ImapSimulation\INBOX\";//tùy vào máy mỗi ng
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
    }
}
