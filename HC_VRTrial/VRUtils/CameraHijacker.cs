using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.Attributes;
using UnityEngine;
using UnityEngine.Rendering;

using HC_VRTrial.Logging;

namespace HC_VRTrial.VRUtils
{
    public class CameraHijacker : MonoBehaviour
    {
        static CameraHijacker() { ClassInjector.RegisterTypeInIl2Cpp<CameraHijacker>(); }

        [HideFromIl2Cpp]
        public static void Hijack(Camera source, Camera destination = null, bool useCopyFrom = true, bool synchronization = true)
        {
            if (source != null)
            {
                PluginLog.Debug(destination ? $"Hijack {source.name} to {destination?.name}" : $"Hijack {source.name}");
                if (destination && useCopyFrom) destination.CopyFrom(source);
                var hijacker = source.GetComponent<CameraHijacker>();
                if (hijacker == null) hijacker = source.gameObject.AddComponent<CameraHijacker>();
                if (destination && synchronization) hijacker.Destination = destination;
            }
        }

        private Camera Destination { get; set; }
        private LayerMask LastCullingMask { get; set; }
        private CameraClearFlags LastClearFlags { get; set; }

        void Awake()
        {
            onBeginContextRendering = (Il2CppSystem.Action<ScriptableRenderContext, Il2CppSystem.Collections.Generic.List<Camera>>)OnBeginContextRendering;
            onEndContextRendering = (Il2CppSystem.Action<ScriptableRenderContext, Il2CppSystem.Collections.Generic.List<Camera>>)OnEndContextRendering;
        }

        void OnEnable()
        {
            RenderPipelineManager.beginContextRendering += onBeginContextRendering;
            RenderPipelineManager.endContextRendering += onEndContextRendering;
        }

        /// <inheritdoc />
        void OnDisable()
        {
            RenderPipelineManager.beginContextRendering -= onBeginContextRendering;
            RenderPipelineManager.endContextRendering -= onEndContextRendering;
        }

        private Il2CppSystem.Action<ScriptableRenderContext, Il2CppSystem.Collections.Generic.List<Camera>> onBeginContextRendering;
        [HideFromIl2Cpp]
        void OnBeginContextRendering(ScriptableRenderContext context, Il2CppSystem.Collections.Generic.List<Camera> cameras)
        {
            var camera = GetComponent<Camera>();
            if (camera != null)
            {
                // Disable camera before the rendering phase begins.
                // Temporarily change cullingMask and clearFlags to minimize impact on the game.
                LastCullingMask = camera.cullingMask;
                LastClearFlags = camera.clearFlags;
                camera.cullingMask = 0;
                camera.clearFlags = CameraClearFlags.Nothing;
                if (Destination != null)
                {
                    // The displayed layer may be turned on or off, so copy it in real time.
                    Destination.cullingMask = LastCullingMask;
                    Destination.clearFlags = LastClearFlags;
                }
            }
        }

        private Il2CppSystem.Action<ScriptableRenderContext, Il2CppSystem.Collections.Generic.List<Camera>> onEndContextRendering;
        [HideFromIl2Cpp]
        void OnEndContextRendering(ScriptableRenderContext context, Il2CppSystem.Collections.Generic.List<Camera> cameras)
        {
            var camera = GetComponent<Camera>();
            if (camera != null)
            {
                // Revert after the rendering phase to minimize impact on game.
                camera.cullingMask = LastCullingMask;
                camera.clearFlags = LastClearFlags;
            }
        }
    }
}
