@ECHO OFF
SETLOCAL

CALL variables.bat

CALL :CLEAN "%HC_VRTrial_GAME_HOME%\BepInEx\plugins\HC_VRTrial" "%HC_VRTrial_GAME_HOME%\HoneyCome_Data"
CALL :CLEAN "%HC_VRTrial_GAME_HOME%\DigitalCraft\BepInEx\plugins\HC_VRTrial" "%HC_VRTrial_GAME_HOME%\DigitalCraft\DigitalCraft_Data"

IF "%1" NEQ "called" PAUSE

GOTO :EOF

REM ==================== Clean
:CLEAN

SET PLUGIN_DIR=%1
SET GAME_DATA_DIR=%2

REM ========== plugins
IF EXIST "%PLUGIN_DIR%" (
  ECHO DELETE %PLUGIN_DIR%
  RMDIR /s /q "%PLUGIN_DIR%"
)

REM ========== Data
REM DLL files
IF EXIST "%GAME_DATA_DIR%\Plugins\x86_64\openvr_api.dll" (
  ECHO DELETE %GAME_DATA_DIR%\Plugins\x86_64\openvr_api.dll
  DEL "%GAME_DATA_DIR%\Plugins\x86_64\openvr_api.dll"
)

IF EXIST "%GAME_DATA_DIR%\Plugins\x86_64\XRSDKOpenVR.dll" (
  ECHO DELETE %GAME_DATA_DIR%\Plugins\x86_64\XRSDKOpenVR.dll
  DEL "%GAME_DATA_DIR%\Plugins\x86_64\XRSDKOpenVR.dll"
)

REM SteamVR Input action files
IF EXIST "%GAME_DATA_DIR%\StreamingAssets\SteamVR\*.json" (
  ECHO DELETE %GAME_DATA_DIR%\StreamingAssets\SteamVR\*.json
  DEL "%GAME_DATA_DIR%\StreamingAssets\SteamVR\*.json"
)

REM OpenVRSettings.asset
IF EXIST "%GAME_DATA_DIR%\StreamingAssets\SteamVR\OpenVRSettings.asset" (
  ECHO DELETE %GAME_DATA_DIR%\StreamingAssets\SteamVR\OpenVRSettings.asset
  DEL "%GAME_DATA_DIR%\StreamingAssets\SteamVR\OpenVRSettings.asset"
)

REM UnitySubsystemsManifest.json
IF EXIST "%GAME_DATA_DIR%\UnitySubsystems\XRSDKOpenVR\UnitySubsystemsManifest.json" (
  ECHO DELETE %GAME_DATA_DIR%\UnitySubsystems\XRSDKOpenVR\UnitySubsystemsManifest.json
  DEL "%GAME_DATA_DIR%\UnitySubsystems\XRSDKOpenVR\UnitySubsystemsManifest.json"
)

GOTO :EOF
