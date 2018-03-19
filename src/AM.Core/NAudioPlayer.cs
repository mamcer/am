using System;
using AM.Core;
using NAudio.Wave;

namespace AM.Core
{
    public class NAudioPlayer : IAudioPlayer, IDisposable
    {
        private readonly WaveOutEvent _outputDevice;
        private AudioFileReader _audioFile;
        private readonly IPlaylist _playlist;

        public NAudioPlayer(IPlaylist playlist)
        {
            _playlist = playlist;
            _outputDevice = new WaveOutEvent();
            _audioFile = null;
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
            return _playlist.Count();
        }

        public void VolumeUp()
        {
            if (_outputDevice.Volume < 1)
            {
                _outputDevice.Volume += 0.1f;
            }
        }

        public void VolumeDown()
        {
            if (_outputDevice.Volume > 0)
            {
                _outputDevice.Volume -= 0.1f;
            }
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
            if (_audioFile != null)
            {
                _outputDevice.Stop();
                _outputDevice.Play();
            }
        }

        public void Stop()
        {
            if (_audioFile != null)
            {
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
        }

        private void PlayFile(string filePath)
        {
            _audioFile = new AudioFileReader(filePath);
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
    }
}