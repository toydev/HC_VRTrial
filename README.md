[日本語](README.ja.md)

# VR Trial plugin for Honey Come
Honey Come's VR plugin experimental project.

Although it's incomplete and has many challenges, you can feel the potential of Honey Come VR.

If you feel like you can do it, let's solve the problem together.

Then try building your own VR plugin.

----

## Prerequisites
- Honey Come
- Latest version of [BepInEx 6.x Unity IL2CPP for Windows (x64) games](https://builds.bepinex.dev/projects/bepinex_be)
- HMD (I'm using Meta Quest 2, which I believe basically works as long as SteamVR recognizes it.)
- SteamVR
- [BepisPlugins](https://github.com/IllusionMods/BepisPlugins/)
- [BepInEx.ConfigurationManager](https://github.com/BepInEx/BepInEx.ConfigurationManager)
- Other VR plugins must not be included

----

## How to play
Install [HC_VRTrial](https://github.com/toydev/HC_VRTrial/releases) into the game, connect the HMD and SteamVR, and start the game.

----

## Operations
No special operation method is implemented.

Operate using the keyboard and mouse as usual.

----

## Configuration
|Section|Key|default|description|
|----|----|----|----|
|General|IsLightDisabled|true|Enables the disabling of lights. Set to true to disable lights.|
|General|IsLODGroupDisabled|true|Enables the disabling of LODGroups. Set to true to disable LODGroups.|
|General|IsParticleSystemDisabled|true|Enables the disabling of ParticleSystems. Set to true to disable ParticleSystems.|
|General|DisabledParticleNameRegex|(?!Star\|Heart\|ef_ne)|Regex pattern for names of ParticleSystems to be disabled. ParticleSystems with names matching this pattern will be disabled.|

----

## For developers

- [Developer wiki home](https://github.com/toydev/HC_VRTrial/wiki/Home) 
