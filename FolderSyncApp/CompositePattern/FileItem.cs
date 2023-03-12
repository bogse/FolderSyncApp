public class FileItem : ComponentItem
{
    public FileItem(string path) : base(path)
    {
    }

    public override void Sync(string replicaPath, Logger logger)
    {
        string fileName = Path.GetFileName(_path);
        string replicaFilePath = Path.Combine(replicaPath, fileName);

        if (FileNeedsCopying(replicaFilePath))
        {
            CopyFile(replicaFilePath, logger);
        }
    }

    private bool FileNeedsCopying(string replicaFilePath)
    {
        if (!File.Exists(replicaFilePath))
        {
            return true;
        }

        FileInfo fileInfo = new FileInfo(_path);
        FileInfo replicaFileInfo = new FileInfo(replicaFilePath);

        if (replicaFileInfo.LastWriteTimeUtc != fileInfo.LastWriteTimeUtc)
        {
            return true;
        }

        return false;
    }

    private void CopyFile(string replicaFilePath, Logger logger)
    {
        File.Copy(_path, replicaFilePath, true);

        FileInfo replicaFileInfo = new FileInfo(replicaFilePath);
        replicaFileInfo.LastWriteTimeUtc = new FileInfo(_path).LastWriteTimeUtc;

        logger.Log($"Copied file: {_path} to {replicaFilePath}");
    }
}
