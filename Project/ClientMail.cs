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
        //TcpClient tcpClient = new TcpClient();
        //NetworkStream ns = default(NetworkStream);
        string readData = null;
        public ClientMail()
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

        static string path = "";
        private void ClienMail_Load(object sender, EventArgs e)
        {
            //connect to server
            /*IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
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
            thread.Start();*/

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
        /* dùng button 
        private void btnAllMail_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnAllMail.Text;
            Fill(path);
        }
        private void btnDraft_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnDraft.Text;
            Fill(path);
        }
        private void btnFlagged_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnFlagged.Text;
            Fill(path);
        }
        private void btnImportant_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnImportant.Text;
            Fill(path);
        }
        private void btnSent_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnSent.Text;
            Fill(path);
        }
        private void btnStarred_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnStarred.Text;
            Fill(path);
        }
        private void btnTrash_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnTrash.Text;
            Fill(path);
        }*/
    }
}
