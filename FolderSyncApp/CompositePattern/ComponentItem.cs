public abstract class ComponentItem
{
    protected internal string _path;

    public ComponentItem(string path)
    {
        _path = path;
    }

    public abstract void Sync(string replicaPath, Logger logger);
}
