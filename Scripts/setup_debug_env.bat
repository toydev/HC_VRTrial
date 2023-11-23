@ECHO OFF
SETLOCAL

CALL variables.bat

CALL clean_debug_env.bat called

REM ========== plugins
MKLINK "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\HC_VRTrial.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\HC_VRTrial.dll"
MKLINK "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_UnityEngine.XR.Management.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\SteamVRLib_UnityEngine.XR.Management.dll"
MKLINK "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Valve.VR.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\SteamVRLib_Valve.VR.dll"
MKLINK "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Unity.XR.OpenXR.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\SteamVRLib_Unity.XR.OpenXR.dll"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\openvr_api.dll" "%HC_VRTrial_GAME_HOME%\BepInEx\plugins"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\XRSDKOpenVR.dll" "%HC_VRTrial_GAME_HOME%\BepInEx\plugins"

REM ========== Data
REM DLL files
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\openvr_api.dll" "%HC_VRTrial_GAME_DATA_DIR%\Plugins\x86_64"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\XRSDKOpenVR.dll" "%HC_VRTrial_GAME_DATA_DIR%\Plugins\x86_64"

REM SteamVR Input action files
MKDIR "%HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Valve.VR\StreamingAssets\SteamVR\*.json" "%HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR"

REM OpenVRSettings.asset
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Valve.VR\XR\Settings\OpenVRSettings.asset" "%HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR"

REM UnitySubsystemsManifest.json
MKDIR "%HC_VRTrial_GAME_DATA_DIR%\UnitySubsystems\XRSDKOpenVR"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\UnitySubsystemsManifest.json" "%HC_VRTrial_GAME_DATA_DIR%\UnitySubsystems\XRSDKOpenVR"

PAUSE
