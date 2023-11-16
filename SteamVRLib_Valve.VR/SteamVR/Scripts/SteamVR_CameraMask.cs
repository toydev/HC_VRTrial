//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Masks out pixels that cannot be seen through the connected hmd.
//
//=============================================================================

using UnityEngine;

namespace Valve.VR
{
    // [ExecuteInEditMode]
    public class SteamVR_CameraMask : MonoBehaviour
    {
        static SteamVR_CameraMask()
        {
            Il2CppInterop.Runtime.Injection.ClassInjector.RegisterTypeInIl2Cpp<SteamVR_CameraMask>();
        }

        void Awake()
        {
            Debug.Log("<b>[SteamVR]</b> SteamVR_CameraMask is deprecated in Unity 5.4 - REMOVING");
            DestroyImmediate(this);
        }
    }
}