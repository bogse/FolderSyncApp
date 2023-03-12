public class FolderItem : ComponentItem
{
    private readonly List<ComponentItem> _items = new List<ComponentItem>();

    public FolderItem(string path) : base(path)
    {
    }

    private void Add(ComponentItem item)
    {
        _items.Add(item);
    }
    private void Remove(ComponentItem item)
    {
        _items.Remove(item);
    }

    public override void Sync(string replicaPath, Logger logger)
    {
        lock (this)
        {
            EnsureReplicaFolderExists(replicaPath, logger);
            SynchronizeSubfolders(replicaPath, logger);
            DeleteUnneccesaryItems(replicaPath, logger);
            SynchronizeFiles(replicaPath, logger);

            foreach (var item in _items)
            {
                item.Sync(replicaPath, logger);
            }

            var sourceLastWriteTime = Directory.GetLastWriteTime(_path);
            Directory.SetLastWriteTime(replicaPath, sourceLastWriteTime);
        }
    }

    private void EnsureReplicaFolderExists(string replicaPath, Logger logger)
    {
        if (!Directory.Exists(replicaPath))
        {
            Directory.CreateDirectory(replicaPath);
            logger.Log($"Created folder: {replicaPath}");
        }
    }

    private void SynchronizeFiles(string replicaPath, Logger logger)
    {
        foreach (string sourceFilePath in Directory.GetFiles(_path))
        {
            string fileName = Path.GetFileName(sourceFilePath);
            string replicaFilePath = Path.Combine(replicaPath, fileName);

            if (File.Exists(replicaFilePath) != File.Exists(sourceFilePath))
            {
                ComponentItem fileItem = new FileItem(sourceFilePath);
                Add(fileItem);
            }

            if (File.Exists(replicaFilePath) && !File.Exists(sourceFilePath))
            {
                ComponentItem itemToRemove = _items.FirstOrDefault(item => item._path == sourceFilePath);
                if (itemToRemove != null)
                {
                    _items.Remove(itemToRemove);
                }
            }
        }
    }

    private void SynchronizeSubfolders(string replicaPath, Logger logger)
    {
        foreach (string sourceSubfolderPath in Directory.GetDirectories(_path))
        {
            string sourceSubfolderName = Path.GetFileName(sourceSubfolderPath);
            string replicaSubfolderPath = Path.Combine(replicaPath, sourceSubfolderName);

            var subfolderItem = new FolderItem(sourceSubfolderPath);
            subfolderItem.Sync(replicaSubfolderPath, logger);
        }
    }

    private void DeleteUnneccesaryItems(string replicaPath, Logger logger)
    {
        foreach (string replicaEntry in Directory.GetFileSystemEntries(replicaPath))
        {
            string replicaName = Path.GetFileName(replicaEntry);
            string sourcePath = Path.Combine(_path, replicaName);

            if (!FileOrDirectoryExists(sourcePath))
            {
                DeleteReplicaItem(replicaEntry, logger);
            }
        }
    }

    private bool FileOrDirectoryExists(string path)
    {
        return File.Exists(path) || Directory.Exists(path);
    }

    private void DeleteReplicaItem(string replicaEntry, Logger logger)
    {
        File.SetAttributes(replicaEntry, FileAttributes.Normal);
        if (File.Exists(replicaEntry))
        {
            File.Delete(replicaEntry);
            logger.Log($"Deleted file: {Path.GetFileName(replicaEntry)}");
        }
        else
        {
            Directory.Delete(replicaEntry, true);
            logger.Log($"Deleted folder: {Path.GetFileName(replicaEntry)}");
        }
    }
}