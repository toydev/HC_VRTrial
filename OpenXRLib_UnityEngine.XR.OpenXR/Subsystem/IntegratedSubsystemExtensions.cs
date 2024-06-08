using System;

using UnityEngine;

namespace UnityEngineExtensions
{
    public static class IntegratedSubsystemExtensions
    {
        public static void Destroy(IntegratedSubsystem subsystem)
        {
            var ptr = subsystem.m_Ptr;
            SubsystemManagerExtensions.RemoveIntegratedSubsystemByPtr(subsystem.m_Ptr);
            SubsystemBindings.DestroySubsystem(ptr);
            subsystem.m_Ptr = IntPtr.Zero;
        }
    }
}
