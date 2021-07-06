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
using System.Security.Cryptography;

namespace Server
{
    public partial class Server : Form
    {
        int mov;
        int movX;
        int movY;

        static string rootPath = Path.GetDirectoryName(Application.ExecutablePath);
        static string accountDBPath = rootPath + @"/db.csv";
        char[] delimiterChars = { ' ', '-', '\n' };

        string initVec = "4JoZD9aVHWnVmlXe5INNow==";
        string pk = "prrtiNdMz2fG1SB7KMevbDwo9qQFCmgdWGitgdTa23Q=";

        public Dasboard dasboard { get; set; }
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
            ipepServer = new IPEndPoint(IPAddress.Parse(Dasboard.IPADDRESS), Dasboard.PORT);
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
                    ipepServer = new IPEndPoint(IPAddress.Parse(Dasboard.IPADDRESS), Dasboard.PORT);
                    listenerSocket.Bind(ipepServer);
                }

            });
            listen.IsBackground = true;
            listen.Start();
            rtbCommunication.SelectionColor = Color.DeepSkyBlue;
            rtbCommunication.AppendText("OK IMAP server ready.\n");
        }

        void sendMess(string s, Socket client)
        {
            foreach (Socket item in clientSocketList)
            {
                if (item != null && item == client)
                    item.Send(Encoding.UTF8.GetBytes(EncryptAES(s, pk, initVec) + "\n"));
            }

            rtbCommunication.SelectionColor = Color.DeepSkyBlue;
            rtbCommunication.AppendText(s);
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
                    sendMess($"tag OK {words[2]} authenticated (Success)\n", client);
                }
                else
                    sendMess("This account are currently using by another person!!!\n", client);
            }
            else
                sendMess("tag NO [AUTHENTICATIONFAILED] Invalid credentials (Failure)\n", client);
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

            if (folders != null)
            {
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
            sendMess("Use LIST command first\n", client);
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
                if (selectFolderPath != null)
                {
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
                        string returnData = item + "-;:{}" + from + "-;:{}" + sub + "-;:{}" + date + "-;:{}" + body;
                        sendMess(returnData, client);
                    }
                }
                else
                    sendMess("Select a folder first\n", client);

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
                    string returnData = $"*BYE IMAP Server logging out\n{user} OK LOGOUT completed\n";
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

        private void CreateFolder(string mess, Socket client)
        {
            try
            {
                string[] words = mess.Split(delimiterChars);
                string inboxPath = clientsList[words[0]].inboxPath;
                string selectFolderPath = clientsList[words[0]].selectFolderPath;
                List<string> folders = clientsList[words[0]].folders;

                mess = words[2];
                folders.Add(mess);
                Directory.CreateDirectory(inboxPath + mess);
                sendMess($"{words[0]} OK CREATE completed\n", client);

                UpdateValueForClient(ref clientsList, words[0], inboxPath, selectFolderPath, folders);
            }
            catch { }
            return;
        }

        //Copy mail
        private void Copy_Mail(string uid, string mailBox, string curFolder, string inboxPath)
        {
            string sourcePath = inboxPath + curFolder;
            string targetPath = inboxPath + mailBox;
            DirectoryInfo d = new DirectoryInfo(sourcePath);
            DirectoryInfo[] dir = d.GetDirectories();
            FileInfo[] fi = d.GetFiles();
            for (int i = 0; i < fi.Length; ++i)
            {
                if (fi[i].Name.Contains(uid))
                {
                    string sourceFile = Path.Combine(sourcePath, fi[i].Name);
                    string destFile = Path.Combine(targetPath, fi[i].Name);
                    File.Copy(sourceFile, destFile, true);
                    break;
                }
            }
        }

        private void CopyMail(string mess, Socket client)
        {
            try
            {
                string[] words = mess.Split(delimiterChars);
                string inboxPath = clientsList[words[0]].inboxPath;
                string selectFolderPath = clientsList[words[0]].selectFolderPath;

                string curFolder = new DirectoryInfo(selectFolderPath).Name;
                Copy_Mail(words[2], words[3], curFolder, inboxPath);
                sendMess($"{words[0]} OK COPY completed\n", client);
            }
            catch { }
            return;
        }

        //Delete mail
        private void Delete_Mail(string uid, string selectFolderPath)
        {
            DirectoryInfo d = new DirectoryInfo(selectFolderPath);
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

        private void DeleteMail(string mess, Socket client)
        {
            try
            {
                string[] words = mess.Split(delimiterChars);
                string selectFolderPath = clientsList[words[0]].selectFolderPath;

                Delete_Mail(words[2], selectFolderPath);
                sendMess($"{words[0]} OK STORE completed\n", client);
            }
            catch { }
            return;
        }

        private void DeleteFol(string mess, Socket client)
        {
            try
            {
                string[] words = mess.Split(delimiterChars);
                string inboxPath = clientsList[words[0]].inboxPath;
                string selectFolderPath = inboxPath + words[2];
                List<string> folders = clientsList[words[0]].folders;

                Directory.Delete(selectFolderPath, true);
                var dirName = new DirectoryInfo(selectFolderPath).Name;
                sendMess($"{words[0]} OK DELETE {dirName} Completed\n", client);

                UpdateValueForClient(ref clientsList, words[0], inboxPath, selectFolderPath, folders);
            }
            catch { }
            return;
        }

        private void Relocate_Mail(string uid, string mailBox, string curFolder, string inboxPath)
        {
            Copy_Mail(uid, mailBox, curFolder, inboxPath);
            string path = inboxPath + curFolder;
            Delete_Mail(uid, path);
        }

        private void RelocateMail(string mess, Socket client)
        {
            try
            {
                string[] words = mess.Split(delimiterChars);
                string inboxPath = clientsList[words[0]].inboxPath;
                string selectFolderPath = clientsList[words[0]].selectFolderPath;

                string curFolder = new DirectoryInfo(selectFolderPath).Name;
                Relocate_Mail(words[2], words[3], curFolder, inboxPath);
                sendMess($"{words[0]} OK MOVE completed\n", client);
            }
            catch { }
            return;
        }

        private List<string> GetFromSearch(string search, string selectFolderPath)
        {
            List<string> uids = new List<string>();

            DirectoryInfo d = new DirectoryInfo(selectFolderPath);
            FileInfo[] subdi = d.GetFiles();

            foreach (var item in subdi)
            {
                string from = File.ReadLines(item.FullName).Skip(1).Take(1).First();
                if (from == search)
                    uids.Add(item.Name);
            }
            return uids;
        }

        private void SearchFrom(string mess, Socket client)
        {
            List<string> uids = new List<string>();

            string[] words = mess.Split(delimiterChars);
            string selectFolderPath = clientsList[words[0]].selectFolderPath;

            string search = words[words.Length - 2];

            uids = GetFromSearch(search, selectFolderPath);

            string senddata = "* SEARCH ";

            foreach (var item in uids)
                senddata += item + " ";

            senddata += $"\n{words[0]} OK SEARCH completed. (Success)";
            sendMess(senddata, client);
        }

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

                    text = DecryptAES(text, pk, initVec);

                    rtbCommunication.SelectionColor = Color.Red;
                    rtbCommunication.AppendText(endPoint + ": " + text);


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
                    else if (text.Contains("create"))
                        CreateFolder(text, client);
                    else if (text.Contains("copy"))
                        CopyMail(text, client);
                    else if (text.Contains("store"))
                        DeleteMail(text, client);
                    else if (text.Contains("delete"))
                        DeleteFol(text, client);
                    else if (text.Contains("move"))
                        RelocateMail(text, client);
                    else if (text.Contains("logout"))
                        LogOutClient(words[0], client);
                    else if (text.Contains("search from"))
                        SearchFrom(text, client);
                    else
                        sendMess("* BAD Unknown command\n", client);
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
            rtbCommunication.SelectionStart = rtbCommunication.Text.Length;
            rtbCommunication.ScrollToCaret();
        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            listenerSocket.Close();
        }

        /////////////////////////////////////////////////////////////////////////////////
        ////////////////////////// move form possition//////////////////////////////////
        private void _MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void _MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
        }

        private void _MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.dasboard.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        string DecryptAES(string ciphertext, string key, string iv)
        {
            byte[] buffer = Convert.FromBase64String(ciphertext);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }


        string EncryptAES(string plaintext, string key, string iv)
        {
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plaintext);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
    }
}