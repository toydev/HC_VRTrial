using System;

namespace UnityEngine
{
    public class DebugExtensions
    {
        public static void LogException(Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError(e.StackTrace);
        }

        public static void LogException(Exception e, UnityEngine.Object context)
        {
            Debug.LogError(e.Message, context);
            Debug.LogError(e.StackTrace);
        }
    }
}
