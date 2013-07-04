using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Sc2Hack_UpdateManager.Classes.Fontend
{
    public partial class MainWindow : Form
    {
        private Version _vOnlineVersion;
        private Version _vCurrentVersion;
        private String _strPathToChanges = String.Empty;
        private String _strPathToExecutable = String.Empty;
        public String StrOnlinePath = "https://dl.dropboxusercontent.com/u/62845853/AnotherSc2Hack/UpdateFiles/Sc2Hack_Version";
        private Font _fBold = new Font("Arial", 12, FontStyle.Bold);
        private Font _fRegular = new Font("Arial", 10, FontStyle.Regular);
        private string _strCurrentFile = @"AnotherSc2Hack.exe";

        public MainWindow()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            new Thread(UpdateThread).Start();

            SetStyle(ControlStyles.DoubleBuffer | 
                ControlStyles.OptimizedDoubleBuffer|
                ControlStyles.UserPaint, true);
        }

        /* Download file and replace */ 
        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.DownloadFileAsync(new Uri(_strPathToExecutable), _strCurrentFile + "tmp");
            wc.DownloadProgressChanged += WcOnDownloadProgressChanged;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
        }

        private void WcOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs downloadProgressChangedEventArgs)
        {
            

            pgrBarMain.Value = downloadProgressChangedEventArgs.ProgressPercentage;
            
            
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (File.Exists(_strCurrentFile + "tmp"))
            {



                if (File.Exists(_strCurrentFile))
                {
                    File.Replace(_strCurrentFile + "tmp", _strCurrentFile,
                                 _strCurrentFile + "Backup_" + DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month +
                                 "-" +
                                 DateTime.Now.Day + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" +
                                 DateTime.Now.Second);
                }

                else
                    File.Move(_strCurrentFile + "tmp", _strCurrentFile);


                var procs = Process.GetProcesses();
                foreach (var process in procs)
                {
                    if (process.ProcessName == "AnotherSc2Hack")
                    {
                        process.CloseMainWindow();

                    }
                }

                try
                {
                    Process.Start("AnotherSc2Hack.exe");
                }

                catch
                {
                    
                }


                Environment.Exit(0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateThread()
        {
            GetInformation();

            if (_vOnlineVersion > _vCurrentVersion)
            {
                btnStartUpdate.Enabled = true;
                ParseChangesFile();
            }

            else
                lblHeader.Text = "You are Up-To-Date!";
        }

        
        private void GetInformation()
        {
              var iCountTimeOuts = 0;

        TryAnotherRound:

            /* We ping the Server first to exclude exceptions! */
            var myPing = new Ping();

            PingReply myResult;


            try
            {
                myResult = myPing.Send("Dropbox.com", 10);
            }

            catch
            {
                goto TryAnotherRound;
            }

            if (myResult != null && myResult.Status != IPStatus.Success)
            {
                if (iCountTimeOuts >= 10)
                {
                    MessageBox.Show("Can not reach Server!\n\nTry later!", "FAILED");
                    Close();
                    return;
                }

                iCountTimeOuts++;
                goto TryAnotherRound;

            }


            /* Connect to server */
            var privateWebClient = new WebClient();
            privateWebClient.Proxy = null;

            string strSource = string.Empty;

            try
            {
                strSource = privateWebClient.DownloadString(StrOnlinePath);
            }

            catch
            {
                MessageBox.Show("Can not reach Server!\n\nTry later!", "FAILED");
                Close();
                return;
            }

            FileVersionInfo fi = null;
            try
            {
                fi = FileVersionInfo.GetVersionInfo(_strCurrentFile);
                _vCurrentVersion = new Version(fi.FileVersion);
            }

            catch
            {
                _vCurrentVersion = new Version(0,0,0,0);   
            }
            
            

            /* Build version from online- file (string) */
            _vOnlineVersion = new Version(GetStringItems(0, strSource));
            _strPathToExecutable = GetStringItems(1, strSource);
            _strPathToChanges = GetStringItems(2, strSource);
        }

        /* Parses out a string of Line x */
        private string GetStringItems(int line, string source)
        {
            /* Is Like
              1  Version=0.0.1.0
              2  Path=https://dl.dropbox.com/u/62845853/AnotherSc2Hack/Binaries/Another%20SC2%20Hack.exe
              3  Changes=https://dl.dropbox.com/u/62845853/AnotherSc2Hack/UpdateFiles/Changes */

            var strmoreSource = source.Split('\n');
            if (strmoreSource[line].Contains("\r"))
                strmoreSource[line] = strmoreSource[line].Substring(0, strmoreSource[line].IndexOf('\r'));

            return strmoreSource[line];
        }

        /* Parse the changes File */
        private void ParseChangesFile()
        {
            Graphics g = CreateGraphics();

            /* Connect to server */
            var privateWebClient = new WebClient();
            privateWebClient.Proxy = null;

            var strSource = string.Empty;

            try
            {
                strSource = privateWebClient.DownloadString(_strPathToChanges);
            }

            catch
            {
                MessageBox.Show("Can not reach Server!\n\nTry later!", "FAILED");
                Close();
                return;
            }

            var strSplit = strSource.Split('\n');



            

            for (var i = 0; i < strSplit.Length; i++)
            {
                if (strSplit[i].EndsWith("\r"))
                    strSplit[i] = strSplit[i].Substring(0, strSplit[i].IndexOf('\r'));
            }


            for (var i = 0; i < strSplit.Length; i++)
            {
                if (strSplit[i].StartsWith("Header"))
                {
                    lblHeader.Text = strSplit[i].Substring(7, strSplit[i].Length - 7);
                    continue;
                }

                rtbItems.Text += strSplit[i];
                rtbItems.Text += "\n";
            }

                return;


            
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
           
        }
    }
}
