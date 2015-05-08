using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GTA5ModManager
{
    internal class Functions
    {
        private RichTextBox _tempConsole;

        public void Log(RichTextBox consoleBox, string message)
        {
            _tempConsole = consoleBox;
            consoleBox.AppendText(message + "\n");
            consoleBox.SelectionStart = consoleBox.Text.Length;
            consoleBox.ScrollToCaret();
        }

        public string GetGtaPath()
        {
            var path =
                Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Rockstar Games\\GTAV",
                    "installfoldersteam", "").ToString().Replace("GTAV", "");
            return path;
        }

        public string GetDisabledModsPath()
        {
            var modPath = GetGtaPath() + "mods\\disabled\\";
            return modPath;
        }

        public void Setup(RichTextBox consoleBox)
        {
            var path = GetGtaPath();
            if (!Directory.Exists(path + "mods/"))
            {
                Log(consoleBox, "[MM] Mod folder does not exist; creating. \n");
                Directory.CreateDirectory(path + "mods/");
                Directory.CreateDirectory(path + "mods/disabled");
            }
        }

        public void EnableMod(string modPath)
        {
            try
            {
                var fileName = Path.GetFileName(modPath);
                File.Move(modPath, GetGtaPath() + fileName);
            }
            catch (Exception e)
            {
                Log(_tempConsole, e.Message + "\n");
            }
        }

        public void DeleteMod(string modPath)
        {
            try
            {
                File.Delete(modPath);
            }
            catch (Exception e)
            {
                Log(_tempConsole, e.Message + "\n");
            }
        }

        public void DisableMod(string modPath)
        {
            try
            {
                var fileName = Path.GetFileName(modPath);
                File.Move(modPath, GetDisabledModsPath() + fileName);
            }
            catch (Exception e)
            {
                Log(_tempConsole, e.Message + "\n");
            }
        }
    }
}