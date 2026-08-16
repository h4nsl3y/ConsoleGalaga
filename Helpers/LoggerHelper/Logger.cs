namespace Helpers.LoggerHelper
{
    public sealed class Logger : ILogger
    {
        #region Fields
        private static readonly Lazy<Logger> _instance = new(() => new Logger());
        private static readonly object _writeLock = new();

        private readonly string _filePath;
        private readonly string _logDirectory;
        #endregion

        #region Constructor
        private Logger()
        {
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            _filePath = Path.Combine(_logDirectory, "log.txt");
        }
        #endregion

        #region Public Methods
        public static ILogger Instance => _instance.Value;

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            Write("INFO", message);
        }

        public void LogError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            Write("ERROR", message);
        }
        #endregion

        #region Private Methods
        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        private void Write(string type, string message)
        {
            lock (_writeLock)
            {
                EnsureDirectoryExists();

                using var writer = new StreamWriter(_filePath, append: true);
                writer.WriteLine($"[{DateTime.UtcNow}] {type}: {message}");
            }
        }
        #endregion
    }
}
