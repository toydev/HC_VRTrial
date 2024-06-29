using System.IO;

using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.Attributes;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem.XR;
using Unity.XR.CoreUtils;

using HC_VRTrial.Logging;
using UnityEngine.InputSystem;

namespace HC_VRTrial.VRUtils
{
    /// <summary>
    /// A VR camera capable of projecting images onto the HMD and supporting HMD tracking.
    /// 
    /// The internal structure of the game objects is as follows:
    /// - parentGameObject
    ///   - Origin: VR Camera's origin
    ///     - Camera: Normal and VR Camera
    /// </summary>
    public class VRCamera : MonoBehaviour
    {
        static VRCamera() { ClassInjector.RegisterTypeInIl2Cpp<VRCamera>(); }

        public static VRCamera Create(GameObject parentGameObject, string name, int depth)
        {
            var gameObject = new GameObject($"{parentGameObject.name}{name}Origin");
            // Ensure the lifecycle of the GameObject is synchronized with its parent.
            gameObject.transform.parent = parentGameObject.transform;
            gameObject.SetActive(false);
            var result = gameObject.AddComponent<VRCamera>();
            result.Depth = depth;
            gameObject.SetActive(true);
            return result;
        }

        public static bool IsBaseHeadSet { get; private set; } = false;
        public static Vector3 BaseHeadPosition { get; private set; } = Vector3.zero;
        public static Quaternion BaseHeadRotation { get; private set; } = Quaternion.identity;

        /// <summary>
        /// Sets the current head position and rotation as the center point for future viewpoints.
        /// </summary>
        public static void UpdateViewport(VRCamera vrCamera)
        {
            IsBaseHeadSet = true;
            BaseHeadPosition = InputTracking.GetLocalPosition(XRNode.Head);
            var orientationEulerAngles = InputTracking.GetLocalRotation(XRNode.Head).eulerAngles;
            BaseHeadRotation = Quaternion.Euler(
                PluginConfig.ReflectHMDRotationXOnViewport.Value ? orientationEulerAngles.x : 0,
                PluginConfig.ReflectHMDRotationYOnViewport.Value ? orientationEulerAngles.y : 0,
                PluginConfig.ReflectHMDRotationZOnViewport.Value ? orientationEulerAngles.z : 0);
        }

        private int Depth { get; set; }

        private InputAction PositionAction;
        private InputAction RotationAction;
        void Awake()
        {
            PluginLog.Info($"Awake: {name}");
            var actionMap = GetInputActionAsset().FindActionMap("XR");
            PluginLog.Info($"actionMap: {actionMap != null}");
            PositionAction = actionMap.FindAction("Position");
            PluginLog.Info($"PositionAction: {PositionAction != null}");
            PositionAction.Enable();
            RotationAction = actionMap.FindAction("Rotation");
            PluginLog.Info($"RotationAction: {RotationAction != null}");
            RotationAction.Enable();
            Setup();
        }

        void OnDestroy()
        {
            PluginLog.Debug($"OnDestroy: {name}");
        }

        private GameObject OriginObject { get; set; }
        [HideFromIl2Cpp] private XROrigin Origin { get; set; }
        private GameObject CameraOffsetObject { get; set; }
        private GameObject CameraObject { get; set; }
        private Camera Camera { get; set; }

        private void Setup()
        {
            if (!CameraOffsetObject)
            {
                CameraOffsetObject = new GameObject($"{name}CameraOffset");
            }

            if (!CameraObject)
            {
                CameraObject = new GameObject($"{name}Camera");
                CameraObject.transform.SetParent(CameraOffsetObject.transform, false);
                Camera = CameraObject.AddComponent<Camera>();
                var trackedPoseDriver = CameraObject.AddComponent<TrackedPoseDriver>();
                trackedPoseDriver.positionAction = PositionAction;
                trackedPoseDriver.rotationAction = RotationAction;
            }

            if (!OriginObject)
            {
                OriginObject = new GameObject($"{name}Origin");
                OriginObject.transform.parent = gameObject.transform;
                OriginObject.SetActive(false);
                Origin = OriginObject.AddComponent<XROrigin>();
                Origin.Camera = Camera;
                Origin.CameraFloorOffsetObject = CameraOffsetObject;
                OriginObject.SetActive(true);
                CameraOffsetObject.transform.SetParent(Origin.transform, false);
            }
        }

        /// <summary>
        /// Hijacks the viewpoint of a camera and displays it through the VR camera.
        /// </summary>
        /// <param name="targetCamera">The target camera.</param>
        /// <param name="useCopyFrom">If true, copies the camera settings using Camera.CopyFrom. Specify false to adjust the camera settings independently.</param>
        /// <param name="synchronization">If true, synchronizes some of the camera settings in real-time. Refer to CameraHijacker.Synchronize for detailed synchronization content.</param>
        public void Hijack(Camera targetCamera, bool useCopyFrom = true, bool synchronization = true)
        {
            Setup();

            /*
            if (targetCamera != null)
            {
                CameraHijacker.Hijack(targetCamera, Normal, useCopyFrom, synchronization);
                PluginLog.Info("Hijack 2");

                // Set origin to the inverse position of the base head from the target camera.
                // The origin of the VR camera is the center of the play area (Usually at the player's feet).
                PluginLog.Info($"VR: {VR != null}");
                PluginLog.Info($"VR.Origin: {VR.Origin != null}");
                PluginLog.Info($"VR.Origin.transform: {VR.Origin.transform != null}");
                PluginLog.Info($"targetCamera: {targetCamera != null}");
                PluginLog.Info($"targetCamera.transform: {targetCamera.transform != null}");
                VR.Origin.transform.rotation = targetCamera.transform.rotation * Quaternion.Inverse(BaseHeadRotation);
                PluginLog.Info("Hijack 3");
                VR.Origin.transform.position = targetCamera.transform.position - VR.Origin.transform.rotation * BaseHeadPosition;
                PluginLog.Info("Hijack 4");
                VR.Origin.transform.SetParent(targetCamera.transform);
                PluginLog.Info("Hijack 5s");
            }

            Normal.depth = Depth;
            */
        }

        public void Update()
        {
            /*
            if (PositionAction != null && RotationAction != null)
            {
                PluginLog.Info($"PositionAction value: {PositionAction.ReadValue<Vector3>()}");
                PluginLog.Info($"RotationAction value: {RotationAction.ReadValue<Quaternion>()}");
            }
            else
            {
                PluginLog.Warning("Actions are null");
            }

            foreach (var device in InputSystem.devices)
            {
                PluginLog.Info("Device: " + device);
            }

            var trackedPoseDriver = CameraObject.GetComponent<TrackedPoseDriver>();
            if (trackedPoseDriver != null)
            {
                PluginLog.Info("TrackedPoseDriver is attached");
            }
            else
            {
                PluginLog.Error("TrackedPoseDriver is not attached");
            }
            */
        }

        private static InputActionAsset InputActionAsset;
        public static InputActionAsset GetInputActionAsset()
        {
            if (InputActionAsset == null)
            {
                InputActionAsset = InputActionAsset.FromJson(System.Text.Encoding.UTF8.GetString(ReadAllBytes($"{nameof(HC_VRTrial)}.Assets.inputactions.json")));
                PluginLog.Info($"InputActionAsset: {InputActionAsset != null}");
            }
            return InputActionAsset;
        }
        private static byte[] ReadAllBytes(string resourceName)
        {
            var assembly = typeof(VRCamera).Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
