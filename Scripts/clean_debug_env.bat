@ECHO OFF
SETLOCAL

CALL variables.bat

REM ========== plugins
IF EXIST "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\HC_VRTrial.dll" (
  ECHO DELETE %HC_VRTrial_GAME_HOME%\BepInEx\plugins\HC_VRTrial.dll
  DEL "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\HC_VRTrial.dll"
)

IF EXIST "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_UnityEngine.XR.Management.dll" (
  ECHO DELETE %HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_UnityEngine.XR.Management.dll
  DEL "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_UnityEngine.XR.Management.dll"
)

IF EXIST "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Valve.VR.dll" (
  ECHO DELETE %HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Valve.VR.dll
  DEL "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Valve.VR.dll"
)

IF EXIST "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Unity.XR.OpenXR.dll" (
  ECHO DELETE %HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Unity.XR.OpenXR.dll
  DEL "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\SteamVRLib_Unity.XR.OpenXR.dll"
)

REM ========== Data
REM DLL files
IF EXIST "%HC_VRTrial_GAME_DATA_DIR%\Plugins\x86_64\UnityOpenXR.dll" (
  ECHO DELETE %HC_VRTrial_GAME_DATA_DIR%\Plugins\x86_64\UnityOpenXR.dll
  DEL "%HC_VRTrial_GAME_DATA_DIR%\Plugins\x86_64\UnityOpenXR.dll"
)

REM SteamVR Input action files
IF EXIST "%HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR\*.json" (
  ECHO DELETE %HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR\*.json
  DEL "%HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR\*.json"
)

REM OpenVRSettings.asset
IF EXIST "%HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR\OpenVRSettings.asset" (
  ECHO DELETE %HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR\OpenVRSettings.asset
  DEL "%HC_VRTrial_GAME_DATA_DIR%\StreamingAssets\SteamVR\OpenVRSettings.asset"
)

REM UnitySubsystemsManifest.json
IF EXIST "%HC_VRTrial_GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR\UnitySubsystemsManifest.json" (
  ECHO DELETE %HC_VRTrial_GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR\UnitySubsystemsManifest.json
  DEL "%HC_VRTrial_GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR\UnitySubsystemsManifest.json"
)

IF "%1" NEQ "called" PAUSE
