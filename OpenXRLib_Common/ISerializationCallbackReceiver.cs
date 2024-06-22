namespace UnityEngine
{
    public interface ISerializationCallbackReceiverExtensions
    {
        void OnBeforeSerialize();
        void OnAfterDeserialize();
    }
}
