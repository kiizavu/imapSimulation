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

        // Nhan message tu client
        void getMess(object obj)
        {
            Socket client = obj as Socket;
            try
            {
                int bytesReceived = 0;
                byte[] recv = new byte[1];

                var endPoint = (IPEndPoint)client.RemoteEndPoint;
                sendMess(endPoint + " joined the server.\n", client);
                while (client.Connected)
                {
                    string text = "";

                    do
                    {
                        bytesReceived = client.Receive(recv);
                        text += Encoding.UTF8.GetString(recv);
                    } while (text[text.Length - 1] != '\n');

                    richTextBox1.Text += endPoint + ": ";

                    sendMess(text, client);

                    //listenerSocket.Close();
                }
            }
            catch
            {
                clientSocketList.Remove(client);
                client.Close();
            }

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
            richTextBox1.Text += "Waiting for client......\n";
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            btnListen.Enabled = false;
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = false;
            serverThread.Start();
        }
    }
}
