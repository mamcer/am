using System;
using System.Collections.Specialized;
using System.Linq;

namespace AM.Core
{
    public class SimplePlaylist : IPlaylist
    {
        private readonly StringCollection _playlist;
        private int _currentItemIndex;

        public SimplePlaylist()
        {
            _playlist = new StringCollection();
            _currentItemIndex = -1;
        }

        public void Add(string[] filePaths)
        {
            if (filePaths.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException(nameof(filePaths));
            }

            _playlist.AddRange(filePaths);
        }

        public void Add(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            _playlist.Add(filePath);
        }

        public string GetPreviousSongPath()
        {
            if (_playlist.Count > 0 && _currentItemIndex > 0)
            {
                _currentItemIndex -= 1;
                return _playlist[_currentItemIndex];
            }

            return string.Empty;
        }

        public string GetCurrentSongPath()
        {
            if (_playlist.Count > 0 && _currentItemIndex > 0 && _currentItemIndex < _playlist.Count)
            {
                return _playlist[_currentItemIndex];
            }

            return string.Empty;
        }

        public string GetNextSongPath()
        {
            if (_playlist.Count > 0 && _currentItemIndex < _playlist.Count)
            {
                _currentItemIndex += 1;
                return _playlist[_currentItemIndex];
            }

            return string.Empty;
        }

        public int Count => _playlist.Count;

        public void Clear()
        {
            _playlist.Clear();
        }
    }
}
