using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.XR.Management
{
    /// <summary>
    /// XR Loader abstract subclass used as a base class for specific provider implementations. Class provides some
    /// helper logic that can be used to handle subsystem handling in a typesafe manner, reducing potential boilerplate
    /// code.
    /// </summary>
    public abstract class XRLoaderHelper : XRLoader
    {
        /// <summary>
        /// Map of loaded susbsystems. Used so we don't always have to fo to XRSubsystemManger and do a manual
        /// search to find the instance we loaded.
        /// </summary>
        protected Dictionary<string, IntegratedSubsystem> m_IntegratedSubsystemInstanceMap = new Dictionary<string, IntegratedSubsystem>();

        /// <summary>
        /// Gets the loaded subsystem of the specified type. Implementation dependent as only implemetnations
        /// know what they have loaded and how best to get it..
        /// </summary>
        ///
        /// <typeparam name="T">Type of the subsystem to get.</typeparam>
        ///
        /// <returns>The loaded subsystem or null if not found.</returns>
        [Il2CppInterop.Runtime.Attributes.HideFromIl2Cpp]
        public override IntegratedSubsystem GetLoadedIntegratedSubsystem(string id)
        {
            IntegratedSubsystem subsystem;
            m_IntegratedSubsystemInstanceMap.TryGetValue(id, out subsystem);
            return subsystem;
        }

        /// <summary>
        /// Start a subsystem instance of a given type. Subsystem assumed to already be loaded from
        /// a previous call to CreateSubsystem
        /// </summary>
        ///
        /// <typeparam name="T">A subclass of <see cref="ISubsystem"/></typeparam>
        protected void StartIntegratedSubsystem(string id)
        {
            var subsystem = GetLoadedIntegratedSubsystem(id);
            if (subsystem != null)
                subsystem.Start();
        }

        /// <summary>
        /// Stop a subsystem instance of a given type. Subsystem assumed to already be loaded from
        /// a previous call to CreateSubsystem
        /// </summary>
        ///
        /// <typeparam name="T">A subclass of <see cref="ISubsystem"/></typeparam>
        protected void StopIntegratedSubsystem(string id)
        {
            var subsystem = GetLoadedIntegratedSubsystem(id);
            if (subsystem != null)
                subsystem.Stop();
        }

        /// <summary>
        /// Destroy a subsystem instance of a given type. Subsystem assumed to already be loaded from
        /// a previous call to CreateSubsystem
        /// </summary>
        ///
        /// <typeparam name="T">A subclass of <see cref="ISubsystem"/></typeparam>
        protected void DestroyIntegratedSubsystem(string id)
        {
            var subsystem = GetLoadedIntegratedSubsystem(id);
            if (subsystem != null)
            {
                if (m_IntegratedSubsystemInstanceMap.ContainsKey(id))
                    m_IntegratedSubsystemInstanceMap.Remove(id);
                UnityEngineExtensions.IntegratedSubsystemExtensions.Destroy(subsystem);
            }
        }

        /// <summary>
        /// Creates a subsystem given a list of descriptors and a specific subsystem id.
        ///
        /// You should make sure to destroy any subsystem that you created so that resources
        /// acquired by your subsystems are correctly cleaned up and released. This is especially important
        /// if you create them during initialization, but initialization fails. If that happens,
        /// you should clean up any subsystems created up to that point.
        /// </summary>
        ///
        /// <typeparam name="TDescriptor">The descriptor type being passed in.</typeparam>
        /// <typeparam name="TSubsystem">The subsystem type being requested</typeparam>
        /// <param name="descriptors">List of TDescriptor instances to use for subsystem matching.</param>
        /// <param name="id">The identifier key of the particualr subsystem implementation being requested.</param>
        [Il2CppInterop.Runtime.Attributes.HideFromIl2Cpp]
        protected void CreateIntegratedSubsystem(List<IntegratedSubsystemDescriptor> descriptors, string id)
        {
            if (descriptors == null)
                throw new ArgumentNullException("descriptors");

            UnityEngineExtensions.SubsystemManagerExtensions.GetIntegratedSubsystemDescriptors(descriptors);

            if (descriptors.Count > 0)
            {
                foreach (var descriptor in descriptors)
                {
                    IntegratedSubsystem subsys = null;
                    if (String.Compare(descriptor.id, id, true) == 0)
                    {
                        subsys = UnityEngineExtensions.IntegratedSubsystemDescriptorExtensions.Create(descriptor);
                    }
                    if (subsys != null)
                    {
                        m_IntegratedSubsystemInstanceMap[id] = subsys;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Override of <see cref="XRLoader.Deinitialize"/> to provide for clearing the instance map.true
        ///
        /// If you override this method in your subclass, you must call the base
        /// implementation to allow the instance map tp be cleaned up correctly.
        /// </summary>
        ///
        /// <returns>True if de-initialization was successful.</returns>
        public override bool Deinitialize()
        {
            m_IntegratedSubsystemInstanceMap.Clear();
            return base.Deinitialize();
        }

#if UNITY_EDITOR
        virtual public void WasAssignedToBuildTarget(BuildTargetGroup buildTargetGroup)
        {

        }

        virtual public void WasUnassignedFromBuildTarget(BuildTargetGroup buildTargetGroup)
        {

        }
#endif
    }
}
