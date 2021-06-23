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

namespace Project
{
    public partial class ClientMail : Form
    {
        public ClientMail()
        {
            InitializeComponent();
        }

        static string path = "";
        private void ClienMail_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            path = @"E:\Bài Tập\Visual Studio\ImapSimulation\INBOX\";//tùy vào máy mỗi ng
            Fill(path);
            textBox1.Text = path;
        }

        //điền vào listview
        void Fill(string path)
        {
            DirectoryInfo DI = new DirectoryInfo(@path);
            DirectoryInfo[] directories = DI.GetDirectories();
            FileInfo[] files = DI.GetFiles();

            //duyet folder
            foreach (DirectoryInfo di in directories)
            {
                ListViewItem lvi = new ListViewItem(di.Name);
                listView1.Items.Add(lvi);
            }

            //duyet file
            foreach (FileInfo fi in files)
            {
                ListViewItem lvi = new ListViewItem(fi.Name);
                listView1.Items.Add(lvi);
            }
        }
        private void btnAllMail_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnAllMail.Text;
            Fill(path);
        }
        private void btnDraft_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnDraft.Text;
            Fill(path);
        }
        private void btnFlagged_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnFlagged.Text;
            Fill(path);
        }
        private void btnImportant_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnImportant.Text;
            Fill(path);
        }
        private void btnSent_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnSent.Text;
            Fill(path);
        }
        private void btnStarred_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnStarred.Text;
            Fill(path);
        }
        private void btnTrash_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            path = textBox1.Text + btnTrash.Text;
            Fill(path);
        }
    }
}
