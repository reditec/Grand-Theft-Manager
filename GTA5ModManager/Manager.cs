using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA5ModManager
{
    public partial class ManagerForm : Form
    {
        Functions functions = new Functions();
        public ManagerForm()
        {
            InitializeComponent();
       
           
            populateModList();
        }


       
        
        private void populateModList()
        {
            var path = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Rockstar Games\\GTAV", "installfoldersteam", "").ToString().Replace("GTAV", "");
            if (String.IsNullOrEmpty(path))
            {
                functions.log(consoleBox, "[MM] GTA Path Not Found: Check if GTA5 Installed properly");
                return;
            }
            DirectoryInfo dinfo = new DirectoryInfo(path);
            functions.log(consoleBox, "[MM] GTA Path Found: " + path + "\n");
            FileInfo[] mods = dinfo.GetFiles("*.asi");
            foreach (FileInfo mod in mods)
            {
                modListBox.Items.Add(mod.Name);
            }
            
            functions.log(consoleBox, "[MM] " + mods.Length + " mods loaded \n");
            functions.log(consoleBox, File.Exists(path + "ScriptHookV.dll") ? "Script Hook installed." : "Script Hook not installed. Download at http://www.dev-c.com/gtav/scripthookv/");
        }

        private void modListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var modName = modListBox.GetItemText(modListBox.SelectedItem);
            functions.log(consoleBox, modName);
        }

      
    }
}
