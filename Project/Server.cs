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

        string rootPath = Path.GetDirectoryName(Application.ExecutablePath) + @"/Inbox/";
        string path = null;


        private void GetMailFoldder(string mess, Socket client)
        {
            if (mess.Contains("tag list"))
            {
                DirectoryInfo DI = new DirectoryInfo(rootPath);
                DirectoryInfo[] directories = DI.GetDirectories();

                //duyet folder
                foreach (var item in directories)
                {
                    sendMess($"* LIST {item.Name}\n", client);
                }
                return;
            }
            else if (!mess.Contains("tag list"))
            {
                return;
            }
        }


        //phan hoi tu server
        private void StatusResponse(string mess, Socket client)
        {
            string[] subpath = { "All mail", "Draft", "Flagged", "Important", "Sent", "Starred", "Trash" };
            if (mess.Contains("tag select"))
                {
                for(int i = 0; i < subpath.Length; i++)
                {
                    if (mess.Contains(subpath[i]))
                    {
                        path = rootPath + subpath[i];
                        sendMess($"tag OK [READ-WRITE] {subpath[i]} selected. (Success)\n", client);
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
        //Search

        private string[] ReadFiles(string word)
        {
            string read = "";
            string[] uids = new string[10000];
            int i = 0;
            string[] subpath = { "All mail", "Draft", "Flagged", "Important", "Sent", "Starred", "Trash" };
            DirectoryInfo d = new DirectoryInfo(rootPath);
            DirectoryInfo[] di = d.GetDirectories();
            FileInfo[] subdi = new FileInfo[10000];
            foreach (var item in di)
            {
                subdi = item.GetFiles();
                foreach (var file in subdi)
                {
                    if (file.Name != null)
                    {
                        string c = word.Trim('\n');
                        read = File.ReadLines(file.FullName).Skip(1).Take(1).First();
                        if (c == read)
                        {
                            uids[i] = file.Name;
                            i++;
                        }
                    }
                }
            }
            return uids;
        }
        private void SearchMail(string mess, Socket client)
        {
            string cut = mess;
            string from = "";
            string[] uids = new string[100000];
            if (mess.Contains("tag search from "))
            {
                for(int i = 16; i < cut.Length; i++)
                {
                    from += cut[i]; 
                }
                uids = ReadFiles(from);
                foreach(var item in uids)
                {
                    sendMess(item, client);
                }
            }

            return;
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
                        string returnData = item + "-" + from + "-" + sub + "-" + date +'\n';
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
                    GetMailFoldder(text, client);
                    StatusResponse(text, client);
                    GetMailUID(text, client);
                    FetchUID(text, client);
                    SearchMail(text, client);
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
