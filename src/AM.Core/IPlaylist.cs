namespace AM.Core
{
    public interface IPlaylist
    {
        string GetNextSongPath();

        string GetPreviousSongPath();

        string GetCurrentSongPath();

        void Add(string[] filePaths);

        void Add(string filePath);

        int Count { get; }

        void Clear();
    }
}