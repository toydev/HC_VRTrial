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
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\OpenXRLib_Common.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\OpenXRLib_Common.dll"
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\OpenXRLib_Unity.XR.CoreUtils.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\OpenXRLib_Unity.XR.CoreUtils.dll"
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\OpenXRLib_UnityEngine.InputSystem.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\OpenXRLib_UnityEngine.InputSystem.dll"
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\OpenXRLib_UnityEngine.XR.Management.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\OpenXRLib_UnityEngine.XR.Management.dll"
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\OpenXRLib_UnityEngine.XR.OpenXR.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\OpenXRLib_UnityEngine.XR.OpenXR.dll"
  COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\openxr_loader.dll" "%PLUGIN_DIR%"\HC_VRTrial
  COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\UnityOpenXR.dll" "%PLUGIN_DIR%"\HC_VRTrial
  MKLINK "%PLUGIN_DIR%\HC_VRTrial\OpenXRLib_UnityEngine.XR.Inteaction.Toolkit.dll" "%HC_VRTrial_DEBUG_OUTPUT_DIR%\OpenXRLib_UnityEngine.XR.Inteaction.Toolkit.dll"
  COPY "%HC_VRTrial_DEBUG_OUTPUT_DIR%\Newtonsoft.Json.dll" "%PLUGIN_DIR%"\HC_VRTrial
)

REM ========== Data
IF EXIST "%GAME_DATA_DIR%" (
  REM DLL files
  COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\openxr_loader.dll" "%GAME_DATA_DIR%\Plugins\x86_64"
  COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\UnityOpenXR.dll" "%GAME_DATA_DIR%\Plugins\x86_64"

  REM *.asset
  MKDIR "%GAME_DATA_DIR%\StreamingAssets\OpenXR"
  COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\Assets\XR\Loaders\Open XR Loader.asset" "%GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRLoader.asset"
  COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\Assets\XR\Settings\OpenXR Package Settings.asset" "%GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRPackageSettings.asset"

  REM UnitySubsystemsManifest.json
  MKDIR "%GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR"
  COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\UnitySubsystemsManifest.json" "%GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR"
)

GOTO :EOF
