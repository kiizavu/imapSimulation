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
        NetworkStream ns = default(NetworkStream);
        char[] delimiterChars = { ' ', '-', '\n' };
        string readData = null;
        string selected;
        int numberOfMail = 0;
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
                if (readData.Contains($"tag OK [READ-WRITE] {selected} selected. (Success)"))
                {
                    numberOfMail = 0;
                    string mess = "tag uid search all\n";
                    SendMess(mess);
                }
                else if (readData.Contains("* SEARCH"))
                {
                    int index = readData.IndexOf("\ntag OK SEARCH completed (Success)");
                    string uids = readData.Substring(9, index);
                    index = uids.IndexOf("\ntag");
                    uids = uids.Substring(0, index - 1);
                    string[] words = uids.Split(delimiterChars);
                    string mess = "tag uid fetch ";
                    foreach (var item in words)
                    {
                        mess += item + " ";
                    }
                    SendMess(mess + "\n");
                }
                else if (readData.Contains("-"))
                {
                    string[] words = readData.Split('-');
                    listView2.Items.Add(words[0]);
                    for (int i = 1; i < words.Length; i++)
                    {
                        listView2.Items[numberOfMail].SubItems.Add(words[i]);
                    }
                    numberOfMail++;
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
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, 1234);
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
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Inbox";//tùy vào máy mỗi ng
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
        private string SendMess(string mess)
        {
            Byte[] data = Encoding.UTF8.GetBytes(mess);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            return mess;
        }
        //Bam vao hien file
        //Lay subpath
        private void listView1_ItemActivate_1(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            selected = listView1.SelectedItems[0].Text;
            string mess = "tag select " + '"' + selected + '"' + "\n";
            SendMess(mess);
        }
    }
}
