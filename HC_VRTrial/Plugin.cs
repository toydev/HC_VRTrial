using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

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
            PluginConfig.Setup(Config);

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
                            if (PluginConfig.IsLightDisabled.Value)
                            {
                                foreach (var i in UnityEngine.Object.FindObjectsOfType<Light>())
                                {
                                    if (i.shadows != LightShadows.None)
                                    {
                                        PluginLog.Info($"Disable Light shadows: {i.name}");
                                        i.shadows = LightShadows.None;
                                    }
                                    if (i.type == LightType.Spot || i.type == LightType.Point)
                                    {
                                        PluginLog.Info($"Disable Light: {i.name}");
                                        i.enabled = false;
                                    }
                                }
                            }

                            // #3: Improved the discomfort between the left and right eyes: Plants.
                            if (PluginConfig.IsLODGroupDisabled.Value)
                            {
                                foreach (var i in UnityEngine.Object.FindObjectsOfType<LODGroup>())
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
                                foreach (var i in UnityEngine.Object.FindObjectsOfType<ParticleSystem>())
                                {
                                    if (Regex.IsMatch(i.name, PluginConfig.DisabledParticleNameRegex.Value))
                                    {
                                        PluginLog.Info($"Disable ParticleSystem: {i.name}");
                                        i.gameObject.SetActive(false);
                                    }
                                }
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
