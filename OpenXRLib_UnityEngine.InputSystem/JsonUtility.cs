namespace UnityEngine.InputSystem
{
    internal class JsonUtility
    {
        public static T FromJson<T>(string json) => default(T);
        public static string ToJson(object obj) => "";
        public static string ToJson(object obj, bool prettyPrint) => "";
    }
}
