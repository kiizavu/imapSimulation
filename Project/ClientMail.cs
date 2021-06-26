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
        const string IPADDRESS = "127.0.0.1";
        const int PORT = 8080;
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        char[] delimiterChars = { ' ', '-', '\n' };
        string readData = null;
        string selectedFolder;
        int numberOfMail = 0;
        int ismailselected = 0;
        public ClientMail()
        {
            InitializeComponent();
            (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItemClicked +=
                    new ToolStripItemClickedEventHandler(copy);
            (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItemClicked +=
                    new ToolStripItemClickedEventHandler(move);
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                richTextBox1.Text = "";
                Restart();
                if (readData.Contains("* LIST "))
                {
                    readData = readData.Substring(7);
                    string[] folder = readData.Split('\n');
                    listView1.Items.Add(folder[0]);
                }
                else if (readData.Contains($"tag OK {selectedFolder} selected. (Success)"))
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
                    if (index > 0)
                    {
                        uids = uids.Substring(0, index - 1);
                        string[] words = uids.Split(delimiterChars);
                        string mess = "tag uid fetch ";
                        foreach (var item in words)
                        {
                            mess += item + " ";
                        }
                        SendMess(mess + "\n");
                    }
                }
                else if (readData.Contains("-") && ismailselected == 0)
                {
                    string[] words = readData.Split('-');
                    listView2.Items.Add(words[0]);
                    for (int i = 1; i < words.Length; i++)
                    {
                        listView2.Items[numberOfMail].SubItems.Add(words[i]);
                    }
                    numberOfMail++;
                }
                else if (readData.Contains("-") && ismailselected == 1)
                {
                    richTextBox1.Text = "";
                    string[] s = readData.Split('-');
                    string from = s[1];
                    string subject = s[2];
                    string date = s[3];
                    richTextBox1.Text += from + '\n' + subject + '\n' + date + '\n';
                    ismailselected = 0;
                }
                else if (readData.Contains("tag delete OK"))
                {
                    listView1.Items.Clear();
                    string mess = "tag list" + "\n";
                    SendMess(mess);
                }    
                else if (readData.Contains("tag create OK"))
                {
                    listView1.Items.Clear();
                    string mess = "tag list" + "\n";
                    SendMess(mess);
                }    
                else if (readData.Contains("tag store OK") || readData.Contains("tag move OK"))
                {
                    listView2.Items.Clear();
                    string mess = "tag select " + "'" + selectedFolder + "'" + "\n";
                    SendMess(mess);
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

            Thread.Sleep(100);
            listView1.Items.Clear();
            string mess = "tag list" + "\n";
            SendMess(mess);

            selectedFolder = "All mail";
            mess = "tag select " + "'" + selectedFolder + "'" + "\n";
            SendMess(mess);
        }

        private string SendMess(string mess)
        {
            byte[] data = Encoding.UTF8.GetBytes(mess);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            return mess;
        }
        //Bam vao hien file
        //Lay subpath
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            selectedFolder = listView1.SelectedItems[0].Text;
            string mess = "tag select " + "'" + selectedFolder + "'" + "\n";
            SendMess(mess);
        }

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            ismailselected = 1;
            string mailselected = listView2.SelectedItems[0].Text;
            string mess = "tag uid fetch " + mailselected + '\n';
            SendMess(mess);
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            string command = "tag create " + textBox2.Text + "\n";
            listView1.Items.Add(textBox2.Text);
            SendMess(command);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string selected = listView2.SelectedItems[0].Text;
            string command = "tag store " + selected + @" +flags (\Delete)" + "\n";
            SendMess(command);
        }

        private void DelFolBtn_Click(object sender, EventArgs e)
        {
            string command = "tag delete " + selectedFolder + "\n";
            SendMess(command);
        }

        //1 click đọc mail
        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            bool match = false;
            if (e.Button == MouseButtons.Left)
            {
                ismailselected = 1;
                string mailselected = listView2.SelectedItems[0].Text;
                string mess = "tag uid fetch " + mailselected + '\n';
                SendMess(mess);
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (ListViewItem item in listView2.Items)
                {
                    if (item.Bounds.Contains(new Point(e.X, e.Y)))
                    {
                        listView2.ContextMenuStrip = contextMenuStrip1;
                        match = true;
                        break;
                    }
                }
                if (match)
                {
                    listView2.ContextMenuStrip.Show(listView2, new Point(e.X, e.Y));
                }
                else
                { }
            }    
        }

        //1 click hiện danh sách mail trong folder
        private void Mouse_Click1(object sender, MouseEventArgs e)
        {
            listView2.Items.Clear();
            try
            {
                selectedFolder = listView1.SelectedItems[0].Text;
                string mess = "tag select " + "'" + selectedFolder + "'" + "\n";
                SendMess(mess);
            }
            catch
            { }
        }

        //Làm sạch giao diện
        void Restart()
        {
            CreateBtn.Visible = false;
            label1.Visible = false;
            textBox2.Visible = false;
        }

        //Nút delete
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeleteBtn_Click(sender, e);
        }

        //Xóa thư mục hiện tại
        private void deleteCurrentFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart();
            DelFolBtn_Click(sender, e);
        }

        //Tạo folder mới
        private void createFolderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Restart();
            CreateBtn.Visible = true;
            label1.Text = "Nhập tên thư mục bạn muốn tạo";
            label1.Visible = true;
            textBox2.Visible = true;
        }

        //Event copy mail
        void copy(object sender, ToolStripItemClickedEventArgs e)
        {
            string msg = String.Format(e.ClickedItem.Text);
            string selected = listView2.SelectedItems[0].Text;
            string command = "tag copy " + selected + " " + msg + "\n";
            SendMess(command);
        }

        //Hiện danh sách thư mục để copy mail khi rê chuột tới
        private void copyToolStripMenuItem1_MouseHover(object sender, EventArgs e)
        {
            (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems.Clear();

            for (var i = 0; i < listView1.Items.Count; i++)
            {
                (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems.Add(listView1.Items[i].Text);
            }
        }

        //Event move mail
        void move(object sender, ToolStripItemClickedEventArgs e)
        {
            string msg = String.Format(e.ClickedItem.Text);
            string selected = listView2.SelectedItems[0].Text;
            string command = "tag move " + selected + " " + msg + "\n";
            SendMess(command);
        }

        //Hiện danh sách thư mục để move mail khi rê chuột tới
        private void chuyểnToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItems.Clear();

            for (var i = 0; i < listView1.Items.Count; i++)
            {
                (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItems.Add(listView1.Items[i].Text);
            }
        }
    }
}
