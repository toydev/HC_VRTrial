[日本語](README.ja.md)

# VR Trial plugin for Honey Come
This is an experimental project for a VR plugin for Honey Come.

It is incomplete and faces many challenges, yet it offers a glimpse into the potential of Honey Come VR.

If you believe you can contribute, let's tackle these challenges together.

Then, embark on the journey to build your own customized VR plugin.

----

## Prerequisites
- Honey Come
- Latest version of [BepInEx 6.x Unity IL2CPP for Windows (x64) games](https://builds.bepinex.dev/projects/bepinex_be)
- An HMD (I'm using Meta Quest 2, but essentially any HMD should work provided that SteamVR recognizes it)
- SteamVR
- [BepisPlugins](https://github.com/IllusionMods/BepisPlugins/)
- [BepInEx.ConfigurationManager](https://github.com/BepInEx/BepInEx.ConfigurationManager)
- No other VR plugins installed

----

## How to play
To play, install [HC_VRTrial](https://github.com/toydev/HC_VRTrial/releases) into the game, then connect your HMD to SteamVR and launch the game.

The plugin will automatically activate upon detecting SteamVR's process at startup.

----

## Operations
Double-click the right mouse button to update the viewport according to the direction of the head.

For all other controls, please use the keyboard and mouse as you would in normal gameplay.

----

## Configuration
### VRExperienceOptimization section
|Key|default|description|
|----|----|----|
|DisableLights|true|If true, disables all lights.|
|DisableLODGroups|true|If true, disables all LODGroups.|
|DisableParticleSystems|true|If true, enables the conditional disabling of ParticleSystems based on the ParticleNameDisableRegex setting.|
|ParticleNameDisableRegex|(?!Star\|Heart\|ef_ne)|Regex pattern to specify which ParticleSystems should be disabled. Requires DisableParticleSystems to be true. Only ParticleSystems matching this pattern will be affected.|

### Viewport section
|Key|default|description|
|----|----|----|
|DoubleClickIntervalToUpdateViewport|0.2f|Defines the maximum interval, in seconds, that is considered for detecting a double-click to update the viewport's orientation based on HMD rotation. Set to 0 or less to disable this feature.|
|ReflectHMDRotationXOnViewport|true|Reflects the HMD's vertical orientation (X-axis rotation) on the viewport. Enable this for use while lying on your back or stomach.|
|ReflectHMDRotationYOnViewport|true|Reflects the HMD's horizontal orientation (Y-axis rotation) on the viewport. It is commonly enabled for use.|
|ReflectHMDRotationZOnViewport|true|Reflects the HMD's tilt (Z-axis rotation) on the viewport. Enable this for use while lying on your side.|

Below are examples of settings for each playstyle, with `Supine/Prone` set as the default.

|Style|ReflectHMDRotationXOnViewport<br>HMD's Vertical Orientation<br>(X-axis rotation)|ReflectHMDRotationYOnViewport<br>HMD's Horizontal Orientation<br>(Y-axis rotation)|ReflectHMDRotationZOnViewport<br>HMD's tilt<br>(Z-axis rotation)|
|----|----|----|----|
|Sitting/Standing|Disabled|Enabled|Disabled|
|Supine/Prone (Default)|Enabled|Enabled|Enabled|
|Lateral|Enabled|Enabled|Enabled|

----

## For developers

- [Developer wiki home](https://github.com/toydev/HC_VRTrial/wiki/Home) 
