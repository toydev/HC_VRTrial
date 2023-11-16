using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR
{
    public class SteamVR_TrackingReferenceManager : MonoBehaviour
    {
        static SteamVR_TrackingReferenceManager()
        {
            Il2CppInterop.Runtime.Injection.ClassInjector.RegisterTypeInIl2Cpp<SteamVR_TrackingReferenceManager>();
        }

        private Dictionary<uint, TrackingReferenceObject> trackingReferences = new Dictionary<uint, TrackingReferenceObject>();

        private void OnEnable()
        {
            SteamVR_Events.NewPoses.Listen(OnNewPoses);
        }

        private void OnDisable()
        {
            SteamVR_Events.NewPoses.Remove(OnNewPoses);
        }

        [Il2CppInterop.Runtime.Attributes.HideFromIl2Cpp]
        private void OnNewPoses(TrackedDevicePose_t[] poses)
        {
            if (poses == null)
                return;

            for (uint deviceIndex = 0; deviceIndex < poses.Length; deviceIndex++)
            {
                if (trackingReferences.ContainsKey(deviceIndex) == false)
                {
                    ETrackedDeviceClass deviceClass = OpenVR.System.GetTrackedDeviceClass(deviceIndex);

                    if (deviceClass == ETrackedDeviceClass.TrackingReference)
                    {
                        TrackingReferenceObject trackingReference = new TrackingReferenceObject();
                        trackingReference.trackedDeviceClass = deviceClass;
                        trackingReference.gameObject = new GameObject("Tracking Reference " + deviceIndex.ToString());
                        trackingReference.gameObject.transform.parent = this.transform;
                        trackingReference.trackedObject = trackingReference.gameObject.AddComponent<SteamVR_TrackedObject>();
                        trackingReference.renderModel = trackingReference.gameObject.AddComponent<SteamVR_RenderModel>();
                        trackingReference.renderModel.createComponents = false;
                        trackingReference.renderModel.updateDynamically = false;

                        trackingReferences.Add(deviceIndex, trackingReference);

                        trackingReference.gameObject.SendMessage("SetDeviceIndex", (int)deviceIndex, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        trackingReferences.Add(deviceIndex, new TrackingReferenceObject() { trackedDeviceClass = deviceClass });
                    }
                }
            }
        }

        private class TrackingReferenceObject
        {
            public ETrackedDeviceClass trackedDeviceClass;
            public GameObject gameObject;
            public SteamVR_RenderModel renderModel;
            public SteamVR_TrackedObject trackedObject;
        }
    }
}