namespace UnityEngine.InputSystem
{
    public class DebugEx
    {
        public static void LogException(System.Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError(e.StackTrace);
        }
    }
}
