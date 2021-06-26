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
        static string rootPath = Path.GetDirectoryName(Application.ExecutablePath);
        static string accountDBPath = rootPath + @"/db.csv";
        char[] delimiterChars = { ' ', '-', '\n' };

        Socket listenerSocket;
        IPEndPoint ipepServer;
        List<Socket> clientSocketList;
        Dictionary<string, string> db;
        Dictionary<string, pathForClient> clientsList;

        struct pathForClient
        {
            public string inboxPath;
            public string selectFolderPath;
            public List<string> folders;
        }

        public Server()
        {
            InitializeComponent();
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
            richTextBox1.Text = "S: " + "OK " + "IMAP server ready.\n";
        }

        void sendMess(string s, Socket client)
        {
            foreach (Socket item in clientSocketList)
            {
                if (item != null && item == client)
                    item.Send(Encoding.UTF8.GetBytes(s));
            }
            richTextBox1.Text += "S: " + s;
        }

        private void GetAccoutDB()                                          // Save database of accounts to a dictionary
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

        private bool CheckAccount(string user, string pass)                 // Check if username and password client sent is correct
        {
            foreach (var item in db)
            {
                if (user == item.Key)
                    if (pass == item.Value)
                        return true;
            }
            return false;
        }

        private void UpdateValueForClient(ref Dictionary<string, pathForClient> clientsList, string user, string inboxPath, string selectFolderPath, List<string> folders)  // Update the value of specific key in clients list
        {
            pathForClient pFC;
            pFC.inboxPath = clientsList[user].inboxPath;
            pFC.selectFolderPath = selectFolderPath;
            pFC.folders = folders;

            clientsList[user] = pFC;
        }

        private void LoginRequest(string mess, Socket client)               // Response when client send login request
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
                    sendMess("This account are currently using by another person!!!", client);
            }
            else
                sendMess("tag NO [AUTHENTICATIONFAILED] Invalid credentials (Failure)", client);
        }

        private void GetMailFolders(string user, Socket client)             // Response when client send LIST folders request
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

        private void SelectFolder(string mess, Socket client)               // Response when client send SELECT folder request
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

        private void GetMailUID(string user, Socket client)                 // Response uid of mail when client send SEARCH mail in folder request
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


        private void FetchUID(string mess, Socket client)                   // Response content of mail when client send FETCH request
        {
            try
            {
                string[] words = mess.Split(delimiterChars);

                string selectFolderPath = clientsList[words[0]].selectFolderPath;
                mess = mess.Substring(11 + words[0].Length);
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

        private void LogOutClient(string user, Socket client)               // Response when client send LOGOUT request
        {
            try
            {
                if (clientsList.Keys.ToList().IndexOf(user) != -1)
                {
                    string returnData = $"*BYE IMAP Server logging out\nS: {user} OK LOGOUT completed\n";
                    sendMess(returnData, client);
                    clientsList.Remove(user);
                }
                else
                {
                    string returnData = $"*A login form has been disconnected from server!!!\n";
                    sendMess(returnData, client);
                }
                clientSocketList.Remove(client);
                client.Shutdown(SocketShutdown.Both);
                client.Disconnect(true);
            }
            catch { }
            return;
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
                    else if (text.Contains("logout"))
                        LogOutClient(words[0], client);
                }
            }
            catch
            {
                clientSocketList.Remove(client);
                client.Close();
            }

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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
