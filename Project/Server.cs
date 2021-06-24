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
            ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
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
/*                        string date = File.ReadLines(path + "/" + uid).Skip(0).Take(1).First();
                        string from = File.ReadLines(path + "/" + uid).Skip(1).Take(1).First();
                        string sub = File.ReadLines(path + "/" + uid).Skip(2).Take(1).First();*/
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



        /*private string AutoRep(int a)//chua xong
        {
            int temp = Int32.Parse(a);
            switch(temp){
                case 1:
                    return "Client Connected";
                    break;
                case 2:
                    break;
            }
            return rev;
        }*/
        // Nhan message tu client
        void getMess(object obj)
        {
            Socket client = obj as Socket;
            try
            {
                int bytesReceived = 0;
                byte[] recv = new byte[1];

                var endPoint = (IPEndPoint)client.RemoteEndPoint;
                //sendMess(AutoRep(endPoint + "","1"), client);
                sendMess("C: " + endPoint + " connected to the server.\n", client);
                while (client.Connected)
                {
                    string text = "";
                    do
                    {
                        bytesReceived = client.Receive(recv);
                        text += Encoding.UTF8.GetString(recv);
                    } while (text[text.Length - 1] != '\n');

                    richTextBox1.Text += "C: "+ endPoint + ": " + text;
                    StatusResponse(text, client);
                    GetMailUID(text, client);
                    FetchUID(text, client);

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
