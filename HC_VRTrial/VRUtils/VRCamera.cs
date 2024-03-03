using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.Attributes;
using UnityEngine;
using Valve.VR;

using HC_VRTrial.Logging;

namespace HC_VRTrial.VRUtils
{
    public class VRCamera : MonoBehaviour
    {
        static VRCamera() { ClassInjector.RegisterTypeInIl2Cpp<VRCamera>(); }

        public static VRCamera Create(GameObject parentGameObject, string name, int depth)
        {
            var gameObject = new GameObject($"{parentGameObject.name}{name}");
            // Synchronized lifecycle
            gameObject.transform.parent = parentGameObject.transform;
            gameObject.SetActive(false);
            var result = gameObject.AddComponent<VRCamera>();
            result.Depth = depth;
            gameObject.SetActive(true);
            return result;
        }

        public static bool HasBaseHead { get; private set; } = false;
        public static Vector3 BaseHeadPosition { get; private set; } = Vector3.zero;
        public static Quaternion BaseHeadRotation { get; private set; } = Quaternion.identity;

        public static void UpdateBaseHead(VRCamera vrCamera)
        {
            // Set the current position and rotation of the head as the base head.
            HasBaseHead = true;
            BaseHeadPosition = vrCamera.VR.head.localPosition;
            var orientationEulerAngles = vrCamera.VR.head.localRotation.eulerAngles;
            BaseHeadRotation = Quaternion.Euler(
                PluginConfig.ReflectHMDRotationXOnViewport.Value ? orientationEulerAngles.x : 0,
                PluginConfig.ReflectHMDRotationYOnViewport.Value ? orientationEulerAngles.y : 0,
                PluginConfig.ReflectHMDRotationZOnViewport.Value ? orientationEulerAngles.z : 0);
        }

        private int Depth { get; set; }
        public Camera Normal { get; private set; }
        [HideFromIl2Cpp] public SteamVR_Camera VR { get; private set; }

        void Awake()
        {
            PluginLog.Debug($"Awake: {name}");
            Setup();
        }

        private void Setup()
        {
            // Prepare a VR camera separate from the game camera to minimize the impact on the game.
            if (!Normal)
            {
                Normal = gameObject.AddComponent<Camera>();
                Normal.depth = Depth;
            }

            // By combining Camera and SteamVR_Camera, the player can see the camera's view from the HMD.
            if (!gameObject.GetComponent<SteamVR_Camera>()) VR = gameObject.AddComponent<SteamVR_Camera>();
            // When SteamVR_TrackedObject is also combined, the camera moves with the movement of the HMD.
            if (!gameObject.GetComponent<SteamVR_TrackedObject>()) gameObject.AddComponent<SteamVR_TrackedObject>();

            // After that, just move the camera as you like.
            // This project camera usage is just one example.
        }

        public void Hijack(Camera targetCamera, bool useCopyFrom = true, bool synchronization = true)
        {
            Setup();

            if (targetCamera != null)
            {
                CameraHijacker.Hijack(targetCamera, Normal, useCopyFrom, synchronization);

                // Set origin to the inverse position of the base head from the target camera.
                // The origin of the VR camera is the center of the play area (Usually at the player's feet).
                VR.origin.rotation = targetCamera.transform.rotation * Quaternion.Inverse(BaseHeadRotation);
                VR.origin.position = targetCamera.transform.position - VR.origin.rotation * BaseHeadPosition;
                VR.origin.SetParent(targetCamera.transform);
            }

            Normal.depth = Depth;
        }
    }
}
