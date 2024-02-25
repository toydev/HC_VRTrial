using BepInEx.Configuration;

namespace HC_VRTrial
{
    public class PluginConfig
    {
        public static ConfigEntry<bool> IsLightDisabled { get; private set; }
        public static ConfigEntry<bool> IsLODGroupDisabled { get; private set; }
        public static ConfigEntry<bool> IsParticleSystemDisabled { get; private set; }
        public static ConfigEntry<string> DisabledParticleNameRegex { get; private set; }

        public static void Setup(ConfigFile config)
        {
            IsLightDisabled = config.Bind(
                "General", nameof(IsLightDisabled), true,
                "Enables the disabling of lights. Set to true to disable lights.");

            IsLODGroupDisabled = config.Bind(
                "General", nameof(IsLODGroupDisabled), true,
                "Enables the disabling of LODGroups. Set to true to disable LODGroups.");

            IsParticleSystemDisabled = config.Bind(
                "General", nameof(IsParticleSystemDisabled), true,
                "Enables the disabling of ParticleSystems. Set to true to disable ParticleSystems.");

            DisabledParticleNameRegex = config.Bind(
                "General", nameof(DisabledParticleNameRegex), "^(?!Star|Heart|ef_ne)",
                "Regex pattern for names of ParticleSystems to be disabled. ParticleSystems with names matching this pattern will be disabled.");
        }
    }
}
