
namespace TEngine.Utils.Logging
{

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }

    public static class Logging
    {
        private static readonly object _lock = new object();
        private static LogLevel _minLevel = LogLevel.Debug;
        private static string _logFilePath = "logs/log.txt";
        private static bool _writeToFile = true;

        public static void Initialize(LogLevel minLevel, string logFilePath, bool writeToFile = true)
        {
            _minLevel = minLevel;
            _logFilePath = logFilePath;
            _writeToFile = writeToFile;

            if (_writeToFile)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath) ?? ".");
                File.WriteAllText(_logFilePath, "=== TEngine Log Start ===\n");
            }
        }

        public static void Log(LogLevel level, string message)
        {
            if (level < _minLevel) return;

            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            lock (_lock)
            {
                Console.WriteLine(logMessage);
                if (_writeToFile)
                {
                    try
                    {
                        File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"[Error] Failed to write to log file: {e.Message}");
                    }
                }
            }
        }

        public static void Debug(string message) => Log(LogLevel.Debug, message);
        public static void Info(string message) => Log(LogLevel.Info, message);
        public static void Warning(string message) => Log(LogLevel.Warning, message);
        public static void Error(string message) => Log(LogLevel.Error, message);
        public static void Critical(string message) => Log(LogLevel.Critical, message);
    }
}
