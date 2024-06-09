@ECHO OFF
SETLOCAL

CALL variables.bat

IF EXIST "%HC_VRTrial_PACKAGE_DIR%" (
  RMDIR /s /q "%HC_VRTrial_PACKAGE_DIR%"
)

CALL :PACKAGE "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins\HC_VRTrial" "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data"
CALL :PACKAGE "%HC_VRTrial_PACKAGE_DIR%\DigitalCraft\BepInEx\plugins\HC_VRTrial" "%HC_VRTrial_PACKAGE_DIR%\DigitalCraft\DigitalCraft_Data"

SET RELEASE_FILE_PATH="%HC_VRTrial_PACKAGE_DIR%\..\HC_VRTrial.zip"

IF EXIST "%RELEASE_FILE_PATH%" (
  DEL "%RELEASE_FILE_PATH%"
)
powershell Compress-Archive -Path "%HC_VRTrial_PACKAGE_DIR%\*" -DestinationPath "%RELEASE_FILE_PATH%"

GOTO :EOF

PAUSE

REM ==================== Package
:PACKAGE

SET PLUGIN_DIR=%1
SET GAME_DATA_DIR=%2

REM ========== plugins
MKDIR "%PLUGIN_DIR%"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\HC_VRTrial.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\OpenXRLib_UnityEngine.InputSystem.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\OpenXRLib_UnityEngine.XR.Management.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\OpenXRLib_UnityEngine.XR.OpenXR.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\openxr_loader.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\UnityOpenXR.dll" "%PLUGIN_DIR%"

REM ========== Data
REM DLL files
MKDIR "%GAME_DATA_DIR%\Plugins\x86_64"
COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\openxr_loader.dll" "%GAME_DATA_DIR%\Plugins\x86_64"
COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\x64\UnityOpenXR.dll" "%GAME_DATA_DIR%\Plugins\x86_64"

REM UnitySubsystemsManifest.json
MKDIR "%GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR"
COPY "%HC_VRTrial_SOLUTION_DIR%\OpenXRLib_UnityEngine.XR.OpenXR\UnitySubsystemsManifest.json" "%GAME_DATA_DIR%\UnitySubsystems\UnityOpenXR"

GOTO :EOF
