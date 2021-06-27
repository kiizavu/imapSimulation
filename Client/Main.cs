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
        public List<string> FolderAdd;

        public List<string> defaultFolders = new List<string>() { "All mail", "Sent", "Drafts", "Important", "Starred", "Spam", "Trash" };

        int mov;
        int movX;
        int movY;

        private Color activeColor = Color.FromArgb(2, 168, 244);

        private Color inactiveColor = Color.FromArgb(22, 25, 28);

        char[] delimiterChars = { ' ', '-', '\n' };
        string serverResponse = null;
        string selectedFolder;
        int numberOfMail = 0;
        public int isMailSelected = 0;

        public Login log { get; set; }
        public Dasboard dasboard { get; set; }
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
            (contextMenuStrip1.Items[1] as ToolStripMenuItem).DropDownItemClicked +=
                    new ToolStripItemClickedEventHandler(DelFol);
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
            IPAddress ipAddress = IPAddress.Parse(Dasboard.IPADDRESS);
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, Dasboard.PORT);
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

            FolderAdd = new List<string>();

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
                if (numberOfMail != 0)
                    pnlContainer.Controls.Remove(lbNoMail);
            });
        }


        private void needToLoad()
        {
            lbNoMail.Location = new Point(148, 65);
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
                    foreach (var item in folder)
                        if (!item.Contains("(Success)") && !item.Contains("\0") && item != "" && !defaultFolders.Contains(item))
                        {
                            drdSeeMore.AddItem(item);
                            FolderAdd.Add(item);
                        }
                }
                else if (serverResponse.Contains($"{Login.user} OK {selectedFolder} selected. (Success)"))  // Select folder
                {
                    //Set tilte for mail container
                    lbTilte.Text = selectedFolder;

                    //Clear the mail container
                    pnlContainer.Controls.Clear();
                    
                    //Add the loading animation and hide the mail contain
                    this.Invoke((MethodInvoker)delegate
                    {
                        needToLoad();
                    });

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
                else if (serverResponse.Contains($"{Login.user} OK DELETE"))                                // Delete folder
                {
                    drdSeeMore.Clear();
                    FolderAdd.Clear();
                    string[] tmp = serverResponse.Split(' ');
                    string mess = $"{Login.user} list\n";
                    SendMess(mess);
                }
                else if (serverResponse.Contains($"{Login.user} OK CREATE completed"))                                // Create folder
                {
                    drdSeeMore.Clear();
                    FolderAdd.Clear();
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

        private void pnlFolder_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pnlFolder.ContextMenuStrip = contextMenuStrip1;
                pnlFolder.ContextMenuStrip.Show(new Point(e.X, e.Y));
            }
        }

        private void createFoldderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFolBtn.Visible = true;
            btnSearch.Visible = false;
        }

        void DelFol(object sender, ToolStripItemClickedEventArgs e)
        {
            string msg = String.Format(e.ClickedItem.Text);
            string command = $"{Login.user} delete " + msg + "\n";
            SendMess(command);
        }
        private void deleteFolderToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            (contextMenuStrip1.Items[1] as ToolStripMenuItem).DropDownItems.Clear();

            for (var i = 0; i < FolderAdd.Count(); i++)
            {
                (contextMenuStrip1.Items[1] as ToolStripMenuItem).DropDownItems.Add(FolderAdd[i]);
                (contextMenuStrip1.Items[1] as ToolStripMenuItem).DropDownItems[i].BackColor = Color.FromArgb(59, 72, 77);
                (contextMenuStrip1.Items[1] as ToolStripMenuItem).DropDownItems[i].ForeColor = Color.White;
            }
        }

        private void CreateFolBtn_Click(object sender, EventArgs e)
        {
            string command = $"{Login.user} create " + textBox1.Text + "\n";
            SendMess(command);
            CreateFolBtn.Visible = false;
            btnSearch.Visible = true;
            textBox1.Clear();
        }

        private void drdSeeMore_onItemSelected(object sender, EventArgs e)
        {
            selectedFolder = drdSeeMore.selectedValue;
            string command = $"{Login.user} select \"" + selectedFolder + "\"\n";
            SendMess(command);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string mess = $"{Login.user} search " + textBox1.Text + '\n';
            SendMess(mess);
            pnlContainer.Controls.Clear();
            textBox1.Clear();
            numberOfMail = 0;
        }
    }
}
