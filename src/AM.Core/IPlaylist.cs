namespace AM.Core
{
    public interface IPlaylist
    {
        string GetNextSongPath();

        string GetPreviousSongPath();

        void Add(string[] filePaths);

        void Add(string filePath);

        int Count();
    }
}