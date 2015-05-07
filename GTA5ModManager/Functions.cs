using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA5ModManager
{
    class Functions
    {

        RichTextBox tempConsole = null;
        public void Log(RichTextBox consoleBox, string message)
        {
            this.tempConsole = consoleBox;
            consoleBox.AppendText(message + "\n");
            consoleBox.SelectionStart = consoleBox.Text.Length;
            consoleBox.ScrollToCaret();
        }

        public string GetGTAPath()
        {
             var path = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Rockstar Games\\GTAV", "installfoldersteam", "").ToString().Replace("GTAV", "");
             return path;
        }

        public string GetDisabledModsPath()
        {
            var modPath = GetGTAPath() + "mods\\disabled\\";
            return modPath;
        }


        public void Setup(RichTextBox consoleBox)
        {
            var path = GetGTAPath();
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
                File.Move(modPath, GetGTAPath() + fileName);
            }
            catch (Exception e)
            {

                Log(tempConsole, e.Message + "\n");
               
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

                Log(tempConsole, e.Message + "\n");

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

                Log(tempConsole, e.Message + "\n");

            }
        }
     
    }
}
