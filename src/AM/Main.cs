using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;
using System.Text;
using NAudio.Wave;

namespace AM
{
    public partial class Main : Form
    {
        private KeyboardHook _hook;
        private bool _closeApplication;
        private WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;
        private StringCollection _playlist;
        private int _playlistIndex;

        public Main()
        {
            InitializeComponent();
            _outputDevice = new WaveOutEvent();
            _audioFile = null;
            _playlist = new StringCollection();

            RegisterKeyboardShortcuts();

            _closeApplication = false;
        }

        private void RegisterKeyboardShortcuts()
        {
            _hook = new KeyboardHook();
            // register the event that is fired after the key press.
            _hook.KeyPressed += hook_KeyPressed;
            // register all combination keys
            StringBuilder sb = new StringBuilder();
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.Insert);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.Home);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.PageDown);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.PageUp);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.End);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.Up);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.Right);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.Down);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.Left);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(AM.ModifierKeys.Control | AM.ModifierKeys.Alt,
                    Keys.I);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == (AM.ModifierKeys.Alt | AM.ModifierKeys.Control))
            {
                switch (e.Key)
                {
                    case Keys.Home:
                        {
                            PlayPause();
                        }
                        break;
                    case Keys.End:
                        {
                            Stop();
                        }
                        break;
                    case Keys.PageUp:
                    {
                        PlayNextSong();
                    }
                        break;
                    case Keys.PageDown:
                    {
                        PlayPreviousSong();
                    }
                        break;
                    case Keys.Insert:
                        {
                            RestartPlay();
                        }
                        break;
                    case Keys.Up:
                    {
                        VolumeUp();
                        }
                        break;
                    case Keys.Down:
                    {
                        VolumeDown();
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

        private void VolumeUp()
        {
            if (_outputDevice.Volume < 1)
            {
                _outputDevice.Volume += 0.1f;
            }
        }

        private void VolumeDown()
        {
            if (_outputDevice.Volume > 0)
            {
                _outputDevice.Volume -= 0.1f;
            }
        }

        private void PlayPreviousSong()
        {
            if (_playlistIndex < 0 && _playlist.Count > 0)
            {
                _playlistIndex += 1;
                PlayCurrentItem();
            }
        }

        private void PlayNextSong()
        {
            if (_playlistIndex < _playlist.Count)
            {
                _playlistIndex += 1;
                PlayCurrentItem();
            }
        }

        private void RestartPlay()
        {
            if (_audioFile != null)
            {
                _outputDevice.Stop();
                _outputDevice.Play();
            }
        }

        private void Stop()
        {
            if (_audioFile != null)
            {
                _outputDevice.Stop();
            }
        }

        private void PlayPause()
        {
            if (_audioFile != null)
            {
                if (_outputDevice.PlaybackState != PlaybackState.Playing)
                {
                    _outputDevice.Play();
                }
                else
                {
                    _outputDevice.Pause();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_closeApplication)
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
            _closeApplication = true;
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
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = folderBrowserDialog1.SelectedPath;
                var fileCount = ScanFolderAndAddFilesToPlaylist(selectedPath);
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "Info";
                notifyIcon1.BalloonTipText = $"Total {fileCount} files added";
                notifyIcon1.ShowBalloonTip(1000);
                PlayCurrentItem();
            }
        }

        private void PlayCurrentItem()
        {
            if (_playlistIndex < _playlist.Count)
            {
                _audioFile = new AudioFileReader(_playlist[_playlistIndex]);
                _outputDevice.Stop();
                _outputDevice.Init(_audioFile);
                _outputDevice.Play();
            }
        }

        private int ScanFolderAndAddFilesToPlaylist(string directoryPath)
        {
            string[] mp3Files = Directory.GetFiles(directoryPath, "*.mp3", SearchOption.AllDirectories);
            _playlist.Clear();
            _playlist.AddRange(mp3Files);
            _playlistIndex = 0;

            return _playlist.Count;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _playlist.Clear();
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Info";
            notifyIcon1.BalloonTipText = "All elements from the playlist have been removed";
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] mp3Files = (string[])e.Data.GetData(DataFormats.FileDrop);
            int fileAdded = 0;
            foreach (string filePath in mp3Files)
            {
                if (Directory.Exists(filePath))
                {
                    fileAdded += ScanFolderAndAddFilesToPlaylist(filePath);
                    continue;
                }
                if (Path.GetExtension(filePath) == ".mp3")
                {
                    _playlist.Add(filePath);
                    fileAdded += 1;
                }
            }
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Info";
            notifyIcon1.BalloonTipText = $"Total {fileAdded} files added";
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
            // TODO: fixit

            //if (axWindowsMediaPlayer1.Ctlcontrols.currentItem != null)
            //{
            //    notifyIcon1.BalloonTipIcon = ToolTipIcon.None;
            //    notifyIcon1.BalloonTipTitle = "File Info";
            //    string artist = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Artist");
            //    string title = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Title");
            //    string year = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("WM/Year");
            //    string album = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Album");
            //    string bitrate = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Bitrate");
            //    if (string.IsNullOrEmpty(axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("IsVBR"))
            //        || axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("IsVBR") == "False")
            //    {
            //        notifyIcon1.BalloonTipText = $"{artist} - {title}\n{year} - {album}\n{bitrate} kbit";
            //    }
            //    else
            //    {
            //        notifyIcon1.BalloonTipText = $"{artist} - {title}\n{year} - {album}\n{bitrate} kbit (VBR)";
            //    }
            //    notifyIcon1.ShowBalloonTip(3000);
            //    //Artista - Tema
            //    //Año - Album
            //    //Rate
            //}
        }

        private void RemoveFromPlaylist()
        {
            // TODO: fixit
            //axWindowsMediaPlayer1.currentPlaylist.removeItem(axWindowsMediaPlayer1.currentMedia);
            //axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RemoveFromPlaylist();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            // TODO: fixit
            //if (axWindowsMediaPlayer1.Ctlcontrols.currentItem != null)
            //{
            //    string artist = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Artist");
            //    string title = axWindowsMediaPlayer1.Ctlcontrols.currentItem.getItemInfo("Title");

            //    notifyIcon1.Text = $"{artist} - {title}";
            //}
        }
    }
}