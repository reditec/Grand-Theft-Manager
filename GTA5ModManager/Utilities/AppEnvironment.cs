﻿using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GTA5ModManager.Utilities
{
    public class AppEnvironment
    {
        public static string DataPath
        {
            get
            {
                try
                {
                    // No version!
                    return Environment.GetEnvironmentVariable("AppData").Trim() + "\\" + Application.CompanyName + "\\" +
                           Application.ProductName;
                }
                catch
                {
                }

                try
                {
                    // Version, but chopped out
                    return Application.UserAppDataPath.Substring(0, Application.UserAppDataPath.LastIndexOf("\\"));
                }
                catch
                {
                    try
                    {
                        // App launch folder
                        return Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
                    }
                    catch
                    {
                        try
                        {
                            // Current working folder
                            return Environment.CurrentDirectory;
                        }
                        catch
                        {
                            try
                            {
                                // Desktop
                                return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                            }
                            catch
                            {
                                // Also current working folder
                                return ".";
                            }
                        }
                    }
                }
            }
        }

        public static RegistryKey RegistryKey
        {
            get
            {
                try
                {
                    return
                        Registry.CurrentUser.CreateSubKey(
                            "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName,
                            RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch
                {
                    var key = Application.UserAppDataRegistry;
                    var sKeyToUse = key.ToString().Replace("HKEY_CURRENT_USER\\", "");
                    sKeyToUse = sKeyToUse.Substring(0, sKeyToUse.LastIndexOf("\\"));
                    key = Registry.CurrentUser.OpenSubKey(sKeyToUse, true);

                    return key;
                }
            }
        }

        public static string Setting(string sKeyName)
        {
            string sVal = null;

            try
            {
                sVal = RegistryKey.GetValue(sKeyName, string.Empty).ToString();
            }
            catch
            {
            }

            return (string.IsNullOrEmpty(sVal) ? string.Empty : sVal);
        }

        public static void Setting(string sKeyName, object oKeyValue)
        {
            try
            {
                if ((oKeyValue == null) || (oKeyValue.ToString() == ""))
                {
                    RegistryKey.SetValue(sKeyName, string.Empty, RegistryValueKind.String);
                    RegistryKey.DeleteValue(sKeyName);
                }
                else
                    RegistryKey.SetValue(sKeyName, oKeyValue.ToString());

                return;
            }
            catch
            {
            }
        }

        // Code commented (but not removed) by psouza4 2015/01/02: there were no references to this method, so no need to compile it and bloat the software.
        //public static string SettingValue(string sAppKey, string sDefault)
        //{
        //    try
        //    {
        //        string s = AppEnvironment.Setting(sAppKey);
        //
        //        if (string.IsNullOrEmpty(s))
        //            return sDefault;
        //
        //        return s;
        //    }
        //    catch { }
        //
        //    return sDefault;
        //}

        public static bool SettingValue(string sAppKey, bool bDefault)
        {
            try
            {
                var s = Setting(sAppKey);

                if (string.IsNullOrEmpty(s))
                    return bDefault;

                var bRet = false;

                if (bool.TryParse(Setting(sAppKey.Trim()).Trim().ToLower(), out bRet))
                    return bRet;
            }
            catch
            {
            }

            return bDefault;
        }
    }
}