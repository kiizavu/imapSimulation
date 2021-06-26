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
        public Login log { get; set; }
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        char[] delimiterChars = { ' ', '-', '\n' };
        string readData = null;
        string selectedFolder;
        int numberOfMail = 0;
        int isMailSelected = 0;

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
                if (readData.Contains("* LIST ")) //List folder
                {
                    readData = readData.Substring(7);
                    string[] folder = readData.Split('\n');
                    listView1.Items.Add(folder[0]);
                }
                else if (readData.Contains($"{Login.user} OK {selectedFolder} selected. (Success)")) //Select folder
                {
                    numberOfMail = 0;
                    string mess = $"{Login.user} uid search all\n";
                    SendMess(mess);
                }
                else if (readData.Contains("Unknown mailbox (Failure)"))
                {
                    MessageBox.Show("Unknown mailbox", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (readData.Contains("* SEARCH")) //Search mail uid in folder
                {
                    int index = readData.IndexOf($"\n{Login.user} OK SEARCH completed. (Success)");
                    int cutLenght= readData.Substring(index).Length;
                    int lengtOfNeed = readData.Length - cutLenght - 10;
                    if (lengtOfNeed > 0)
                    {
                        string uids = readData.Substring(9, lengtOfNeed);

                        string[] words = uids.Split(delimiterChars);
                        string mess = $"{Login.user} uid fetch ";
                        foreach (var item in words)
                        {
                            mess += item + " ";
                        }
                        SendMess(mess + "\n");
                    }
                }
                else if (readData.Contains("-") && isMailSelected == 0) //List mail to list view
                {
                    string[] words = readData.Split('-');
                    listView2.Items.Add(words[0]);
                    for (int i = 1; i < words.Length - 1; i++)
                    {
                        listView2.Items[numberOfMail].SubItems.Add(words[i]);
                    }
                    numberOfMail++;
                }
                else if (readData.Contains("-") && isMailSelected == 1) //Select mail
                {
                    richTextBox1.Text = "";
                    string mailContaint = readData.Replace('-','\n');
                    richTextBox1.Text = mailContaint;
                    isMailSelected = 0;
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
            string mess = $"{Login.user} list" + "\n";
            SendMess(mess);

            selectedFolder = "All mail";
            mess = $"{Login.user} select " + "\"" + selectedFolder + "\"" + "\n";
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
            string mess = $"{Login.user} select " + "\"" + selectedFolder + "\"" + "\n";
            SendMess(mess);
        }

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            isMailSelected = 1;
            string mailselected = listView2.SelectedItems[0].Text;
            string mess = $"{Login.user} uid fetch " + mailselected + '\n';
            SendMess(mess);
        }
    }
}
