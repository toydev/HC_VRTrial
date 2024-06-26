using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

using HC_VRTrial.Logging;
using HC_VRTrial.VRUtils;
using UnityEngine.Events;

namespace HC_VRTrial
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            PluginLog.Setup(Log);

            // Log some information debugging purposes.
            for (var i = 0; i < 32; ++i) PluginLog.Debug($"Available layers - Layer[{i}]: {LayerMask.LayerToName(i)}");
            foreach (var i in Resources.FindObjectsOfTypeAll(Il2CppType.From(typeof(Shader)))) PluginLog.Debug($"Available shader: {i.name}");

            // Initialize VR.
            VR.Initialize(() =>
            {
                SceneManager.sceneLoaded += (UnityAction<Scene, LoadSceneMode>)OnSceneLoaded;
            });
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Detects a single mode scene and starts VR control of the scene.
            if (mode == LoadSceneMode.Single)
            {
                new GameObject($"{nameof(SimpleVRController)}{scene.name}").AddComponent<SimpleVRController>();
            }
        }
    }
}
