using System;
using NAudio.Wave;

namespace AM.Core
{
    public class NAudioPlayer : IAudioPlayer, IDisposable
    {
        private readonly WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;
        private readonly IPlaylist _playlist;
        private bool _ignoreStop;

        public NAudioPlayer(IPlaylist playlist)
        {
            _playlist = playlist;
            _outputDevice = new WaveOutEvent();
            _audioFile = null;
            _outputDevice.PlaybackStopped += PlaybackStopped;
            _ignoreStop = false;
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (!_ignoreStop)
            {
                Next();
            }
            else
            {
                _ignoreStop = false;
            }
        }

        public void AddToPlaylist(string[] filePath)
        {
            _playlist.Add(filePath);
        }

        public void AddToPlaylist(string filePath)
        {
            _playlist.Add(filePath);
        }

        public int PlaylistItemCount()
        {
            return _playlist.Count;
        }

        public void VolumeUp()
        {
            if (_outputDevice.Volume < 1)
            {
                if (_outputDevice.Volume + 0.05f > 1.0f)
                {
                    _outputDevice.Volume = 1;
                }
                else
                {
                    _outputDevice.Volume += 0.05f;
                }
            }
        }

        public void VolumeDown()
        {
            if (_outputDevice.Volume > 0)
            {
                if (_outputDevice.Volume < 0.05f)
                {
                    _outputDevice.Volume = 0;
                }
                else
                {
                    _outputDevice.Volume -= 0.05f;
                }
            }
        }

        public float GetCurrentVolumeLevel()
        {
            return _outputDevice.Volume;
        }

        public string GetCurrentFileName()
        {
            return _playlist.GetCurrentSongPath();
        }

        public void Previous()
        {
            var previousSongPath = _playlist.GetPreviousSongPath();
            if (!string.IsNullOrEmpty(previousSongPath))
            {
                PlayFile(previousSongPath);
            }
        }

        public void Next()
        {
            var nextSongPath = _playlist.GetNextSongPath();
            if (!string.IsNullOrEmpty(nextSongPath))
            {
                PlayFile(nextSongPath);
            }
        }

        public void Resume()
        {
            var filePath = _playlist.GetCurrentSongPath();
            if (!string.IsNullOrEmpty(filePath))
            {
                PlayFile(filePath);
            }
        }

        public void Stop()
        {
            if (_audioFile != null)
            {
                _ignoreStop = true;
                _audioFile = null;
                _outputDevice.Stop();
            }
        }

        public void PlayPause()
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
            else
            {
                var filePath = _playlist.GetCurrentSongPath();
                if (!string.IsNullOrEmpty(filePath))
                {
                    PlayFile(filePath);
                }
            }
        }

        private void PlayFile(string filePath)
        {
            _audioFile = new AudioFileReader(filePath);
            _ignoreStop = true;
            _outputDevice.Stop();
            _outputDevice.Init(_audioFile);
            _outputDevice.Play();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _outputDevice.Dispose();
                _audioFile.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ClearPlaylist()
        {
            _playlist.Clear();
        }
    }
}