using Newtonsoft.Json;

namespace UnityEngine.InputSystem
{
    internal class JsonUtility
    {
        public static T FromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonException e)
            {
                Debug.LogError($"Json deserialization error: {e.Message}");
                return default(T);
            }
        }

        public static string ToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (JsonException e)
            {
                Debug.LogError($"Json serialization error: {e.Message}");
                return string.Empty;
            }
        }

        public static string ToJson(object obj, bool prettyPrint)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, prettyPrint ? Formatting.Indented : Formatting.None);
            }
            catch (JsonException e)
            {
                Debug.LogError($"Json serialization error: {e.Message}");
                return string.Empty;
            }
        }
    }
}
