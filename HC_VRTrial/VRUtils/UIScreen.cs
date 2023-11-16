using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

using HC_VRTrial.Logging;

namespace HC_VRTrial.VRUtils
{
    public class UIScreen : MonoBehaviour
    {
        static UIScreen() { ClassInjector.RegisterTypeInIl2Cpp<UIScreen>(); }

        [HideFromIl2Cpp]
        public static UIScreen Create(
            GameObject parentGameObject,
            string name,
            int cameraDepth,
            int screenLayer,
            RenderTexture targetTexture,
            CameraClearFlags clearFlags = CameraClearFlags.Nothing
        )
        {
            var gameObject = new GameObject($"{parentGameObject.name}{name}");
            // Synchronized lifecycle
            gameObject.transform.parent = parentGameObject.transform;
            gameObject.SetActive(false);
            var result = gameObject.AddComponent<UIScreen>();
            result.TargetTexture = targetTexture;
            result.CameraDepth = cameraDepth;
            result.ScreenLayer = screenLayer;
            result.ClearFlags = clearFlags;
            gameObject.SetActive(true);
            return result;
        }

        public const string TRANSPARENT_SHADER_NAME = "Easy Masking Transition/Unlit/Transparent Tint";
        public const string COLOR_SHADER_NAME = "Unlit/Color";

        [HideFromIl2Cpp] public VRCamera Camera { get; private set; }
        private int CameraDepth { get; set; }
        private int ScreenLayer { get; set; }
        private RenderTexture TargetTexture { get; set; }
        private CameraClearFlags ClearFlags { get; set; }
        private GameObject Screen { get; set; }
        private GameObject MouseCursor { get; set; }

        [HideFromIl2Cpp]
        private void Setup()
        {
            if (!Camera || !Camera.Normal)
            {
                PluginLog.Info($"Setup Camera: {name}");
                Camera = VRCamera.Create(gameObject, nameof(Camera), CameraDepth);
                Camera.Normal.cullingMask = 1 << ScreenLayer;
                Camera.Normal.clearFlags = ClearFlags;
                Camera.Normal.nearClipPlane = 0.01f;  // 1cm
            }

            if (!Screen)
            {
                PluginLog.Debug($"Setup Screen: {name}");
                Screen = new GameObject($"{gameObject.name}Screen");
                Screen.transform.parent = transform;
                Screen.transform.localPosition = Vector3.zero;
                Screen.transform.localScale = new Vector3(TargetTexture.width / (float)TargetTexture.height, 1f, 1f);
                Screen.layer = ScreenLayer;
                var meshFilter = Screen.AddComponent<MeshFilter>();
                meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Quad.fbx");
                var material = new Material(Shader.Find(TRANSPARENT_SHADER_NAME))
                {
                    mainTexture = TargetTexture,
                };
                var meshRenderer = Screen.AddComponent<MeshRenderer>();
                meshRenderer.material = material;
            }

            if (!MouseCursor)
            {
                PluginLog.Info($"Setup MouseCursor: {name}");
                MouseCursor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                MouseCursor.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                MouseCursor.layer = ScreenLayer;
                MouseCursor.GetComponent<MeshRenderer>().material = new Material(Shader.Find(COLOR_SHADER_NAME))
                {
                    color = new Color(0.9f, 0.9f, 0.9f, 0.5f),
                };
                var mouseCursorCollider = MouseCursor.GetComponent<Collider>();
                if (mouseCursorCollider) Destroy(mouseCursorCollider);
            }
        }

        void Awake()
        {
            PluginLog.Debug($"Awake: {name}");
            Setup();
        }

        void Update()
        {
            if (0 <= Input.mousePosition.x && Input.mousePosition.x <= UnityEngine.Screen.width && 0 <= Input.mousePosition.y && Input.mousePosition.y <= UnityEngine.Screen.height)
            {
                // If the mouse cursor is inside the window, convert it to UI screen coordinates and display it.
                MouseCursor.SetActive(true);
                var newPosition = GetWorldPositionFromScreen(Input.mousePosition.x, Input.mousePosition.y);
                if (newPosition != null) MouseCursor.transform.position = newPosition.Value;
            }
            else
            {
                MouseCursor.SetActive(false);
            }
        }

        void OnDestroy()
        {
            PluginLog.Debug($"OnDestroy: {name}");
            if (Camera) Destroy(Camera);
            if (Screen) Destroy(Screen);
            if (MouseCursor) Destroy(MouseCursor);
        }

        public Vector3? GetWorldPositionFromScreen(float x, float y)
        {
            return Screen
                ? Screen.transform.TransformPoint(x / UnityEngine.Screen.width - 0.5f, y / UnityEngine.Screen.height - 0.5f, 0f)
                : null;
        }

        [HideFromIl2Cpp]
        public void LinkToFront(VRCamera targetCamera, float distance)
        {
            Setup();

            // Fix to origin of targetCamera.
            Camera.VR.origin.SetParent(targetCamera.VR.origin);
            Camera.VR.origin.localPosition = Vector3.zero;
            Camera.VR.origin.localRotation = Quaternion.identity;

            // Put the screen in front of the base head. The screen doesn't follow player's head movements.
            Screen.transform.SetParent(targetCamera.VR.origin);
            Screen.transform.localPosition = VRCamera.BaseHeadPosition + VRCamera.BaseHeadRotation * (distance * Vector3.forward);
            Screen.transform.localRotation = VRCamera.BaseHeadRotation;
            // Example: Always keep the screen in front of player's head.
            // Screen.transform.SetParent(targetCamera.VR.head);
            // Screen.transform.localPosition = distance * Vector3.forward;
            // Screen.transform.localRotation = Quaternion.identity;
        }
    }
}
