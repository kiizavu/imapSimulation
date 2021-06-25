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
        List<string> folders;
        Dictionary<string, string> db;

        public Server()
        {
            InitializeComponent();
        }


        private void Server_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = false;
            serverThread.Start();
            GetAccoutDB();
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
            richTextBox1.Text += "S: " + s;
        }

        static string rootPath = Path.GetDirectoryName(Application.ExecutablePath);
        static string inboxPath = rootPath + @"/Inbox/";
        static string accountDBPath = rootPath + @"/db.csv";
        string folderPath = null;


        private void GetAccoutDB()
        {
            using (var reader = new StreamReader(accountDBPath))
            {
                db = new Dictionary<string, string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    db.Add(values[0], values[1]);
                }
            }
        }

        private bool CheckAccount(string user, string pass)
        {
            foreach (var item in db)
            {
                if (user == item.Key)
                    if (pass == item.Value)
                        return true;
            }
            return false;
        }

        private void LoginRequest(string mess, Socket client)
        {
            string[] words = mess.Split(delimiterChars);
            if (CheckAccount(words[2], words[3]))
                sendMess("Correct!!!", client);
            else
                sendMess("Username or password is incorrect!!!", client);
        }

        private void GetMailFoldders(Socket client)
        {
            folders = new List<string>();
            DirectoryInfo DI = new DirectoryInfo(inboxPath);
            DirectoryInfo[] directories = DI.GetDirectories();

            //duyet folder
            foreach (var item in directories)
            {
                sendMess($"* LIST {item.Name}\n", client);
                folders.Add(item.Name);
            }
            return;
        }


        private void SelectFolder(string mess, Socket client)
        {
            foreach(var item in folders)
            {
                if (mess.Contains(item))
                {
                    folderPath = inboxPath + item;
                    sendMess($"tag OK {item} selected. (Success)\n", client);
                    return;
                }
            }
            sendMess("Unknown mailbox\n", client);
            return;
        }

        private void GetMailUID(Socket client)
        {
            FileInfo[] fi = new FileInfo[100000];
            DirectoryInfo di = new DirectoryInfo(folderPath);
            fi = di.GetFiles();
            try
            {
                string returnData = "* SEARCH ";
                for (int i = 0; i < fi.Length; i++)
                {
                    string uid = fi[i].Name;
                    returnData += uid + " ";
                }
                returnData += "\ntag OK SEARCH completed. (Success)\n";
                sendMess(returnData, client);
            }
            catch { }
            return;
        }


        private void FetchUID(string mess, Socket client)
        {
            try
            {
                mess = mess.Substring(14);
                string[] uids = mess.Split(delimiterChars);
                foreach (var item in uids)
                {
                    string date = File.ReadLines(folderPath + "/" + item).Skip(0).Take(1).First();
                    string from = File.ReadLines(folderPath + "/" + item).Skip(1).Take(1).First();
                    string sub = File.ReadLines(folderPath + "/" + item).Skip(2).Take(1).First();
                    string returnData = item + "-" + from + "-" + sub + "-" + date + "\n";
                    sendMess(returnData, client);
                }
                    
            }
            catch { }
            return;
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
                sendMess(endPoint + " connected to the server.\n", client);
                while (client.Connected)
                {
                    string text = "";
                    do
                    {
                        bytesReceived = client.Receive(recv);
                        text += Encoding.UTF8.GetString(recv);
                    } while (text[text.Length - 1] != '\n');

                    richTextBox1.Text += "C: " + endPoint + ": " + text;

                    if (text.Contains("login"))
                        LoginRequest(text, client);
                    else if (text.Contains("list"))
                        GetMailFoldders(client);
                    else if (text.Contains("select"))
                        SelectFolder(text, client);
                    else if (text.Contains("uid search all"))
                        GetMailUID(client);
                    else if (text.Contains("uid fetch"))
                        FetchUID(text, client);

                }
            }
            catch
            {
                clientSocketList.Remove(client);
                client.Close();
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
