@ECHO OFF
SETLOCAL

CALL variables.bat

IF EXIST "%HC_VRTrial_PACKAGE_DIR%" (
  RMDIR /s /q "%HC_VRTrial_PACKAGE_DIR%"
)

REM Plugin
MKDIR "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\HC_VRTrial.dll" "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\SteamVRLib_UnityEngine.XR.Management.dll" "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\SteamVRLib_Valve.VR.dll" "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins"
COPY "%HC_VRTrial_RELEASE_OUTPUT_DIR%\SteamVRLib_Unity.XR.OpenVR.dll" "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\openvr_api.dll" "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\XRSDKOpenVR.dll" "%HC_VRTrial_PACKAGE_DIR%\BepInEx\plugins"

REM ========== Data
REM DLL files
MKDIR "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\Plugins\x86_64"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\openvr_api.dll" "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\Plugins\x86_64"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\x64\XRSDKOpenVR.dll" "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\Plugins\x86_64"

REM SteamVR Input action files
MKDIR "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\StreamingAssets\SteamVR"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Valve.VR\StreamingAssets\SteamVR\*.json" "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\StreamingAssets\SteamVR"

REM OpenVRSettings.asset
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Valve.VR\XR\Settings\OpenVRSettings.asset" "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\StreamingAssets\SteamVR"

REM UnitySubsystemsManifest.json
MKDIR "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\UnitySubsystems\XRSDKOpenVR"
COPY "%HC_VRTrial_SOLUTION_DIR%\SteamVRLib_Unity.XR.OpenVR\UnitySubsystemsManifest.json" "%HC_VRTrial_PACKAGE_DIR%\HoneyCome_Data\UnitySubsystems\XRSDKOpenVR"

powershell Compress-Archive -Path "%HC_VRTrial_PACKAGE_DIR%\*" -DestinationPath "%HC_VRTrial_PACKAGE_DIR%\HC_VRTrial.zip"

PAUSE
