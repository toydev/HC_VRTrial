using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using Il2CppInterop.Runtime;

using HC_VRTrial.Logging;
using HC_VRTrial.VRUtils;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace HC_VRTrial
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            PluginLog.Setup(Log);
            PluginConfig.Setup(Config);

            var devices = InputSystem.devices;

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
            var headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
            PluginLog.Info($"Head device: {headDevice.deviceId}, {headDevice.name}");
            var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            PluginLog.Info($"Head device: {leftHandDevice.deviceId}, {leftHandDevice.name}");
            var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            PluginLog.Info($"Head device: {rightHandDevice.deviceId}, {rightHandDevice.name}");

            InputSystem.RunInitialUpdate();
            // Detects a single mode scene and starts VR control of the scene.
            if (mode == LoadSceneMode.Single)
            {
                new GameObject($"{nameof(SimpleVRController)}{scene.name}").AddComponent<SimpleVRController>();
            }
        }
    }
}
