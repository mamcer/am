using System;
using Xunit;

namespace AM.Core.Test
{
    public class SimplePlaylistTest
    {
        [Fact]
        public void ConstructorShoulInitializePlaylist()
        {
            // Arrange
            SimplePlaylist playlist; 

            // Act
            playlist = new SimplePlaylist();

            // Assert
            Assert.Equal(0, playlist.Count);
        }

        [Fact]
        public void AddWithArrayShouldAddElementsToCollection()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var items = new [] {"file01.mp3", "file02.mp3", "file03.mp3"};

            // Act
            playlist.Add(items);

            // Assert
            Assert.Equal(3, playlist.Count);
        }

        [Fact]
        public void AddArrayWithNullShouldThrowArgumentNullException()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string[] items = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => playlist.Add(items));
        }
        
        [Fact]
        public void AddArrayWithEmptyItemShouldThrowArgumentException()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string[] items = {"file01.mp3", string.Empty, "file03.mp3"};

            // Act & Assert
            Assert.Throws<ArgumentException>(() => playlist.Add(items));
        }

        [Fact]
        public void AddWithNullShouldThrowArgumentNullException()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string item = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => playlist.Add(item));
        }

        [Fact]
        public void AddWithEmptyShouldThrowArgumentNullException()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string item = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => playlist.Add(item));
        }

        [Fact]
        public void GetPreviousSongPathWithEmptyPlaylistShouldReturnStringEmpty()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string previousSongPath = "dummy-value";

            // Act
            previousSongPath = playlist.GetPreviousSongPath();

            // Assert
            Assert.Equal(string.Empty, previousSongPath);
        }

        [Fact]
        public void GetPreviousSongPathWithOneItemShouldReturnStringEmpty()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            playlist.Add(@"C:\music\song.mp3");
            string previousSongPath;

            // Act
            previousSongPath = playlist.GetPreviousSongPath();

            // Assert
            Assert.Equal(string.Empty, previousSongPath);
        }

        [Fact]
        public void GetPreviousSongPathWithMoreThanOneItemShouldReturnPreviousItem()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var song01 = @"C:\music\song01.mp3";
            playlist.Add(song01);
            playlist.Add(@"C:\music\song02.mp3");
            playlist.GetNextSongPath();
            string previousSongPath;

            // Act
            previousSongPath = playlist.GetPreviousSongPath();

            // Assert
            Assert.Equal(song01, previousSongPath);
        }

        [Fact]
        public void GetPreviousSongPathSecondCallWithTwoItemsShouldReturnStringEmpty()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            playlist.Add(@"C:\music\song01.mp3");
            playlist.Add(@"C:\music\song02.mp3");
            playlist.GetNextSongPath();
            string previousSongPath;

            // Act
            playlist.GetPreviousSongPath();
            previousSongPath = playlist.GetPreviousSongPath();

            // Assert
            Assert.Equal(string.Empty, previousSongPath);
        }

        [Fact]
        public void GetCurrentSongWithEmptyPlaylistShouldReturnStringEmpty()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string currentSongPath;

            // Act
            currentSongPath = playlist.GetCurrentSongPath();

            // Assert
            Assert.Equal(string.Empty, currentSongPath);
        }

        [Fact]
        public void GetCurrentSongWithOneItemShouldReturnItem()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var song01 = @"C:\music\song01.mp3";
            playlist.Add(song01);
            string currentSongPath;

            // Act
            currentSongPath = playlist.GetCurrentSongPath();

            // Assert
            Assert.Equal(song01, currentSongPath);
        }

        [Fact]
        public void GetCurrentSongShouldReturnCurrentItem()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var song01 = @"C:\music\song01.mp3";
            var song02 = @"C:\music\song02.mp3";
            playlist.Add(song01);
            playlist.Add(song02);
            string currentSongPath;

            // Act
            playlist.GetNextSongPath();
            currentSongPath = playlist.GetCurrentSongPath();

            // Assert
            Assert.Equal(song02, currentSongPath);
        }

        [Fact]
        public void GetNextSongWithEmptyPlaylistShouldReturnStringEmpty()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string nextSongPath;

            // Act
            nextSongPath = playlist.GetNextSongPath();

            // Assert
            Assert.Equal(string.Empty, nextSongPath);
        }

        [Fact]
        public void GetNextSongShouldReturnNextItem()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var song01 = @"C:\music\song01.mp3";
            var song02 = @"C:\music\song02.mp3";
            playlist.Add(song01);
            playlist.Add(song02);
            string nextSongPath;

            // Act
            nextSongPath = playlist.GetNextSongPath();

            // Assert
            Assert.Equal(song02, nextSongPath);
        }

        [Fact]
        public void GetNextSongShouldOfLatestItemShouldReturnStringEmpty()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var song01 = @"C:\music\song01.mp3";
            var song02 = @"C:\music\song02.mp3";
            playlist.Add(song01);
            playlist.Add(song02);
            string nextSongPath;

            // Act
            playlist.GetNextSongPath();
            nextSongPath = playlist.GetNextSongPath();

            // Assert
            Assert.Equal(string.Empty, nextSongPath);
        }

        [Fact]
        public void ClearWithEmptyPlaylistShouldDoNothing()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            string currentSongPath;

            // Act
            playlist.Clear();
            currentSongPath = playlist.GetCurrentSongPath();

            // Assert
            Assert.Equal(string.Empty, currentSongPath);
        }
        
        [Fact]
        public void ClearWithItemsShouldClearPlaylist()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var song01 = @"C:\music\song01.mp3";
            var song02 = @"C:\music\song02.mp3";
            playlist.Add(song01);
            playlist.Add(song02);
            string currentSongPath;

            // Act
            playlist.Clear();
            currentSongPath = playlist.GetCurrentSongPath();

            // Assert
            Assert.Equal(string.Empty, currentSongPath);
        }

        [Fact]
        public void CountWithEmptyEmptyPlaylistShouldReturnZero()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            int count;

            // Act
            count = playlist.Count;

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void CountWithPlaylistWithItemsShouldReturnPlaylistItemCount()
        {
            // Arrange
            var playlist = new SimplePlaylist();
            var song01 = @"C:\music\song01.mp3";
            var song02 = @"C:\music\song02.mp3";
            playlist.Add(song01);
            playlist.Add(song02);
            int count;

            // Act
            count = playlist.Count;

            // Assert
            Assert.Equal(2, count);
        }
    }
}