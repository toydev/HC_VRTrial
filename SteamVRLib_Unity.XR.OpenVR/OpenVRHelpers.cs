using System.Reflection;

namespace Unity.XR.OpenVR
{
    public class OpenVRHelpers
    {
        public static bool IsUsingSteamVRInput()
        {
            return DoesTypeExist("SteamVR_Input");
        }

        public static bool DoesTypeExist(string className, bool fullname = false)
        {
            return GetType(className, fullname) != null;
        }

        public static Type GetType(string className, bool fullname = false)
        {
            if (fullname)
            {
                try
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        try
                        {
                            foreach (var type in assembly.GetTypes())
                            {
                                if (type.FullName == className) return type;
                            }
                        }
                        catch (Exception)
                        {
                            // ignore
                        }
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
                return null;
            }
            else
            {
                try
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        try
                        {
                            foreach (var type in assembly.GetTypes())
                            {
                                if (type.Name == className) return type;
                            }
                        }
                        catch (Exception)
                        {
                            // ignore
                        }
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
                return null;
            }
        }

        public static string GetActionManifestPathFromPlugin()
        {
            Type steamvrInputType = GetType("SteamVR_Input");
            MethodInfo getPathMethod = steamvrInputType.GetMethod("GetActionsFilePath");
            object path = getPathMethod.Invoke(null, new object[] { false });

            return (string)path;
        }

        public static string GetActionManifestNameFromPlugin()
        {
            Type steamvrInputType = GetType("SteamVR_Input");
            MethodInfo getPathMethod = steamvrInputType.GetMethod("GetActionsFileName");
            object path = getPathMethod.Invoke(null, null);

            return (string)path;
        }

        public static string GetEditorAppKeyFromPlugin()
        {
            Type steamvrInputType = GetType("SteamVR_Input");
            MethodInfo getPathMethod = steamvrInputType.GetMethod("GetEditorAppKey");
            object path = getPathMethod.Invoke(null, null);

            return (string)path;
        }
    }
}