namespace UnityEngineExtensions
{
    public static class SubsystemManagerExtensions
    {
        public static void GetIntegratedSubsystemDescriptors(System.Collections.Generic.List<UnityEngine.IntegratedSubsystemDescriptor> descriptors)
        {
            descriptors.Clear();
            foreach (var i in UnityEngine.SubsystemsImplementation.SubsystemDescriptorStore.s_IntegratedDescriptors) descriptors.Add(i);
        }


        public static void RemoveIntegratedSubsystemByPtr(System.IntPtr ptr)
        {

            for (int i = 0; i < UnityEngine.SubsystemManager.s_IntegratedSubsystems.Count; i++)
            {
                if (!(UnityEngine.SubsystemManager.s_IntegratedSubsystems[i].m_Ptr != ptr))
                {
                    UnityEngine.SubsystemManager.s_IntegratedSubsystems[i].m_Ptr = System.IntPtr.Zero;
                    UnityEngine.SubsystemManager.s_IntegratedSubsystems.RemoveAt(i);
                    break;
                }
            }
        }

    }
}
