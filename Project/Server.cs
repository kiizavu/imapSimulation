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
        List<Socket> clientSocketList;
        Socket listenerSocket;
        IPEndPoint ipepServer;

        public Server()
        {
            InitializeComponent();
        }

        const string path = @"E:\Bài Tập\Visual Studio\ImapSimulation\INBOX";//tùy vào máy mỗi ng

        //mở form là listen luôn
        private void Server_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        void StartUnsafeThread()
        {
            clientSocketList = new List<Socket>();
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
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
                    ipepServer = new IPEndPoint(IPAddress.Any, 8080);
                    listenerSocket.Bind(ipepServer);
                }

            });
            listen.IsBackground = true;
            listen.Start();
            richTextBox1.Text = "S: " + StastusResponse(1) + "IMAP4rev1 server ready.\n";
        }

        //phan hoi tu server
        private string StastusResponse(int a)
        {
            switch(a){
                case 1:
                    return "* OK ";
                case 2:
                    return "* NO ";
                case 3:
                    return "* BAD";
                case 4:
                    return "* PREAUTH ";
                default:
                    return "* BYE ";
            }
        }
        private void ListFolder(string temp)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            DirectoryInfo DI = new DirectoryInfo(@path);
            DirectoryInfo[] directories = DI.GetDirectories();
            switch (temp){
                case "All": //list all
                    foreach (DirectoryInfo di in directories)                                               
                    {
                        richTextBox1.Text +="S: *LIST (|" + temp +") '/' '[Gmail]/" + di.Name + "'\n";
                    }
                    richTextBox1.Text += "S: " + StastusResponse(1) + "List Completed";
                    break;
                    /*"S: *LIST  (|All) '/' '[Gmail]/All Mail'\n" +
                        "S: *LIST  (|Drafts) '/' '[Gmail]/Drafts'\n" +
                        "S: *LIST  (|Important) '/' '[Gmail]/Important'\n" +
                        "S: *LIST  (|Sent) '/' '[Gmail]/Sent Mail'\n" +
                        "S: *LIST  (|Junk) '/' '[Gmail]/Spam'\n" +
                        "S: *LIST  (|Flagged) '/' '[Gmail]/Starred'\n" +
                        "S: *LIST  (|Trash) '/' '[Gmail]/Trash'\nS: " +*/
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
                sendMess("S: " + endPoint + " connected to the server.\n", client);
                while (client.Connected)
                {
                    sendMess("C: tag list '' '*'\n", client);
                    ListFolder("All");
                    string text = "";
                    do
                    {
                        bytesReceived = client.Receive(recv);
                        text += Encoding.UTF8.GetString(recv);
                    } while (text[text.Length - 1] != '\n');

                    richTextBox1.Text += endPoint + ": ";
                    sendMess(text, client);
                }
            }
            catch
            {
                clientSocketList.Remove(client);
                client.Close();
            }
        }
        // Gui lai message cho client
        void sendMess(string s, Socket client)
        {
            foreach (Socket item in clientSocketList)
            {
                if (item != null && item != client)
                    item.Send(Encoding.UTF8.GetBytes(s));
            }
            richTextBox1.Text += s;
        }
    }
}
