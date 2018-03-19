using System.Collections.Specialized;

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

        public string GetPreviousSongPath()
        {
            if (_playlist.Count > 0 && _currentItemIndex > 0)
            {
                _currentItemIndex -= 1;
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

        public void Add(string[] filePaths)
        {
            _playlist.AddRange(filePaths);
        }

        public void Add(string filePath)
        {
            _playlist.Add(filePath);
        }

        public int Count()
        {
            return _playlist.Count;
        }
    }
}
