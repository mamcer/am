using System;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text;

using KeyboardUtilities;

namespace AM
{
    public partial class Golem : Form
    {
        private KeyboardHook hook;
        private string destinationFolder;
        private bool closeApplication;
        private WMPLib.IWMPPlaylist playlist;

        public Golem()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.Visible = false;
            this.playlist = axWindowsMediaPlayer1.newPlaylist("lalala", string.Empty);
            axWindowsMediaPlayer1.currentPlaylist = playlist;

            hook = new KeyboardHook();
            // register the event that is fired after the key press.
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register all combination keys
            StringBuilder sb = new StringBuilder();
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.Insert);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.Home);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.PageDown);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.PageUp);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.End);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.Up);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.Right);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.Down);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.Left);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.F12);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                hook.RegisterHotKey(KeyboardUtilities.ModifierKeys.Control | KeyboardUtilities.ModifierKeys.Alt,
                    Keys.I);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }

            this.destinationFolder = ConfigurationManager.AppSettings["DestinationFolder"];
            if (Directory.Exists(destinationFolder) == false)
            {
                sb.AppendLine(string.Format("No se encuentra el directorio:'{0}'", destinationFolder));
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.closeApplication = false;
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == (KeyboardUtilities.ModifierKeys.Alt | KeyboardUtilities.ModifierKeys.Control))
            {
                switch (e.Key)
                {
                    case Keys.Home:
                        {
                            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                            {
                                axWindowsMediaPlayer1.Ctlcontrols.play();
                            }
                            else
                            {
                                axWindowsMediaPlayer1.Ctlcontrols.pause();
                            }
                        }
                        break;
                    case Keys.End:
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.stop();
                        }
                        break;
                    case Keys.PageUp:
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.next();
                        }
                        break;
                    case Keys.PageDown:
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.previous();
                        }
                        break;
                    case Keys.Insert:
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.stop();
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                        }
                        break;
                    case Keys.Right:
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition += 10;
                        }
                        break;
                    case Keys.Left:
                        {
                            axWindowsMediaPlayer1.Ctlcontrols.currentPosition -= 10;
                        }
                        break;
                    case Keys.Up:
                        {
                            axWindowsMediaPlayer1.settings.volume += 1;
                        }
                        break;
                    case Keys.Down:
                        {
                            axWindowsMediaPlayer1.settings.volume -= 1;
                        }
                        break;
                    case Keys.F12:
                        {
                            backgroundWorker1.RunWorkerAsync();
                        }
                        break;
                    case Keys.I:
                        {
                            ShowCurrentTrackInfo();
                        }
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string filePath = axWindowsMediaPlayer1.currentMedia.sourceURL;
            string fileName = Path.GetFileName(filePath);
            try
            {
                File.Copy(filePath, destinationFolder + fileName);
            }
            catch (IOException ex)
            {
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                notifyIcon1.BalloonTipTitle = "Error";
                notifyIcon1.BalloonTipText = string.Format(string.Format("No se pudo copiar el archivo '{0}' - {1}", fileName, ex.Message));
                notifyIcon1.ShowBalloonTip(3000);
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Copia finalizada";
            notifyIcon1.BalloonTipText = "Copia finalizada con exito";
            notifyIcon1.ShowBalloonTip(500);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.closeApplication)
            {
                WindowState = FormWindowState.Minimized;
                Visible = false;
                e.Cancel = true;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.closeApplication = true;
            Close();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int fileCount = 0;
            AddFolder addFolder = new AddFolder();
            if(addFolder.ShowDialog() == DialogResult.OK)
            {
                fileCount = ScanFolderAndAddFilesToPlaylist(addFolder.SelectedPath);
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "Info";
                notifyIcon1.BalloonTipText = string.Format("Total {0} archivos agregados", fileCount);
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private int ScanFolderAndAddFilesToPlaylist(string directoryPath)
        {
            string[] mp3Files = Directory.GetFiles(directoryPath, "*.mp3", SearchOption.AllDirectories);
            foreach (string filePath in mp3Files)
            {
                this.playlist.appendItem(axWindowsMediaPlayer1.newMedia(filePath));
            }
            return mp3Files.Length;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.playlist.clear();
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Info";
            notifyIcon1.BalloonTipText = "Se eliminaron todos los elementos de la playlist";
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void Golem_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void Golem_DragDrop(object sender, DragEventArgs e)
        {
            string[] mp3Files = (string[])e.Data.GetData(DataFormats.FileDrop);
            int fileAdded = 0;
            foreach (string filePath in mp3Files)
            {
                if(Directory.Exists(filePath))
                {
                    fileAdded += ScanFolderAndAddFilesToPlaylist(filePath);
                    continue;
                }
                if (Path.GetExtension(filePath) == ".mp3")
                {
                    this.playlist.appendItem(axWindowsMediaPlayer1.newMedia(filePath));
                    fileAdded += 1;
                }
            }
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Info";
            notifyIcon1.BalloonTipText = string.Format("Total {0} archivos agregados", fileAdded);
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowHelp();
        }

        private void ShowHelp()
        {
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Help";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Ctrl-Alt-Home Pause");
            sb.AppendLine("Ctrl-Alt-End Stop");
            sb.AppendLine("Ctrl-Alt-PageUp Next");
            sb.AppendLine("Ctrl-Alt-PageDown Previous");
            sb.AppendLine("Ctrl-Alt-Insert Play");
            sb.AppendLine("Ctrl-Alt-Right Forward 10 sec.");
            sb.AppendLine("Ctrl-Alt-Left Backward 10 sec.");
            sb.AppendLine("Ctrl-Alt-Up Volume increase");
            sb.AppendLine("Ctrl-Alt-Down Volume decrease");
            sb.AppendLine("Ctrl-Alt-F12 Copy File");
            notifyIcon1.BalloonTipText = sb.ToString();
            notifyIcon1.ShowBalloonTip(10000);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowAboutBox();
        }

        private void ShowAboutBox()
        {
            notifyIcon1.BalloonTipIcon = ToolTipIcon.None;
            notifyIcon1.BalloonTipTitle = "About";
            notifyIcon1.BalloonTipText = "Sound Golem\nAldebaran\nCopyright © Mario Moreno 2009";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void ShowCurrentTrackInfo()
        {
            if (axWindowsMediaPlayer1.Ctlcontrols.currentItem != null)
            {
                notifyIcon1.BalloonTipIcon = ToolTipIcon.None;
                notifyIcon1.BalloonTipTitle = "File Info";
                string artist = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Artist");
                string title = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Title");
                string year = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("WM/Year");
                string album = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Album");
                string bitrate = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Bitrate");
                if (axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("IsVBR") == string.Empty
                    || axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("IsVBR") == "False")
                {
                    notifyIcon1.BalloonTipText = string.Format("{0} - {1}\n{2} - {3}\n{4} kbit", artist, title, year, album, bitrate);
                }
                else
                {
                    notifyIcon1.BalloonTipText = string.Format("{0} - {1}\n{2} - {3}\n{4} kbit (VBR)", artist, title, year, album, bitrate);
                }
                notifyIcon1.ShowBalloonTip(3000);
                //Artista - Tema
                //Año - Album
                //Rate
            }
        }

        private void RemoveFromPlaylist()
        {
            axWindowsMediaPlayer1.currentPlaylist.removeItem(axWindowsMediaPlayer1.currentMedia);
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RemoveFromPlaylist();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            if (axWindowsMediaPlayer1.Ctlcontrols.currentItem != null)
            {
                string artist = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Artist");
                string title = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Title");

                notifyIcon1.Text = string.Format("{0} - {1}", artist, title);
            }
        }
    }
}
