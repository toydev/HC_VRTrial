using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

using HC_VRTrial.Logging;

namespace HC_VRTrial.VRUtils
{
    /// <summary>
    /// UIScreen facilitates the display of UGUI/IMGUI elements on an HMD, using a VR camera setup.
    /// GUI elements are displayed as overlapping panels with positional offsets.
    /// It also supports displaying the mouse cursor.
    /// 
    /// The internal structure of the game objects is as follows:
    /// - parentGameObject
    ///   - UIScreen
    ///     - VRCamera Origin
    ///       -  Camera: Normal and VR Camera
    ///     - ScreenObject
    ///       - PanelObject[0]
    ///       - PanelObject[1]
    ///       - ...
    ///     - MouseCursor
    /// </summary>
    public class UIScreen : MonoBehaviour
    {
        static UIScreen() { ClassInjector.RegisterTypeInIl2Cpp<UIScreen>(); }

        /// <summary>
        /// Creates and initializes a UIScreen instance.
        /// </summary>
        /// <param name="parentGameObject">Parent game object.</param>
        /// <param name="name">Name.</param>
        /// <param name="cameraDepth">Camera depth.</param>
        /// <param name="screenLayer">Layer occupied for display.</param>
        /// <param name="panel">Layer occupied for display.</param>
        /// <param name="screenLayer">Layer occupied for display.</param>
        /// <param name="panels">Array of UIScreenPanel objects to be displayed on the UIScreen.</param>
        /// <param name="clearFlags">Camera clear flags to determine what the camera clears before rendering.</param>
        /// <returns>A new instance of UIScreen.</returns>
        [HideFromIl2Cpp]
        public static UIScreen Create(
            GameObject parentGameObject,
            string name,
            int cameraDepth,
            int screenLayer,
            UIScreenPanel[] panels,
            CameraClearFlags clearFlags = CameraClearFlags.Nothing
        )
        {
            var gameObject = new GameObject($"{parentGameObject.name}{name}");
            // Ensure the lifecycle of the GameObject is synchronized with its parent.
            gameObject.transform.parent = parentGameObject.transform;
            gameObject.SetActive(false);
            var result = gameObject.AddComponent<UIScreen>();
            result.Panels = panels;
            result.CameraDepth = cameraDepth;
            result.ScreenLayer = screenLayer;
            result.ClearFlags = clearFlags;
            gameObject.SetActive(true);
            return result;
        }

        public const string COLOR_SHADER_NAME = "Unlit/Color";

        [HideFromIl2Cpp] public VRCamera Camera { get; private set; }
        private int CameraDepth { get; set; }
        private int ScreenLayer { get; set; }
        [HideFromIl2Cpp] private UIScreenPanel[] Panels { get; set; }
        private CameraClearFlags ClearFlags { get; set; }
        private GameObject ScreenObject { get; set; }
        [HideFromIl2Cpp] private GameObject[] PanelObjects { get; set; }
        private GameObject MainPanelObject { get => PanelObjects != null && 1 <= PanelObjects.Length ? PanelObjects[0] : null; }
        private GameObject MouseCursor { get; set; }

        [HideFromIl2Cpp]
        private void Setup()
        {
            if (!Camera || !Camera.Normal)
            {
                Camera = VRCamera.Create(gameObject, nameof(Camera), CameraDepth);
                Camera.Normal.cullingMask = 1 << ScreenLayer;
                Camera.Normal.clearFlags = ClearFlags;
                Camera.Normal.nearClipPlane = 0.01f;  // 1cm
            }

            if (!ScreenObject)
            {
                ScreenObject = new GameObject($"{gameObject.name}{nameof(ScreenObject)}");
                // Ensure the lifecycle of the GameObject is synchronized with its parent.
                ScreenObject.transform.parent = transform;
                ScreenObject.transform.localPosition = Vector3.zero;
                ScreenObject.transform.localScale = Vector3.one;
            }

            PanelObjects ??= new GameObject[Panels.Length];
            for (var i = 0; i < Panels.Length; ++i)
            {
                if (!PanelObjects[i])
                {
                    var panelObject = new GameObject($"{gameObject.name}PanelObject{i}");
                    var panel = Panels[i];
                    // Ensure the lifecycle of the GameObject is synchronized with its parent.
                    panelObject.transform.parent = ScreenObject.transform;
                    panelObject.transform.localPosition = panel.Offset;
                    panelObject.transform.localScale = new Vector3(panel.Texture.width / (float)panel.Texture.height * panel.Scale.x, panel.Scale.y, panel.Scale.z);
                    panelObject.layer = ScreenLayer;
                    var meshFilter = panelObject.AddComponent<MeshFilter>();
                    meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Quad.fbx");
                    var material = new Material(CustomAssetManager.UiUnlitTransparentShader)
                    {
                        mainTexture = panel.Texture,
                    };
                    var meshRenderer = panelObject.AddComponent<MeshRenderer>();
                    meshRenderer.material = material;

                    PanelObjects[i] = panelObject;
                }
            }

            if (!MouseCursor)
            {
                MouseCursor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // Ensure the lifecycle of the GameObject is synchronized with its parent.
                MouseCursor.transform.parent = gameObject.transform;
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

        void OnDestroy()
        {
            PluginLog.Debug($"OnDestroy: {name}");
        }

        void Update()
        {
            if (0 <= Input.mousePosition.x && Input.mousePosition.x <= UnityEngine.Screen.width && 0 <= Input.mousePosition.y && Input.mousePosition.y <= UnityEngine.Screen.height)
            {
                // If the mouse cursor is within the screen boundaries, it's converted to UI screen coordinates for display.
                MouseCursor.SetActive(true);
                var newPosition = GetWorldPositionFromScreen(Input.mousePosition.x, Input.mousePosition.y);
                if (newPosition != null) MouseCursor.transform.position = newPosition.Value;
            }
            else
            {
                MouseCursor.SetActive(false);
            }
        }

        public Vector3? GetWorldPositionFromScreen(float x, float y)
        {
            var mainPanelObject = MainPanelObject;
            return mainPanelObject
                ? mainPanelObject.transform.TransformPoint(x / UnityEngine.Screen.width - 0.5f, y / UnityEngine.Screen.height - 0.5f, 0f)
                : null;
        }

        /// <summary>
        /// Links the UI screen in front of the specified VR camera.
        /// </summary>
        /// <param name="targetCamera">The target camera.</param>
        /// <param name="distance">Distance from VR camera.</param>
        [HideFromIl2Cpp]
        public void LinkToFront(VRCamera targetCamera, float distance)
        {
            Setup();

            // Set the UIScreen's origin to match the target VR camera's origin.
            Camera.VR.origin.SetParent(targetCamera.VR.origin);
            Camera.VR.origin.localPosition = Vector3.zero;
            Camera.VR.origin.localRotation = Quaternion.identity;

            // Position the UIScreen in front of the base head position, ensuring it does not follow the player's head movements.
            ScreenObject.transform.SetParent(targetCamera.VR.origin);
            ScreenObject.transform.localPosition = VRCamera.BaseHeadPosition + VRCamera.BaseHeadRotation * (distance * Vector3.forward);
            ScreenObject.transform.localRotation = VRCamera.BaseHeadRotation;
            // Example: Always keep the screen in front of player's head.
            // ScreenObject.transform.SetParent(targetCamera.VR.head);
            // ScreenObject.transform.localPosition = distance * Vector3.forward;
            // ScreenObject.transform.localRotation = Quaternion.identity;
        }
    }
}
