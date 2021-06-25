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
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                if (readData.Contains("* LIST "))
                {
                    readData = readData.Substring(7);
                    string[] folder = readData.Split('\n');
                    listView1.Items.Add(folder[0]);
                }
                else if (readData.Contains($"tag OK [READ-WRITE] {selectedFolder} selected. (Success)"))
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
            }
        }

        //Date: odsahfiouasdhf
        //Subject: soldahnflaisjdhf
        
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

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            string mess = "tag search " + textBox1.Text + '\n';
            SendMess(mess);
        }
        //Xoa listView2
        /*Go date voi ngay neu can tim, tuong tu voi from va cac thu khac
         VD: go search from Name trong textbox, no se gui tin nhan tag search voi
        thu can tim trong textbox sang server*/
    }
}
