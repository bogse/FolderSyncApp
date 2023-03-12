namespace FolderSyncApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Correct format: <program.exe> <sourcePath> <replicaPath> <syncIntervalInSeconds> <logFilePath>");
                Console.WriteLine("Ex: FolderSyncApp.exe D:\\Source D:\\Destination 60 D:\\Log\\log.txt");
                return;
            }

            string sourcePath = args[0];
            string replicaPath = args[1];
            int syncInterval = int.Parse(args[2]);
            string logFilePath = args[3];

            if (logFilePath.StartsWith(replicaPath))
            {
                Console.WriteLine("Error: Log file cannot be inside the replica folder.");
                Console.WriteLine("Requirement was the replica to be an exact copy of source, so the file will be deleted during the sync");
                Console.WriteLine("In order to create a log file in the replica, create the log in the source.");
                return;
            }

            string logDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logger = new Logger(logFilePath);

            var sourceFolder = new FolderItem(sourcePath);
            var replicaFolder = new FolderItem(replicaPath);
            var syncManager = new SyncManager(sourceFolder, replicaFolder, syncInterval, logger);

            try
            {
                syncManager.Start();
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access to the replica folder '{replicaPath}' is denied: {ex.Message}");
                return;
            }

            Console.ReadKey();
            syncManager.Stop();
        }
    }
}