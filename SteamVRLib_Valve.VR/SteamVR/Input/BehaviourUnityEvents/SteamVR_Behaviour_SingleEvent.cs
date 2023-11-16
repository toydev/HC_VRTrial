//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System;

namespace Valve.VR
{
    [Serializable]
    public class SteamVR_Behaviour_SingleEvent : SteamVR_Events.Event<SteamVR_Behaviour_Single, SteamVR_Input_Sources, float, float> { }
}