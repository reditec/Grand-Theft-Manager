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
        private string currentPath;
        public ManagerForm()
        {
           
           
            InitializeComponent();

            enableButton.TabStop = false;
            enableButton.FlatStyle = FlatStyle.Flat;
            enableButton.FlatAppearance.BorderSize = 0;
            uninstallButton.TabStop = false;
            uninstallButton.FlatStyle = FlatStyle.Flat;
            uninstallButton.FlatAppearance.BorderSize = 0;
            refreshButton.TabStop = false;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;

            functions.Setup(consoleBox);
            PopulateModList();
        }



        private void RefreshModList()
        {
            modListBox.DataSource = null;
            modListBox.Items.Clear();
            var gtaPath = functions.GetGTAPath();
            var disabledModsPath = functions.GetDisabledModsPath();

            List<ModList> mods = new List<ModList>();

            DirectoryInfo enabledModsInfo = new DirectoryInfo(gtaPath);

            FileInfo[] enabledMods = enabledModsInfo.GetFiles("*.asi");


            foreach (FileInfo enabledMod in enabledMods)
            {
                mods.Add(new ModList() { Path = gtaPath + enabledMod.Name, Name = enabledMod.Name.Replace(".asi", "") });

            }

            DirectoryInfo disabledModsInfo = new DirectoryInfo(disabledModsPath);

            FileInfo[] disabledMods = disabledModsInfo.GetFiles("*.asi");
            foreach (FileInfo disabledMod in disabledMods)
            {
                mods.Add(new ModList() { Path = disabledModsPath + disabledMod.Name, Name = disabledMod.Name.Replace(".asi", "") });

            }
            modListBox.DisplayMember = "Name";
            modListBox.DataSource = mods;

            var modsLoaded = enabledMods.Length + disabledMods.Length;
            var totalDisabled = disabledMods.Length;
            var totalEnabled = enabledMods.Length;
            Log("[MM] " + modsLoaded + " total mods loaded, " + totalEnabled + " mods are enabled and " + totalDisabled + " mods are disabled \n");
        }
        private void PopulateModList()
        {
            var gtaPath = functions.GetGTAPath();
            var disabledModsPath = functions.GetDisabledModsPath();

            List<ModList> mods = new List<ModList>();

            if (String.IsNullOrEmpty(gtaPath))
            {
                Log("[MM] GTA Path Not Found: Check if GTA5 Installed properly");
                return;
            }
            Log("[MM] Scanning for mods: " + gtaPath + "\n");


            DirectoryInfo enabledModsInfo = new DirectoryInfo(gtaPath);
            
            FileInfo[] enabledMods = enabledModsInfo.GetFiles("*.asi");


            foreach (FileInfo enabledMod in enabledMods)
            {
                mods.Add(new ModList() { Path = gtaPath + enabledMod.Name, Name = enabledMod.Name.Replace(".asi", "") });
  
            }

            DirectoryInfo disabledModsInfo = new DirectoryInfo(disabledModsPath);

            FileInfo[] disabledMods = disabledModsInfo.GetFiles("*.asi");
            foreach (FileInfo disabledMod in disabledMods)
            {
                mods.Add(new ModList() { Path = disabledModsPath + disabledMod.Name, Name = disabledMod.Name.Replace(".asi", "") });
               
            }
            modListBox.DisplayMember = "Name";
            modListBox.DataSource = mods;

            var modsLoaded = enabledMods.Length + disabledMods.Length;
            Log("[MM] " + modsLoaded + " mods loaded \n");
            Log(File.Exists(gtaPath + "ScriptHookV.dll") ? "Script Hook installed." : "Script Hook not installed. Download at http://www.dev-c.com/gtav/scripthookv/");
            
        }

        private void modListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var modPath = "";
            try
            {

                 modPath = (modListBox.SelectedItem as ModList).Path;
            }
            catch (Exception)
            {

                return;
            }
            currentPath = modPath;
        
            if (modPath.Contains("\\mods\\disabled\\")) {
                enableButton.Text = "Enable";
            }
            else
            {
                enableButton.Text = "Disable";
            }
        }

        private void enableButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(currentPath))
            {
                return;
            }
            if (enableButton.Text == "Enable")
            {
                var mod = currentPath;
                functions.EnableMod(currentPath);
                RefreshModList();
                Log(Path.GetFileName(mod) + " has been enabled");

            }
            else
            {
                var mod = currentPath;
                functions.DisableMod(currentPath);
                RefreshModList();
                Log(Path.GetFileName(mod) + " has been disabled");
            }
        }

        private void Log(string message)
        {
            functions.Log(consoleBox, message);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshModList();
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(currentPath))
            {
                return;
            }
            DialogResult dialogResult = MessageBox.Show("This will delete the installed mod from your computer, are you sure?", "Hold on there!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var mod = currentPath;
                var modName = Path.GetFileName(currentPath);
                functions.DeleteMod(mod);
                RefreshModList();
                Log(modName + " has been deleted");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
      
    }
}
