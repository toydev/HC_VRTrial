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
            UIScreenPanel[] panels,
            CameraClearFlags clearFlags = CameraClearFlags.Nothing
        )
        {
            var gameObject = new GameObject($"{parentGameObject.name}{name}");
            // Synchronized lifecycle
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
                // Synchronized lifecycle
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
                    // Synchronized lifecycle
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
                // Synchronized lifecycle
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

        public Vector3? GetWorldPositionFromScreen(float x, float y)
        {
            var mainPanelObject = MainPanelObject;
            return mainPanelObject
                ? mainPanelObject.transform.TransformPoint(x / UnityEngine.Screen.width - 0.5f, y / UnityEngine.Screen.height - 0.5f, 0f)
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
