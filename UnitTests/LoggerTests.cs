namespace UnitTests
{
    [TestClass]
    public class LoggerTests
    {
        private const string LogFilePath = "test_log.txt";

        [TestMethod]
        public void Logger_Log_CreatesLogFileIfNotExists()
        {
            // Arrange
            if (File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }

            // Act
            Logger logger = new Logger(LogFilePath);
            logger.Log("Test message");

            // Assert
            Assert.IsTrue(File.Exists(LogFilePath));
        }

        [TestMethod]
        public void Logger_Log_WritesToLogFile()
        {
            // Arrange
            string logFilePath = "test.txt";

            Logger logger = new Logger(logFilePath);

            // Act
            logger.Log("Test message");
            Thread.Sleep(2000);

            // Assert
            string logContent = File.ReadAllText(logFilePath);
            Assert.IsTrue(logContent.Contains("Test message"));
        }
    }
}
