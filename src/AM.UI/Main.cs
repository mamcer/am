using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AM.Core;

namespace AM.UI
{
    public partial class Main : Form
    {
        private KeyboardHook _hook;
        private bool _closeApplication;
        private readonly IAudioPlayer _player;

        public Main(IAudioPlayer player)
        {
            InitializeComponent();

            RegisterKeyboardShortcuts();

            _closeApplication = false;

            _player = player;

            ConsoleLog("Welcome to Sound Golem!");
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
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.Insert);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.Home);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.PageDown);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.PageUp);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.End);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.Up);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.Right);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.Down);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
                    Keys.Left);
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine(ioe.Message);
            }
            try
            {
                _hook.RegisterHotKey(UI.ModifierKeys.Control | UI.ModifierKeys.Alt,
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
            if (e.Modifier == (UI.ModifierKeys.Alt | UI.ModifierKeys.Control))
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
                            PlayPreviousSong();
                        }
                        break;
                    case Keys.PageDown:
                        {
                            PlayNextSong();
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
                }
            }
        }

        private void VolumeUp()
        {
            _player.VolumeUp();
            ConsoleLog($"Current volume {_player.GetCurrentVolumeLevel()*100:0.0}%");
        }

        private void VolumeDown()
        {
            _player.VolumeDown();
            ConsoleLog($"Current volume {_player.GetCurrentVolumeLevel()*100:0.0}%");
        }

        private void PlayPreviousSong()
        {
            _player.Previous();
            ConsoleLog($"Current song: {_player.GetCurrentFileName()}");
        }

        private void PlayNextSong()
        {
            _player.Next();
            ConsoleLog($"Current song: {_player.GetCurrentFileName()}");
        }

        private void RestartPlay()
        {
            _player.Resume();
            ConsoleLog($"Resume play: {_player.GetCurrentFileName()}");
        }

        private void Stop()
        {
            _player.Stop();
            ConsoleLog($"Stop play: {_player.GetCurrentFileName()}");
        }

        private void PlayPause()
        {
            _player.PlayPause();
            ConsoleLog($"Play/Pause: {_player.GetCurrentFileName()}");
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
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = folderBrowserDialog.SelectedPath;
                var fileCount = ScanFolderAndAddFilesToPlaylist(selectedPath);
                ConsoleLog($"Total {fileCount} files added");
            }
        }

        private int ScanFolderAndAddFilesToPlaylist(string directoryPath)
        {
            string[] mp3Files = Directory.GetFiles(directoryPath, "*.mp3", SearchOption.AllDirectories);
            _player.AddToPlaylist(mp3Files);

            return _player.PlaylistItemCount();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _player.ClearPlaylist();
            ConsoleLog("All elements from the playlist have been removed");
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
                    _player.AddToPlaylist(filePath);
                    fileAdded += 1;
                }
            }

            ConsoleLog($"Total {fileAdded} files added");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowHelp();
        }

        private void ShowHelp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Ctrl-Alt-Insert\t\tPlay");
            sb.AppendLine("Ctrl-Alt-Home\t\tPlay/Pause");
            sb.AppendLine("Ctrl-Alt-End\t\tStop");
            sb.AppendLine("Ctrl-Alt-PageUp\t\tNext");
            sb.AppendLine("Ctrl-Alt-PageDown\tPrevious");
            sb.AppendLine("Ctrl-Alt-Up\t\tVolume increase");
            sb.AppendLine("Ctrl-Alt-Down\t\tVolume decrease");

            MessageBox.Show(sb.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowAboutBox();
        }

        private void ShowAboutBox()
        {
            MessageBox.Show("Sound Golem\n\nMario Moreno 2009", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void ConsoleLog(string message)
        {
            txtConsole.Text += DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + @" - " + message + Environment.NewLine;
            txtConsole.SelectionStart = txtConsole.Text.Length;
            txtConsole.ScrollToCaret();
        }
    }
}