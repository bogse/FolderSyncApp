using Moq;

namespace UnitTests.CompositePatternTests
{

    [TestClass]
    public class FolderItemTests
    {
        [TestMethod]
        public void Sync_CreatesReplicaFolder_WhenFolderDoesNotExist()
        {
            // Arrange
            string replicaPath = @"C:\temp\replica";
            var mockLogger = new Mock<Logger>("test");
            var folderItem = new FolderItem(@"C:\temp\source");

            // Act
            folderItem.Sync(replicaPath, mockLogger.Object);

            // Assert
            Assert.IsTrue(Directory.Exists(replicaPath)); ;
        }

        [TestMethod]
        public void Sync_DeletesUnnecessaryFolders_WhenTheyExistOnlyInReplica()
        {
            // Arrange
            string replicaPath = @"C:\temp\replica";
            var mockLogger = new Mock<Logger>("test");
            var folderItem = new FolderItem(@"C:\temp\source");

            Directory.CreateDirectory(replicaPath);
            File.Create(Path.Combine(replicaPath, "file1.txt"));
            Directory.CreateDirectory(Path.Combine(replicaPath, "subfolder1"));

            // Act
            folderItem.Sync(replicaPath, mockLogger.Object);

            // Assert
            Assert.IsFalse(Directory.Exists(Path.Combine(replicaPath, "subfolder1")));
        }

        [TestMethod]
        public void Sync_DeletesUnnecessaryFiles_WhenTheyExistOnlyInReplica()
        {
            // Arrange
            string replicaPath = @"C:\temp\replica";
            var mockLogger = new Mock<Logger>("test");
            var folderItem = new FolderItem(@"C:\temp\source");

            Directory.CreateDirectory(replicaPath);
            File.WriteAllText(Path.Combine(replicaPath, "file2.txt"), "hello");

            // Act

            folderItem.Sync(replicaPath, mockLogger.Object);

            // Assert
            Assert.IsFalse(File.Exists(Path.Combine(replicaPath, "file2.txt")));
        }
    }
}

