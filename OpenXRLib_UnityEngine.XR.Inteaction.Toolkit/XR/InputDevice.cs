namespace UnityEngine.XR
{
    public static class InputDeviceExtensions
    {
        public static bool TryGetFeatureValue(this InputDevice device, string featureName, out bool result)
        {
            result = default;
            return device.IsValidId() && InputDevices.TryGetFeatureValue_bool(device.deviceId, featureName, out result);
        }

        public static bool TryGetFeatureValue(this InputDevice device, string featureName, out float result)
        {
            result = default;
            return device.IsValidId() && InputDevices.TryGetFeatureValue_float(device.deviceId, featureName, out result);
        }

        public static bool TryGetFeatureValue(this InputDevice device, string featureName, out Vector2 result)
        {
            result = default;
            return device.IsValidId() && InputDevices.TryGetFeatureValue_Vector2f(device.deviceId, featureName, out result);
        }

        public static bool TryGetFeatureValue(this InputDevice device, string featureName, out Vector3 result)
        {
            result = default;
            return device.IsValidId() && InputDevices.TryGetFeatureValue_Vector3f(device.deviceId, featureName, out result);
        }

        public static bool TryGetFeatureValue(this InputDevice device, string featureName, out Quaternion result)
        {
            result = default;
            return device.IsValidId() && InputDevices.TryGetFeatureValue_Quaternionf(device.deviceId, featureName, out result);
        }

        public static bool TryGetFeatureValue(this InputDevice device, string featureName, out InputTrackingState result)
        {
            uint value = 0u;
            if (device.IsValidId() && InputDevices.TryGetFeatureValue_UInt32(device.deviceId, featureName, out value))
            {
                result = (InputTrackingState)value;
                return true;
            }
            result = InputTrackingState.None;
            return false;
        }
    }
}
