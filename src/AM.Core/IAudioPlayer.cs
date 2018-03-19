namespace AM.Core
{
    public interface IAudioPlayer
    {
        void AddToPlaylist(string[] filePath);

        void AddToPlaylist(string filePath);

        int PlaylistItemCount();

        void VolumeUp();

        void VolumeDown();

        void Previous();

        void Next();

        void Resume();

        void Stop();

        void PlayPause();
    }
}