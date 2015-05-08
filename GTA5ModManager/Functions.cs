using System;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
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

        public void ExtractZipFile(string archiveFilenameIn, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                var fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);

                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile) continue; // Ignore directories


                    var entryFileName = Path.GetFileName(zipEntry.Name);
                    // to remove the folder from the entry:
                    // entryFileName = Path.GetFileName(entryFileName);

                    var buffer = new byte[4096]; // 4K is optimum
                    var zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    var fullZipToPath = Path.Combine(outFolder, entryFileName);
                    var directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    using (var streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true;
                    zf.Close();
                }
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