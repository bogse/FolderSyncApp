using Moq;

namespace UnitTests
{
    [TestClass]
    public class SyncManagerTests
    {
        [TestMethod]
        public void SyncManager_Start_SetsUpTimerAndStartsSync()
        {
            // Arrange
            var source = new Mock<ComponentItem>("source/path");
            var replica = new Mock<ComponentItem>("replica/path");
            var logger = new Mock<Logger>("test");
            var syncInterval = 5;
            var syncIntervalInMillis = syncInterval * 1000;

            var syncManager = new SyncManager(source.Object, replica.Object, syncInterval, logger.Object);

            // Act
            syncManager.Start();

            // Assert

            source.Verify(x => x.Sync(It.IsAny<string>(), logger.Object), Times.AtLeastOnce());
            syncManager.Stop();
        }

        [TestMethod]
        public void SyncManager_Stop_StopsTimer()
        {
            var source = new Mock<ComponentItem>("source/path");
            var replica = new Mock<ComponentItem>("replica/path");
            var logger = new Mock<Logger>("test");
            var syncInterval = 5;
            var syncIntervalInMillis = syncInterval * 1000;

            var syncManager = new SyncManager(source.Object, replica.Object, syncInterval, logger.Object);
            syncManager.Start();

            // Act
            syncManager.Stop();

            // Assert
            source.Verify(x => x.Sync(It.IsAny<string>(), logger.Object), Times.Once());
        }
    }
}
