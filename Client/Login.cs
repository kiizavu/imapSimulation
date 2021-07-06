using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.IO;

namespace Client
{
    public partial class Login : Form
    {
        int mov;
        int movX;
        int movY;

        public static string user;
        string serverResponse = null;

        string initVec = "4JoZD9aVHWnVmlXe5INNow==";
        string pk = "prrtiNdMz2fG1SB7KMevbDwo9qQFCmgdWGitgdTa23Q=";

        public Dasboard dasboard { get; set; }
        TcpClient tcpClient = new TcpClient();
        NetworkStream ns = default(NetworkStream);

        public Login()
        {
            InitializeComponent();
            initData();
        }

        /// <summary>
        /// 
        /// EVENT
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        // move form possition
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }


        //login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Initialize();
        }


        private void tbUP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Initialize();
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            string mess = $"tag logout\n";
            SendMess(mess);
            Environment.Exit(0);
        }


        //Form load
        private void Login_Load(object sender, EventArgs e)
        {
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
        }

        /// <summary>
        /// 
        /// FUNCTION
        /// 
        /// </summary>

        private void msg()                                  // Get respone from server and do something
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                int cut = serverResponse.IndexOf('\0');
                serverResponse = serverResponse.Substring(0, cut);
                string[] Responses = serverResponse.Split('\n');
                for (int i = 0; i < Responses.Count(); i++)
                {
                    if (Responses[i] != "")
                        Responses[i] = DecryptAES(Responses[i], pk, initVec);

                    if (Responses[i].Contains($"OK {tbUserName.Text} authenticated (Success)"))         // If user and password is correct, open client mail form
                    {
                        Checked();
                        Main main = new Main();
                        main.Show();
                        main.log = this;
                        this.Visible = false;
                    }
                    else if (Responses[i].Contains("tag NO [AUTHENTICATIONFAILED] Invalid credentials (Failure)"))      // If user and password is incorrect, show message box to infrom user
                        MessageBox.Show("Username or password is incorrect!!!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (Responses[i].Contains("This account are currently using by another person!!!"))             // If the account is being used by another person
                        MessageBox.Show("This account are currently using by another person!!!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private string SendMess(string mess)
        {
            var smess = EncryptAES(mess, pk, initVec) + '\n';
            byte[] data = Encoding.UTF8.GetBytes(smess);
            ns.Write(data, 0, data.Length);
            ns.Flush();
            return mess;
        }


        static string ComputeSha512Hash(string rawData)         // Hash Sha512
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] bytes = shaM.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        private void Initialize()
        {
            user = tbUserName.Text;
            if (tbPassword.Text != string.Empty && tbUserName.Text != string.Empty)
            {
                string mess = $"tag login {tbUserName.Text} {ComputeSha512Hash(tbPassword.Text)}\n";        // Hash password before send to server
                SendMess(mess);
            }
        }



        //Save username, password if click remme
        public void initData()
        {
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                if (Properties.Settings.Default.Remme == "yes")
                {
                    tbUserName.Text = Properties.Settings.Default.UserName;
                    tbPassword.Text = Properties.Settings.Default.Password;
                    cbRemember.Checked = true;
                }
                else
                {
                    tbUserName.Text = Properties.Settings.Default.UserName;
                    tbPassword.Text = "";
                }
            }
        }


        //Kiểm tra đã đánh dấu Remmeber me chưa ?
        public void Checked()
        {
            if (!cbRemember.Checked)
            {
                Properties.Settings.Default.UserName = tbUserName.Text;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Remme = "no";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserName = tbUserName.Text;
                Properties.Settings.Default.Password = tbPassword.Text;
                Properties.Settings.Default.Remme = "yes";
                Properties.Settings.Default.Save();
            }
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