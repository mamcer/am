using System;
using AM.Core;
using Xunit;

namespace AM.Test
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
    }
}