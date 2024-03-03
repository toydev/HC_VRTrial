@ECHO OFF
SETLOCAL

CALL variables.bat

CALL clean_debug_env.bat called

CALL :SETUP "%HC_VRTrial_GAME_HOME%\BepInEx\plugins" "%HC_VRTrial_GAME_HOME%\HoneyCome_Data"
CALL :SETUP "%HC_VRTrial_GAME_HOME%\DigitalCraft\BepInEx\plugins" "%HC_VRTrial_GAME_HOME%\DigitalCraft\DigitalCraft_Data"

PAUSE

GOTO :EOF

REM ==================== Setup
:SETUP

SET PLUGIN_DIR=%1
SET GAME_DATA_DIR=%2

REM ========== plugins
IF EXIST "%PLUGIN_DIR%" (
  MKDIR "%PLUGIN_DIR%"\HC_VRTrial
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\HC_VRTrial.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\HC_VRTrial.dll"
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\SteamVRLib_UnityEngine.XR.Management.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\SteamVRLib_UnityEngine.XR.Management.dll"
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\SteamVRLib_Valve.VR.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\SteamVRLib_Valve.VR.dll"
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\SteamVRLib_Unity.XR.OpenVR.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\SteamVRLib_Unity.XR.OpenVR.dll"
  COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\openvr_api.dll" "%PLUGIN_DIR%"\HC_VRTrial
  COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\XRSDKOpenVR.dll" "%PLUGIN_DIR%"\HC_VRTrial
)

REM ========== Data
IF EXIST "%GAME_DATA_DIR%" (
  REM DLL files
  COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\openvr_api.dll" "%GAME_DATA_DIR%\Plugins\x86_64"
  COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\XRSDKOpenVR.dll" "%GAME_DATA_DIR%\Plugins\x86_64"

  REM SteamVR Input action files
  MKDIR "%GAME_DATA_DIR%\StreamingAssets\SteamVR"
  COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Valve.VR\StreamingAssets\SteamVR\*.json" "%GAME_DATA_DIR%\StreamingAssets\SteamVR"

  REM OpenVRSettings.asset
  COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Valve.VR\XR\Settings\OpenVRSettings.asset" "%GAME_DATA_DIR%\StreamingAssets\SteamVR"

  REM UnitySubsystemsManifest.json
  MKDIR "%GAME_DATA_DIR%\UnitySubsystems\XRSDKOpenVR"
  COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\UnitySubsystemsManifest.json" "%GAME_DATA_DIR%\UnitySubsystems\XRSDKOpenVR"
)

GOTO :EOF
