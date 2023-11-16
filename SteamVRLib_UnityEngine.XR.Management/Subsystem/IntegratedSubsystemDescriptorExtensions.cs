using UnityEngine;

namespace UnityEngineExtensions
{
    public static class IntegratedSubsystemDescriptorExtensions
    {
        public static IntegratedSubsystem Create(IntegratedSubsystemDescriptor integratedSubsystem)
        {
            var ptr = SubsystemDescriptorBindings.Create(integratedSubsystem.m_Ptr);
            var val = SubsystemManager.GetIntegratedSubsystemByPtr(ptr);
            if (val != null)
            {
                val.m_SubsystemDescriptor = integratedSubsystem.Cast<UnityEngine.ISubsystemDescriptor>();
            }
            return val;
        }
    }
}
