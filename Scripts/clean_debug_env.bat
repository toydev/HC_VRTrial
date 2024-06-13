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
IF EXIST "%GAME_DATA_DIR%\Plugins\x86_64\openxr_loader.dll" (
  ECHO DELETE %GAME_DATA_DIR%\Plugins\x86_64\openxr_loader.dll
  DEL "%GAME_DATA_DIR%\Plugins\x86_64\openxr_loader.dll"
)

IF EXIST "%GAME_DATA_DIR%\Plugins\x86_64\UnityOpenXR.dll" (
  ECHO DELETE %GAME_DATA_DIR%\Plugins\x86_64\UnityOpenXR.dll
  DEL "%GAME_DATA_DIR%\Plugins\x86_64\UnityOpenXR.dll"
)

REM *.asset
IF EXIST "%GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRLoader.asset" (
  ECHO DELETE %GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRLoader.asset
  DEL "%GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRLoader.asset"
)
IF EXIST "%GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRPackageSettings.asset" (
  ECHO DELETE %GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRPackageSettings.asset
  DEL "%GAME_DATA_DIR%\StreamingAssets\OpenXR\OpenXRPackageSettings.asset"
)

REM UnitySubsystemsManifest.json
IF EXIST "%GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR\UnitySubsystemsManifest.json" (
  ECHO DELETE %GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR\UnitySubsystemsManifest.json
  DEL "%GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR\UnitySubsystemsManifest.json"
)

GOTO :EOF
