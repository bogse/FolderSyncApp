using Moq;

namespace UnitTests.CompositePatternTests
{
    [TestClass]
    public class FileItemTests
    {
        [TestMethod]
        public void Sync_AddsNewFileItems_WhenTheyExistOnlyInSource()
        {
            // Arrange
            string replicaPath = @"C:\temp\replica";
            var mockLogger = new Mock<Logger>("test");
            var folderItem = new FolderItem(@"C:\temp\source");

            Directory.CreateDirectory(@"C:\temp\source");
            File.WriteAllText(Path.Combine(@"C:\temp\source", "file1.txt"), "hello");

            // Act
            folderItem.Sync(replicaPath, mockLogger.Object);

            // Assert
            Assert.IsTrue(File.Exists(Path.Combine(replicaPath, "file1.txt")));
        }
    }
}
