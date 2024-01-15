using System;
using System.Diagnostics;
using System.Linq;

using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

using HC_VRTrial.Logging;
using HC_VRTrial.VRUtils;

namespace HC_VRTrial
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        private bool IsSteamVRRunning => Process.GetProcesses().Any(i => i.ProcessName == "vrcompositor");

        public override void Load()
        {
            PluginLog.Setup(Log);

            // Check
            for (var i = 0; i < 32; ++i) PluginLog.Info($"Layer[{i}]: {LayerMask.LayerToName(i)}");
            foreach (var i in Resources.FindObjectsOfTypeAll(Il2CppType.From(typeof(Shader)))) PluginLog.Info($"Shader: {i.name}");

            try
            {
                // Initialize VR if the SteamVR process is running.
                if (IsSteamVRRunning)
                {
                    VR.Initialize(() =>
                    {
                        SceneManager.sceneLoaded += new Action<Scene, LoadSceneMode>((scene, mode) =>
                        {
                            // Detects a single mode scene and starts VR control of the scene.
                            if (mode == LoadSceneMode.Single)
                            {
                                new GameObject($"{nameof(SimpleVRController)}_{scene.name}").AddComponent<SimpleVRController>();
                            }

                            // #2: Improved the discomfort between the left and right eyes: Shadows and lights.
                            foreach (var i in UnityEngine.Object.FindObjectsOfType<Light>())
                            {
                                if (i.shadows != LightShadows.None) i.shadows = LightShadows.None;
                                if (i.type == LightType.Spot || i.type == LightType.Point) i.enabled = false;
                            }

                            // #3: Improved the discomfort between the left and right eyes: Plants.
                            foreach (var i in GameObject.FindObjectsOfType<LODGroup>())
                            {
                                if (1 < i.lodCount)
                                {
                                    i.SetLODs(new LOD[] { i.GetLODs()[0] });
                                    i.RecalculateBounds();
                                }
                            }

                            // #4: Improved the discomfort between the left and right eyes: Pal's Room particles
                            foreach (var i in UnityEngine.Object.FindObjectsOfType<ParticleSystem>())
                            {
                                string particlesName = i.name;
                                if (particlesName == "e_hc_00_fire01_00" || particlesName == "particle_dust00")
                                    i.gameObject.SetActive(false);
                            }
                        });
                    });
                }
            }
            catch (Exception e)
            {
                PluginLog.Error(e);
            }
        }
    }
}
