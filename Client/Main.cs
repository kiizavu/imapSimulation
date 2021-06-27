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

namespace Client
{
    public partial class Main : Form
    {
        int mov;
        int movX;
        int movY;

        loadingProgress lP;

        private Color activeColor = Color.FromArgb(2, 168, 244);

        private Color inactiveColor = Color.FromArgb(22, 25, 28);

        const string IPADDRESS = "127.0.0.1";
        const int PORT = 8080;
        char[] delimiterChars = { ' ', '-', '\n' };
        string serverResponse = null;
        string selectedFolder;
        int numberOfMail = 0;
        public int isMailSelected = 0;

        public Login log { get; set; }
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);
        List<MailItem> mailItems;


        public Label Subject
        {
            get { return this.lbSubject; }
        }

        public Label Date
        {
            get { return this.lbDate; }
        }


        public Label NoMailSelected
        {
            get { return this.lbNoMailSelect; }
        }

        public RichTextBox Body
        {
            get { return this.rtbBody; }
        }

        public RichTextBox From
        {
            get { return this.rtbFrom; }
        }


        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }


        /////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////Control button/////////////////////////////////////
        private void btnClose_Click(object sender, EventArgs e)
        {
            string mess = $"{Login.user} logout\n";
            SendMess(mess);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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


        /////////////////////////////////////////////////////////////////////////////////
        //////////////////////////button click//////////////////////////////////////////
        private void btnAll_Click(object sender, EventArgs e)
        {
            activeButton(btnAll);
            selectedFolder = "All mail";
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            activeButton(btnSent);
            selectedFolder = "Sent";
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void btnDrafts_Click(object sender, EventArgs e)
        {
            activeButton(btnDrafts);
            selectedFolder = "Drafts";
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void btnImportant_Click(object sender, EventArgs e)
        {
            activeButton(btnImportant);
            selectedFolder = "Important";
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void btnStarred_Click(object sender, EventArgs e)
        {
            activeButton(btnStarred);
            selectedFolder = "Starred";
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void btnSpam_Click(object sender, EventArgs e)
        {
            activeButton(btnSpam);
            selectedFolder = "Spam";
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            activeButton(btnTrash);
            selectedFolder = "Trash";
            string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure ?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string mess = $"{Login.user} logout\n";
                SendMess(mess);
            }
        }



        /////////////////////////////////////////////////////////////////////////////////
        //////////////////////////Form load/////////////////////////////////////////////
        private void Main_Load(object sender, EventArgs e)
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
            string mess = $"{Login.user} list\n";
            SendMess(mess);


            lP = new loadingProgress();
            lP.Location = new Point(29, 668);

            selectedFolder = "All mail";
            mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(mess);

        }




        /////////////////////////////////////////////////////////////////////////////////
        //////////////////////////Function//////////////////////////////////////////////
        private void loadMails(string[] words)
        {
            //Add the loading animation and hide the mail contain
            this.Invoke((MethodInvoker)delegate
            {
                needToLoad();
            });


            Task.Run(() =>
            {
                this.BeginInvoke((Action)(() =>
                {
                    mailItems.Add(new MailItem());
                    mailItems[numberOfMail].Dock = DockStyle.Top;
                    mailItems[numberOfMail].main = this;
                    pnlContainer.Controls.Add(mailItems[numberOfMail]);
                    mailItems[numberOfMail].Uid.Text = words[0];
                    mailItems[numberOfMail].From.Text = words[1];
                    mailItems[numberOfMail].Date.Text = words[3];
                    mailItems[numberOfMail].Subject.Text = words[2];
                    mailItems[numberOfMail++].Body.Text = words[4];
                }));

                //Add "No mail here" label if there is no mail in folder
                if (mailItems.Count != 0)
                    pnlContainer.Controls.Remove(lbNoMail);
                else
                    pnlContainer.Controls.Add(lbNoMail);

                //remove the loading animation
                pnlFolder.Controls.Remove(lP);
            });
        }


        private void needToLoad()
        {
            pnlFolder.Controls.Add(lP);
            pnlContainer.Controls.Add(lbNoMail);

            setViEnFalse(lbDate);

            setViEnFalse(lbSubject);

            setViEnFalse(rtbFrom);

            setViEnFalse(rtbBody);

            lbNoMailSelect.Enabled = true;
            lbNoMailSelect.Visible = true;
        }

        private void setViEnFalse(Label lb)
        {
            lb.Visible = false;
            lb.Enabled = false;
        }

        private void setViEnFalse(RichTextBox rtb)
        {
            rtb.Visible = false;
            rtb.Enabled = false;
        }

        private void activeButton(Bunifu.Framework.UI.BunifuFlatButton active)
        {
            btnAll.Normalcolor = inactiveColor;
            btnSent.Normalcolor = inactiveColor;
            btnDrafts.Normalcolor = inactiveColor;
            btnImportant.Normalcolor = inactiveColor;
            btnStarred.Normalcolor = inactiveColor;
            btnSpam.Normalcolor = inactiveColor;
            btnTrash.Normalcolor = inactiveColor;

            active.Normalcolor = activeColor;
        }

        private void setViEnTrue(Label lb)
        {
            lb.Visible = true;
            lb.Enabled = true;
        }

        private void setViEnTrue(RichTextBox rtb)
        {
            rtb.Visible = true;
            rtb.Enabled = true;
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                if (serverResponse.Contains("* LIST "))                                                      // List folder
                {
                    serverResponse = serverResponse.Substring(7);
                    string[] folder = serverResponse.Split(new[] { "* LIST ", "\n" }, StringSplitOptions.None);
                    /*foreach (var item in folder)
                        if (!item.Contains("(Success)") && !item.Contains("\0") && item != "")
                            listView1.Items.Add(item);*/
                }
                else if (serverResponse.Contains($"{Login.user} OK {selectedFolder} selected. (Success)"))  // Select folder
                {
                    //Set tilte for mail container
                    lbTilte.Text = selectedFolder;

                    //Clear the mail container
                    pnlContainer.Controls.Clear();

                    numberOfMail = 0;
                    string mess = $"{Login.user} uid search all\n";
                    SendMess(mess);
                }
                else if (serverResponse.Contains("Unknown mailbox (Failure)"))                               // If folder is not exist
                {
                    MessageBox.Show("Unknown mailbox", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (serverResponse.Contains("* SEARCH"))                                               // Search mail uid in folder
                {
                    int index = serverResponse.IndexOf($"\n{Login.user} OK SEARCH completed. (Success)");
                    int cutLenght = serverResponse.Substring(index).Length;
                    int lengtOfNeed = serverResponse.Length - cutLenght - 10;
                    if (lengtOfNeed > 0)
                    {
                        string uids = serverResponse.Substring(9, lengtOfNeed);

                        string[] words = uids.Split(delimiterChars);
                        string mess = $"{Login.user} uid fetch ";
                        foreach (var item in words)
                        {
                            mess += item + " ";
                        }

                        mailItems = new List<MailItem>();

                        SendMess(mess + "\n");
                    }
                }
                else if (serverResponse.Contains("-;:{}") && isMailSelected == 0)                               // List mail to list view
                {
                    string[] words = serverResponse.Split(new[] { "-;:{}" }, StringSplitOptions.None);
                    loadMails(words);
                }
                else if (serverResponse.Contains("-;:{}") && isMailSelected == 1)                               // Select mail
                {
                    string[] words = serverResponse.Split(new[] { "-;:{}" }, StringSplitOptions.None);

                    rtbFrom.Text = words[1];
                    lbSubject.Text = words[2];
                    lbDate.Text = words[3];
                    rtbBody.Text = words[4];

                    setViEnTrue(From);
                    setViEnTrue(Date);
                    setViEnTrue(Subject);
                    setViEnTrue(Body);
                    isMailSelected = 0;
                }
                else if (serverResponse.Contains("OK LOGOUT completed"))                                    // Logout
                {
                    this.log.initData();
                    this.log.Visible = true;
                    this.Close();
                }
                else if (serverResponse.Contains($"{Login.user} OK DELETE Completed"))                                // Delete folder
                {
                    //listView1.Items.Clear();
                    string mess = $"{Login.user} list\n";
                    SendMess(mess);
                }
                else if (serverResponse.Contains($"{Login.user} OK CREATE completed"))                                // Create folder
                {
                    //listView1.Items.Clear();
                    string mess = $"{Login.user} list\n";
                    SendMess(mess);
                }
                else if (serverResponse.Contains($"{Login.user} OK STORE completed") || serverResponse.Contains($"{Login.user} OK MOVE completed"))
                {
                    pnlContainer.Controls.Clear();
                    string mess = $"{Login.user} select \"" + selectedFolder + "\"\n";
                    SendMess(mess);
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
                    serverResponse = returnData;
                    msg();
                }
            }
            catch
            {
                tcpClient.Close();
            }
        }

        public string SendMess(string mess)
        {
            byte[] data = Encoding.UTF8.GetBytes(mess);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            return mess;
        }
    }
}
