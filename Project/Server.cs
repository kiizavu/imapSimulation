using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Project
{
    public partial class Server : Form
    {
        const string IPADDRESS = "127.0.0.1";
        const int PORT = 8080;
        char[] delimiterChars = { ' ', '-', '\n' };
        List<Socket> clientSocketList;
        Socket listenerSocket;
        IPEndPoint ipepServer;

        public Server()
        {
            InitializeComponent();
        }

        //mở form là listen luôn
        private void Server_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = false;
            serverThread.Start();
        }
        void StartUnsafeThread()
        {
            clientSocketList = new List<Socket>();

            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipepServer = new IPEndPoint(IPAddress.Parse(IPADDRESS), PORT);
            listenerSocket.Bind(ipepServer);

            Thread listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        listenerSocket.Listen(10);
                        Socket client = listenerSocket.Accept();
                        clientSocketList.Add(client);

                        Thread receive = new Thread(getMess);
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch
                {
                    listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ipepServer = new IPEndPoint(IPAddress.Any, 1234);
                    listenerSocket.Bind(ipepServer);
                }

            });
            listen.IsBackground = true;
            listen.Start();
            richTextBox1.Text = "S: " + "OK " + "IMAP4rev1 server ready.\n";
        }

        // Gui lai message cho client
        void sendMess(string s, Socket client)
        {
            foreach (Socket item in clientSocketList)
            {
                if (item != null && item == client)
                    item.Send(Encoding.UTF8.GetBytes(s));
            }
            richTextBox1.Text += s;
        }

        string rootPath = Path.GetDirectoryName(Application.ExecutablePath) + @"/INBOX/";
        string path = null;
        List<string> folders;

        private void GetMailFolders(string mess, Socket client)
        {
            if (mess.Contains("tag list"))
            {
                folders = new List<string>();
                DirectoryInfo DI = new DirectoryInfo(rootPath);
                DirectoryInfo[] directories = DI.GetDirectories();

                //duyet folder

                foreach (var item in directories)
                {
                    sendMess($"* LIST {item.Name}\n", client);
                    folders.Add(item.Name);
                }
                return;
            }
            else if(!mess.Contains("tag list"))
            {
                return;
            }
        }


        //phan hoi tu server
        private void StatusResponse(string mess, Socket client)
        {
            if (mess.Contains("tag select"))
                {
                foreach (var item in folders)
                {
                    if (mess.Contains(item))
                    {
                        path = rootPath + item;
                        sendMess($"tag OK {item} selected. (Success)\n", client);
                        return;
                    }
                }
                sendMess("Unknown mailbox\n", client);
                return;
            }
            else if(!mess.Contains("tag select"))
            {
                return;
            }
        }

        private void GetMailUID(string mess, Socket client)
        {
            if (mess.Contains("tag uid search all"))
            {
                FileInfo[] fi = new FileInfo[100000];
                DirectoryInfo di = new DirectoryInfo(path);
                fi = di.GetFiles();
                try
                {
                    string returnData = "* SEARCH ";
                    for (int i = 0; i < fi.Length; i++)
                    {
                        string uid = fi[i].Name;
                        returnData += uid + " ";
                    }
                    returnData += "\ntag OK SEARCH completed (Success)\n";
                    sendMess(returnData, client);
                }
                catch { }
                return;
            }
            else if(!mess.Contains("tag uid search all"))
            {
                return;
            }
        }

        private void FetchUID(string mess, Socket client)
        {
            if (mess.Contains("tag uid fetch"))
            {
                try
                {
                    mess = mess.Substring(14);
                    string[] uids = mess.Split(delimiterChars);
                    foreach (var item in uids)
                    {
                        string date = File.ReadLines(path + "/" + item).Skip(0).Take(1).First();
                        string from = File.ReadLines(path + "/" + item).Skip(1).Take(1).First();
                        string sub = File.ReadLines(path + "/" + item).Skip(2).Take(1).First();
                        string returnData = item + "-" + from + "-" + sub + "-" + date;
                        sendMess(returnData, client);
                    }
                    
                }
                catch { }
                return;
            }
            else if (!mess.Contains("tag uid fetch"))
            {
                return;
            }
        }

        void Create_Folder(string mess, Socket client)
        {
            if (mess.Contains("tag create"))
            {
                try
                {
                    mess = mess.Substring(11);
                    folders.Add(mess);
                    Directory.CreateDirectory(rootPath + mess);
                    sendMess("tag create OK\n", client);
                }
                catch { }
                return;
            }    
            else if (!mess.Contains("tag create"))
            {
                return;
            }    
        }

        //Copy mail
        void Copy_Mail(string uid, string mailBox, string curMailBox)
        {
            string sourcePath = rootPath + curMailBox;
            string targetPath = rootPath + mailBox;
            DirectoryInfo d = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dir = d.GetDirectories();
            FileInfo[] fi = d.GetFiles();
            for (int i = 0; i < fi.Length; ++i)
            {
                if (fi[i].Name.Contains(uid))
                {
                    string sourceFile = System.IO.Path.Combine(sourcePath, fi[i].Name);
                    string destFile = System.IO.Path.Combine(targetPath, fi[i].Name);
                    File.Copy(sourceFile, destFile, true);
                    break;
                }
            }
        }

        void CopyMail(string mess, Socket client)
        {
            if (mess.Contains("tag copy"))
            {
                try
                {
                    mess = mess.Substring(9);
                    string[] tmp = mess.Split(' ', '\n');
                    string curFolder = new DirectoryInfo(path).Name;
                    Copy_Mail(tmp[0], tmp[1], curFolder);
                    sendMess("tag copy OK\n", client);
                }
                catch { }
                return;
            }
            else if (!mess.Contains("tag copy"))
            {
                return;
            }
        }

        //Delete mail
        void Delete_Mail(string uid, string curMailBox)
        {
            path = rootPath + curMailBox;
            DirectoryInfo d = new DirectoryInfo(path);
            DirectoryInfo[] dir = d.GetDirectories();
            FileInfo[] fi = d.GetFiles();
            for (int i = 0; i < fi.Length; ++i)
            {
                if (fi[i].Name.Contains(uid))
                {
                    File.Delete(fi[i].FullName);
                    break;
                }
            }
        }

        void DeleteMail(string mess, Socket client)
        {
            if (mess.Contains("tag store"))
            {
                try
                {
                    mess = mess.Substring(10);
                    string[] tmp = mess.Split(' ', '\n');
                    string curFolder = new DirectoryInfo(path).Name;
                    Delete_Mail(tmp[0], curFolder);
                    sendMess("tag store OK\n", client);
                }
                catch { }
                return;
            }
            else if (!mess.Contains("tag store"))
            {
                return;
            }
        }

        void DeleteFol(string mess, Socket client)
        {
            if (mess.Contains("tag delete"))
            {
                try
                {
                    Directory.Delete(path, true);
                    sendMess("tag delete OK\n", client);
                }
                catch { }
                return;
            }
            else if (!mess.Contains("tag delete"))
            {
                return;
            }
        }

        void Relocate_Mail(string uid, string mailBox, string curMailBox)
        {
            Copy_Mail(uid, mailBox, curMailBox);
            Delete_Mail(uid, curMailBox);
        }

        void RelocateMail(string mess, Socket client)
        {
            if (mess.Contains("tag move"))
            {
                try
                {
                    mess = mess.Substring(9);
                    string[] tmp = mess.Split(' ', '\n');
                    string curFolder = new DirectoryInfo(path).Name;
                    Relocate_Mail(tmp[0], tmp[1], curFolder);
                    sendMess("tag move OK\n", client);
                }
                catch { }
                return;
            }
            else if (!mess.Contains("tag mnove"))
            {
                return;
            }
        }
        // Nhan message tu client
        void getMess(object obj)
        {
            Socket client = obj as Socket;
            try
            {
                int bytesReceived = 0;
                byte[] recv = new byte[1];

                var endPoint = (IPEndPoint)client.RemoteEndPoint;
                sendMess("C: " + endPoint + " connected to the server.\n", client);
                while (client.Connected)
                {
                    string text = "";
                    do
                    {
                        bytesReceived = client.Receive(recv);
                        text += Encoding.UTF8.GetString(recv);
                    } while (text[text.Length - 1] != '\n');

                    richTextBox1.Text += endPoint + ": " + text;
                    GetMailFolders(text, client);
                    StatusResponse(text, client);
                    GetMailUID(text, client);
                    FetchUID(text, client);
                    Create_Folder(text, client);
                    CopyMail(text, client);
                    DeleteMail(text, client);
                    DeleteFol(text, client);
                    RelocateMail(text, client);
                }
            }
            catch
            {
                clientSocketList.Remove(client);
                client.Close();
            }
        }
    }
}
