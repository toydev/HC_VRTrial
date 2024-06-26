using BepInEx.Configuration;

namespace HC_VRTrial
{
    public class PluginConfig
    {
        public static ConfigEntry<bool> DisableLights { get; private set; }
        public static ConfigEntry<bool> DisableLODGroups { get; private set; }
        public static ConfigEntry<bool> DisableParticleSystems { get; private set; }
        public static ConfigEntry<string> ParticleNameDisableRegex { get; private set; }

        public static ConfigEntry<float> DoubleClickIntervalToUpdateViewport { get; private set; }
        public static ConfigEntry<bool> ReflectHMDRotationXOnViewport { get; private set; }
        public static ConfigEntry<bool> ReflectHMDRotationYOnViewport { get; private set; }
        public static ConfigEntry<bool> ReflectHMDRotationZOnViewport { get; private set; }

        public static void Setup(ConfigFile config)
        {
            DisableLights = config.Bind("VRExperienceOptimization", nameof(DisableLights), true,
                "If true, disables all lights.");
            DisableLODGroups = config.Bind("VRExperienceOptimization", nameof(DisableLODGroups), true,
                "If true, disables all LODGroups.");
            DisableParticleSystems = config.Bind("VRExperienceOptimization", nameof(DisableParticleSystems), true,
                "If true, enables the conditional disabling of ParticleSystems based on the ParticleNameDisableRegex setting.");
            ParticleNameDisableRegex = config.Bind("VRExperienceOptimization", nameof(ParticleNameDisableRegex), "^(?!Star|Heart|ef_ne)",
                "Regex pattern to specify which ParticleSystems should be disabled. Requires DisableParticleSystems to be true. Only ParticleSystems matching this pattern will be affected.");

            DoubleClickIntervalToUpdateViewport = config.Bind("Viewport", nameof(DoubleClickIntervalToUpdateViewport), 0.2f,
                "Defines the maximum interval, in seconds, that is considered for detecting a double-click to update the viewport's orientation based on HMD rotation. Set to 0 or less to disable this feature.");
            ReflectHMDRotationXOnViewport = config.Bind("Viewport", nameof(ReflectHMDRotationXOnViewport), true,
                "Reflects the HMD's vertical orientation (X-axis rotation) on the viewport. Enable this for use while lying on your back or stomach.");
            ReflectHMDRotationYOnViewport = config.Bind("Viewport", nameof(ReflectHMDRotationYOnViewport), true,
                "Reflects the HMD's horizontal orientation (Y-axis rotation) on the viewport. It is commonly enabled for use.");
            ReflectHMDRotationZOnViewport = config.Bind("Viewport", nameof(ReflectHMDRotationZOnViewport), true,
                "Reflects the HMD's tilt (Z-axis rotation) on the viewport. Enable this for use while lying on your side.");
        }
    }
}
