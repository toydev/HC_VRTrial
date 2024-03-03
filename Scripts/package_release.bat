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
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\SteamVRLib_UnityEngine.XR.Management.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\SteamVRLib_Valve.VR.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\SteamVRLib_Unity.XR.OpenVR.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\openvr_api.dll" "%PLUGIN_DIR%"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\XRSDKOpenVR.dll" "%PLUGIN_DIR%"

REM ========== Data
REM DLL files
MKDIR "%GAME_DATA_DIR%\Plugins\x86_64"
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

GOTO :EOF
