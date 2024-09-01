using Serilog;

namespace Application
{
    public static class TelthemwebLogException
    {
         public static void LogException(Exception ex)
        {
            LogtoFile(ex.Message);

            LogtoDebug(ex.Message);

            LogtoConsole(ex.Message);
        }

        public static void LogtoFile(string? message) => Log.Information(message);

        public static void LogtoDebug(string? message) => Log.Warning(message);

        public static void LogtoConsole(string? message) => Log.Debug(message);
    }
}