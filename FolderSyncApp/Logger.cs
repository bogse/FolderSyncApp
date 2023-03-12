public class Logger
{
    private readonly string _logFilePath;

    public Logger(string logFilePath)
    {
        _logFilePath = logFilePath;

        if (!File.Exists(_logFilePath))
        {
            StreamWriter writer = File.CreateText(_logFilePath);
            Log("Created log file: logFilePath");
        }
    }

    public void Log(string message)
    {
        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        string logLine = $"{timestamp} {message}";

        Console.WriteLine(logLine);

        int retries = 3;
        bool success = false;
        while (retries > 0 && !success)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(_logFilePath))
                {
                    writer.WriteLine(logLine);
                    success = true;
                }
            }
            catch (IOException ex)
            {
                retries--;
                Thread.Sleep(1000);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Failed to write to log file due to unauthorized access: {ex.Message}");
                Console.WriteLine("Syncronization will continue, but logs will not be created.");
                break;
            }
        }
    }
}
