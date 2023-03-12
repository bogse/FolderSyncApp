public class SyncManager
{
    private readonly ComponentItem _source;
    private readonly ComponentItem _replica;
    private readonly int _syncInterval;
    private readonly Logger _logger;

    private Timer _timer;
    private bool _isSyncing;

    public SyncManager(ComponentItem source, ComponentItem replica, int syncInterval, Logger logger)
    {
        _source = source;
        _replica = replica;
        _syncInterval = syncInterval * 1000;
        _logger = logger;
    }

    public void Start()
    {
        if (_timer == null)
        {
            _timer = new Timer(SyncTimerCallback, null, 0, _syncInterval);
            _source.Sync(_replica._path, _logger);
        }
    }

    public void Stop()
    {
        if (_timer != null)
        {
            _timer.Dispose();
            _timer = null;
        }
    }

    private void SyncTimerCallback(object state)
    {
        if (_isSyncing)
        {
            _logger.Log("Previous sync is still in progress.");
            return;
        }

        try
        {
            _isSyncing = true;

            _logger.Log("Starting synchronization...");

            _source.Sync(_replica._path, _logger);

            _logger.Log("Synchronization completed.");
        }
        catch (Exception ex)
        {
            _logger.Log($"Synchronization failed: {ex.Message}");
        }
        finally
        {
            _isSyncing = false;
        }
    }
}
