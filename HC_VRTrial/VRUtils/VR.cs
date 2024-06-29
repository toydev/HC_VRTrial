using System;
using System.Collections.Generic;

using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using UnityEngine.XR.OpenXR;

using HC_VRTrial.Logging;
using UnityEngine.XR.Management;

namespace HC_VRTrial.VRUtils
{
    public class VR : MonoBehaviour
    {
        static VR() { ClassInjector.RegisterTypeInIl2Cpp<VR>(); }

        public static bool Initialized { get; private set; } = false;

        public static void Initialize(Action actionAfterInitialization, bool force = false)
        {
            if (force || !Initialized)
            {
                Initialized = false;
                ActionAfterInitialization = actionAfterInitialization;
                new GameObject(nameof(VR)) { hideFlags = HideFlags.HideAndDontSave }.AddComponent<VR>();
            }
        }

        private static Action ActionAfterInitialization { get; set; }

        void Start()
        {
            Setup();
        }

        [HideFromIl2Cpp]
        private void Setup()
        {
            PluginLog.Info("Start Setup");

            try
            {
                try
                {
                    // Initialize the OpenVR Display and OpenVR Input submodules.
                    var xrGeneralSettings = ScriptableObject.CreateInstance<XRGeneralSettings>();
                    var xrManagerSettings = ScriptableObject.CreateInstance<XRManagerSettings>();
                    var xrLoader = ScriptableObject.CreateInstance<OpenXRLoader>();
                    PluginLog.Info(xrLoader != null ? "OK" : "NG");
                    ((List<XRLoader>)xrManagerSettings.activeLoaders).Clear();
                    ((List<XRLoader>)xrManagerSettings.activeLoaders).Add(xrLoader);
                    OpenXRSettings.Instance.renderMode = OpenXRSettings.RenderMode.MultiPass;
                    OpenXRSettings.Instance.depthSubmissionMode = OpenXRSettings.DepthSubmissionMode.None;
                    xrManagerSettings.InitializeLoaderSync();
                    if (xrManagerSettings.activeLoader != null)
                    {
                        xrManagerSettings.StartSubsystems();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    PluginLog.Error("Failed to initialize OpenXR Loader.");
                    return;
                }

                Initialized = true;
                ActionAfterInitialization?.Invoke();
            }
            finally
            {
                PluginLog.Info("Finish Setup");
                Destroy(gameObject);
            }
        }
    }
}
