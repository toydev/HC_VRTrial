namespace UnityEngine
{
    public class DebugEx
    {
        public static void LogException(System.Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError(e.StackTrace);
        }

        public static void LogException(System.Exception e, UnityEngine.Object context)
        {
            Debug.LogError(e.Message, context);
            Debug.LogError(e.StackTrace);
        }
    }
}
