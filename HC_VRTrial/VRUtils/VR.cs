using System;
using System.Collections;

using BepInEx.Unity.IL2CPP.Utils;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using UnityEngine.XR.OpenXR;

using HC_VRTrial.Logging;

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
            this.StartCoroutine(Setup());
        }

        [HideFromIl2Cpp]
        private IEnumerator Setup()
        {
            PluginLog.Info("Start Setup");

            try
            {
                try
                {
                    // Initialize the OpenVR Display and OpenVR Input submodules.
                    var vrLoader = ScriptableObject.CreateInstance<OpenXRLoader>();
                    if (vrLoader.Initialize())
                    {
                        PluginLog.Info("OpenVRLoader.Initialize succeeded.");
                    }
                    else
                    {
                        PluginLog.Error("OpenVRLoader.Initialize failed.");
                        yield break;
                    }

                    // Start the OpenVR Display and OpenVR Input submodules.
                    if (vrLoader.Start())
                    {
                        PluginLog.Info("OpenVRLoader.Start succeeded.");
                    }
                    else
                    {
                        PluginLog.Error("OpenVRLoader.Start failed.");
                        yield break;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("SteamVR initialization error.", e);
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
