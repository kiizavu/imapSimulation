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
        const string IPADDRESS = "25.70.254.3";
        const int PORT = 8080;
        char[] delimiterChars = { ' ', '-', '\n' };
        string serverResponse = null;
        string selectedFolder;
        int numberOfMail = 0;
        int isMailSelected = 0;

        public Login log { get; set; }
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);

        public ClientMail()
        {
            InitializeComponent();
            (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItemClicked += new ToolStripItemClickedEventHandler(copy);
            (contextMenuStrip1.Items[2] as ToolStripMenuItem).DropDownItemClicked += new ToolStripItemClickedEventHandler(move);
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                Restart();
                if (serverResponse.Contains("* LIST "))                                                      // List folder
                {
                    serverResponse = serverResponse.Substring(7);
                    string[] folder = serverResponse.Split(new[] { "* LIST ", "\n" }, StringSplitOptions.None);
                    foreach (var item in folder)
                        if (!item.Contains("(Success)") && !item.Contains("\0") && item != "")
                            listView1.Items.Add(item);
                }
                else if (serverResponse.Contains($"{Login.user} OK {selectedFolder} selected. (Success)"))  // Select folder
                {
                    numberOfMail = 0;
                    string mess = $"{Login.user} uid search all\n";
                    SendMess(mess);
                }
                else if (serverResponse.Contains("Unknown mailbox (Failure)"))                               // If folder is not exist
                {
                    MessageBox.Show("Unknown mailbox", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (serverResponse.Contains("* SEARCH"))                                               // Search mail uid in folder
                {
                    int index = serverResponse.IndexOf($"\n{Login.user} OK SEARCH completed. (Success)");
                    int cutLenght= serverResponse.Substring(index).Length;
                    int lengtOfNeed = serverResponse.Length - cutLenght - 10;
                    if (lengtOfNeed > 0)
                    {
                        string uids = serverResponse.Substring(9, lengtOfNeed);

                        string[] words = uids.Split(delimiterChars);
                        string mess = $"{Login.user} uid fetch ";
                        foreach (var item in words)
                        {
                            mess += item + " ";
                        }
                        SendMess(mess + "\n");
                    }
                }
                else if (serverResponse.Contains("-;:{}") && isMailSelected == 0)                               // List mail to list view
                {
                    string[] words = serverResponse.Split(new[] { "-;:{}" }, StringSplitOptions.None);
                    listView2.Items.Add(words[0]);
                    for (int i = 1; i < words.Length - 1; i++)
                    {
                        listView2.Items[numberOfMail].SubItems.Add(words[i]);
                    }
                    numberOfMail++;
                }
                else if (serverResponse.Contains("-;:{}") && isMailSelected == 1)                               // Select mail
                {
                    richTextBox1.Text = "";
                    string mailContaint = serverResponse.Replace("-;:{}", "\n");
                    richTextBox1.Text = mailContaint;
                    isMailSelected = 0;
                }
                else if (serverResponse.Contains("OK LOGOUT completed"))                                    // Logout
                {
                    this.log.initData();
                    this.log.Visible = true;
                    this.Close();
                }
                else if (serverResponse.Contains($"{Login.user} OK DELETE Completed"))                                // Delete folder
                {
                    listView1.Items.Clear();
                    string mess = $"{Login.user} list\n";
                    SendMess(mess);
                }    
                else if (serverResponse.Contains($"{Login.user} OK CREATE completed"))                                // Create folder
                {
                    listView1.Items.Clear();
                    string mess = $"{Login.user} list\n";
                    SendMess(mess);
                }    
                else if (serverResponse.Contains($"{Login.user} OK STORE completed") || serverResponse.Contains($"{Login.user} OK MOVE completed"))
                {
                    listView2.Items.Clear();
                    string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
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
            string mess = $"{Login.user} list\n";
            SendMess(mess);

            //load all mail folder when start
            selectedFolder = "All mail";
            mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            selectedFolder = listView1.SelectedItems[0].Text;
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            isMailSelected = 1;
            string mailselected = listView2.SelectedItems[0].Text;
            string mess = $"{Login.user} uid fetch " + mailselected + '\n';
            SendMess(mess);
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            string command = $"{Login.user} create " + textBox2.Text + "\n";
            listView1.Items.Add(textBox2.Text);
            SendMess(command);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string selected = listView2.SelectedItems[0].Text;
            string command = $"{Login.user} store " + selected + @" +flags (\Delete)" + "\n";
            SendMess(command);
        }

        private void DelFolBtn_Click(object sender, EventArgs e)
        {
            string command = $"{Login.user} delete " + selectedFolder + "\n";
            SendMess(command);
        }

        //right click để thao tác
        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            bool match = false;
            if (e.Button == MouseButtons.Right)
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
            }    
        }

        //Làm sạch giao diện
        void Restart()
        {
            richTextBox1.Text = "";
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
            string command = $"{Login.user} copy " + selected + " " + msg + "\n";
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
            string command = $"{Login.user} move " + selected + " " + msg + "\n";
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

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure ?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string mess = $"{Login.user} logout\n";
                SendMess(mess);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string mess = $"{Login.user} search " + tbSearch.Text + '\n';
            SendMess(mess);
            listView2.Items.Clear();
            numberOfMail = 0;
        }

        private void ClientMail_FormClosed(object sender, FormClosedEventArgs e)
        {
            string mess = $"{Login.user} logout\n";
            SendMess(mess);
        }
    }
}
