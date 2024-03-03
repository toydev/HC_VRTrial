using System.Collections;
using System.Text.RegularExpressions;

using BepInEx.Unity.IL2CPP.Utils;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

using HC_VRTrial.Logging;
using HC_VRTrial.VRUtils;

namespace HC_VRTrial
{
    public class SimpleVRController : MonoBehaviour
    {
        static SimpleVRController() { ClassInjector.RegisterTypeInIl2Cpp<SimpleVRController>(); }

        // Layer: Assign layers not used by the game.
        // for UGUI capture work
        public const int UGUI_CAPTURE_LAYER = 15;
        // for UI screen camera
        public const int UI_SCREEN_LAYER = 31;

        // Camera depth: Set the output after the game camera.
        public const int MAIN_VR_CAMERA_DEPTH = 1000;
        public const int UI_SCREEN_CAMERA_DEPTH = MAIN_VR_CAMERA_DEPTH + 10;

        // Distance from player to UI screen, in meters.
        public const float UI_SCREEN_DISTANCE = 1.0f;

        void Start()
        {
            // mainVRCamera is a camera that displays 3D space.
            var mainVRCamera = VRCamera.Create(gameObject, $"{gameObject.name}VRCamera", MAIN_VR_CAMERA_DEPTH);

            // uguiCapture captures 2D UI for display in 3D space.
            var uguiCapture = UGUICapture.Create(gameObject, $"{gameObject.name}UGUICapture", UGUI_CAPTURE_LAYER);

            // uiScreen displays the texture as a screen using the built-in VRCamera.
            // Also projects the mouse cursor onto the 3D screen.
            var uiScreen = UIScreen.Create(gameObject, nameof(UIScreen), UI_SCREEN_CAMERA_DEPTH, UI_SCREEN_LAYER, uguiCapture.Texture);

            this.StartCoroutine(Hijack(mainVRCamera, uiScreen));
        }

        [HideFromIl2Cpp]
        IEnumerator Hijack(VRCamera mainVRCamera, UIScreen uiScreen)
        {
            // Wait a moment for HMD tracking.
            yield return new WaitForSeconds(0.1f);

            // For the first time, Fix the position of the player's base head.
            if (!VRCamera.HasBaseHead) VRCamera.UpdateBaseHead(mainVRCamera);

            // Hijack the view of Camera.main and sync it to mainVRCamera.
            mainVRCamera.Hijack(Camera.main);

            // Put the UI screen in front of mainVRCamera.
            uiScreen.LinkToFront(mainVRCamera, UI_SCREEN_DISTANCE);
        }

        void Update()
        {
            OptimizeVRExperience();
        }

        void OptimizeVRExperience()
        {
            // #2: Improved the discomfort between the left and right eyes: Shadows and lights.
            if (PluginConfig.IsLightDisabled.Value)
            {
                foreach (var i in FindObjectsOfType<Light>())
                {
                    if (i.shadows != LightShadows.None)
                    {
                        PluginLog.Info($"Disable Light shadows: {i.name}");
                        i.shadows = LightShadows.None;
                    }
                    if (i.enabled && (i.type == LightType.Spot || i.type == LightType.Point))
                    {
                        PluginLog.Info($"Disable Light: {i.name}");
                        i.enabled = false;
                    }
                }
            }

            // #3: Improved the discomfort between the left and right eyes: Plants.
            if (PluginConfig.IsLODGroupDisabled.Value)
            {
                foreach (var i in FindObjectsOfType<LODGroup>())
                {
                    if (1 < i.lodCount)
                    {
                        PluginLog.Info($"Disable LODGroup: {i.name}");
                        i.SetLODs(new LOD[] { i.GetLODs()[0] });
                        i.RecalculateBounds();
                    }
                }
            }

            // #10: Improved the discomfort between the left and right eyes: Particles.
            if (PluginConfig.IsParticleSystemDisabled.Value)
            {
                foreach (var i in FindObjectsOfType<ParticleSystem>())
                {
                    if (Regex.IsMatch(i.name, PluginConfig.DisabledParticleNameRegex.Value))
                    {
                        PluginLog.Info($"Disable ParticleSystem: {i.name}");
                        i.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
