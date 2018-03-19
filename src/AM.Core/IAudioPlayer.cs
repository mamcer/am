namespace AM.Core
{
    public interface IAudioPlayer
    {
        void AddToPlaylist(string[] filePath);

        void AddToPlaylist(string filePath);

        int PlaylistItemCount();

        void ClearPlaylist();

        void VolumeUp();

        void VolumeDown();

        float GetCurrentVolumeLevel();

        string GetCurrentFileName();

        void Previous();

        void Next();

        void Resume();

        void Stop();

        void PlayPause();
    }
}