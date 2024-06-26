using System.Diagnostics;

using BepInEx.Logging;

namespace HC_VRTrial.Logging
{
    public class PluginLog
    {
        public static void Setup(ManualLogSource logger)
        {
            LogSource = logger;
        }

        private static ManualLogSource LogSource { get; set; }

        public static void Debug(object data)
        {
            LogSource.LogDebug(Format(data));
        }

        public static void Info(object data)
        {
            LogSource.LogInfo(Format(data));
        }

        public static void Warning(object data)
        {
            LogSource.LogWarning(Format(data));
        }

        public static void Error(object data)
        {
            LogSource.LogError(Format(data));
        }

        private static string Format(object data)
        {
            var methodBase = new StackTrace().GetFrame(2)?.GetMethod();
            var className = methodBase?.ReflectedType?.Name;
            var methodName = methodBase?.Name;
            return $"[{className}.{methodName}] {data}";
        }
    }
}
