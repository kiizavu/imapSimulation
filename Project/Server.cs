﻿using System;
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
        Dictionary<string, string> db;

        static string rootPath = Path.GetDirectoryName(Application.ExecutablePath);
        static string accountDBPath = rootPath + @"/db.csv";

        struct pathForClient
        {
            public string inboxPath;
            public string selectFolderPath;
            public List<string> folders;
        }

        Dictionary<string, pathForClient> clientsList;

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
            clientsList = new Dictionary<string, pathForClient>();
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
                    ipepServer = new IPEndPoint(IPAddress.Parse(IPADDRESS), PORT);
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
            {
                if (!clientsList.ContainsKey(words[2]))
                {
                    pathForClient pFC;
                    pFC.inboxPath = rootPath + $@"/{words[2]}/";
                    pFC.selectFolderPath = null;
                    pFC.folders = null;
                    clientsList.Add(words[2], pFC);
                    sendMess($"tag OK {words[2]} authenticated (Success)", client);
                }
                else
                    sendMess("The account are currently using by another person!!!", client);
            }
            else
                sendMess("tag NO [AUTHENTICATIONFAILED] Invalid credentials (Failure)", client);
        }

        private void UpdateValueForClient(ref Dictionary<string, pathForClient> clientsList, string user, string inboxPath, string selectFolderPath, List<string> folders)
        {
            pathForClient pFC;
            pFC.inboxPath = clientsList[user].inboxPath;
            pFC.selectFolderPath = selectFolderPath;
            pFC.folders = folders;

            clientsList[user] = pFC;
        }

        private void GetMailFolders(string user, Socket client)
        {
            List<string> foldersOfClient = new List<string>();
            string inboxPathOfClient = clientsList[user].inboxPath;

            DirectoryInfo DI = new DirectoryInfo(inboxPathOfClient);
            DirectoryInfo[] directories = DI.GetDirectories();

            //duyet folder
            foreach (var item in directories)
            {
                sendMess($"* LIST {item.Name}\n", client);
                foldersOfClient.Add(item.Name);
            }

            UpdateValueForClient(ref clientsList, user, inboxPathOfClient, null, foldersOfClient);

            return;
        }


        private void SelectFolder(string mess, Socket client)
        {
            string[] words = mess.Split(delimiterChars);

            string inboxPath = clientsList[words[0]].inboxPath;
            string selectFolderPath = clientsList[words[0]].selectFolderPath;
            List<string> folders = clientsList[words[0]].folders;

            foreach (var item in folders)
            {
                if (mess.Contains(item))
                {
                    selectFolderPath = inboxPath + item;
                    UpdateValueForClient(ref clientsList, words[0], inboxPath, selectFolderPath, folders);
                    sendMess($"{words[0]} OK {item} selected. (Success)\n", client);
                    return;
                }
            }
            sendMess("Unknown mailbox (Failure)\n", client);
            return;
        }

        private void GetMailUID(string user, Socket client)
        {
            FileInfo[] fi = new FileInfo[100000];
            DirectoryInfo di = new DirectoryInfo(clientsList[user].selectFolderPath);
            fi = di.GetFiles();
            try
            {
                string returnData = "* SEARCH ";
                for (int i = 0; i < fi.Length; i++)
                {
                    string uid = fi[i].Name;
                    returnData += uid + " ";
                }
                returnData += $"\n{user} OK SEARCH completed. (Success)\n";
                sendMess(returnData, client);
            }
            catch { }
            return;
        }


        private void FetchUID(string mess, Socket client)
        {
            try
            {
                string[] words = mess.Split(delimiterChars);

                string selectFolderPath = clientsList[words[0]].selectFolderPath;
                mess = mess.Substring(14);
                string[] uids = mess.Split(delimiterChars);
                foreach (var item in uids)
                {
                    string date = File.ReadLines(selectFolderPath + "/" + item).Skip(0).Take(1).First();
                    string from = File.ReadLines(selectFolderPath + "/" + item).Skip(1).Take(1).First();
                    string sub = File.ReadLines(selectFolderPath + "/" + item).Skip(2).Take(1).First();
                    string body = "";
                    for (int i = 3; i < File.ReadLines(selectFolderPath + "/" + item).Count(); i++)
                    {
                        body += File.ReadLines(selectFolderPath + "/" + item).Skip(i).Take(1).First() + "\n";
                    }
                    string returnData = item + "-" + from + "-" + sub + "-" + date + "-" + body;
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


                    string[] words = text.Split(delimiterChars);

                    if (text.Contains("login"))
                        LoginRequest(text, client);
                    else if (text.Contains("list"))
                        GetMailFolders(words[0], client); // words[0] is username
                    else if (text.Contains("select"))
                        SelectFolder(text, client);
                    else if (text.Contains("uid search all"))
                        GetMailUID(words[0], client);
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
