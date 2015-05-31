using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GTA5ModManager.Properties;

namespace GTA5ModManager
{
    public partial class ManagerForm : Form
    {
        private readonly Functions _functions = new Functions();
        private string _currentPath;

        public ManagerForm()
        {
            InitializeComponent();

            consoleBox.AllowDrop = true;
            consoleBox.DragEnter += Console_DragEnter;
            consoleBox.DragDrop += Console_DragDrop;

            enableButton.TabStop = false;
            enableButton.FlatStyle = FlatStyle.Flat;
            enableButton.FlatAppearance.BorderSize = 0;

            uninstallButton.TabStop = false;
            uninstallButton.FlatStyle = FlatStyle.Flat;
            uninstallButton.FlatAppearance.BorderSize = 0;

            refreshButton.TabStop = false;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;

            installButton.TabStop = false;
            installButton.FlatStyle = FlatStyle.Flat;
            installButton.FlatAppearance.BorderSize = 0;

            _functions.Setup(consoleBox);
            PopulateModList();
        }

        private void Console_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Console_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            if (files == null) throw new ArgumentNullException("files");
            foreach (var file in files)
            {
                var status = _functions.ExtractZipFile(consoleBox, file, _functions.GetGtaPath());
                if (status == true)
                {
                    RefreshModList();
                    Log(Path.GetFileName(file) + " has been installed");
                }
               
            }
        }

        private void RefreshModList()
        {
            modListBox.DataSource = null;
            modListBox.Items.Clear();
            var gtaPath = _functions.GetGtaPath();
            var disabledModsPath = _functions.GetDisabledModsPath();

            var enabledModsInfo = new DirectoryInfo(gtaPath);

            var enabledMods = enabledModsInfo.GetFiles("*.asi");


            var mods =
                enabledMods.Select(
                    enabledMod =>
                        new ModList {Path = gtaPath + enabledMod.Name, Name = enabledMod.Name.Replace(".asi", "")})
                    .ToList();

            var disabledModsInfo = new DirectoryInfo(disabledModsPath);

            var disabledMods = disabledModsInfo.GetFiles("*.asi");
            mods.AddRange(disabledMods.Select(disabledMod => new ModList
            {
                Path = disabledModsPath + disabledMod.Name,
                Name = disabledMod.Name.Replace(".asi", "")
            }));
            modListBox.DisplayMember = "Name";
            modListBox.DataSource = mods;

            var modsLoaded = enabledMods.Length + disabledMods.Length;
            var totalDisabled = disabledMods.Length;
            var totalEnabled = enabledMods.Length;
            Log("[MM] " + modsLoaded + " total mods loaded, " + totalEnabled + " mods are enabled and " + totalDisabled +
                " mods are disabled \n");
        }

        private void PopulateModList()
        {
            var gtaPath = _functions.GetGtaPath();
            var disabledModsPath = _functions.GetDisabledModsPath();

            if (string.IsNullOrEmpty(gtaPath))
            {
                Log("[MM] GTA Path Not Found: Check if GTA5 Installed properly");
                return;
            }
            Log("[MM] Scanning for mods: " + gtaPath + "\n");


            var enabledModsInfo = new DirectoryInfo(gtaPath);

            var enabledMods = enabledModsInfo.GetFiles("*.asi");


            var mods =
                enabledMods.Select(
                    enabledMod =>
                        new ModList {Path = gtaPath + enabledMod.Name, Name = enabledMod.Name.Replace(".asi", "")})
                    .ToList();
            if(!(Directory.Exists(disabledModsPath))) //fixes a bug which causes the program to crash
             {
                 Directory.CreateDirectory(disabledModsPath);
             }
            var disabledModsInfo = new DirectoryInfo(disabledModsPath);
             
             
                               

            var disabledMods = disabledModsInfo.GetFiles("*.asi");
            mods.AddRange(disabledMods.Select(disabledMod => new ModList
            {
                Path = disabledModsPath + disabledMod.Name,
                Name = disabledMod.Name.Replace(".asi", "")
            }));
            modListBox.DisplayMember = "Name";
            modListBox.DataSource = mods;

            var modsLoaded = enabledMods.Length + disabledMods.Length;
            Log("[MM] " + modsLoaded + " mods loaded \n");
            Log(File.Exists(gtaPath + "ScriptHookV.dll")
                ? "Script Hook installed."
                : "Script Hook not installed. Download at http://www.dev-c.com/gtav/scripthookv/");
        }

        private void modListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var modPath = "";
            try
            {
                var modList = modListBox.SelectedItem as ModList;
                if (modList != null) modPath = modList.Path;
            }
            catch (Exception)
            {
                return;
            }
            _currentPath = modPath;

            if (modPath.Contains("\\mods\\disabled\\"))
            {
                enableButton.Text = Resources.ManagerForm_modListBox_SelectedIndexChanged_Enable;
            }
            else
            {
                enableButton.Text = Resources.ManagerForm_modListBox_SelectedIndexChanged_Disable;
            }
        }

        private void enableButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return;
            }
            if (enableButton.Text == Resources.ManagerForm_modListBox_SelectedIndexChanged_Enable)
            {
                var mod = _currentPath;
                _functions.EnableMod(_currentPath);
                RefreshModList();
                Log(Path.GetFileName(mod) + " has been enabled");
            }
            else
            {
                var mod = _currentPath;
                _functions.DisableMod(_currentPath);
                RefreshModList();
                Log(Path.GetFileName(mod) + " has been disabled");
            }
        }

        private void Log(string message)
        {
            _functions.Log(consoleBox, message);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshModList();
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return;
            }
            var dialogResult =
                MessageBox.Show(
                    Resources
                        .ManagerForm_uninstallButton_Click_This_will_delete_the_installed_mod_from_your_computer__are_you_sure_,
                    Resources.ManagerForm_uninstallButton_Click_Hold_on_there_, MessageBoxButtons.YesNo);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    var mod = _currentPath;
                    var modName = Path.GetFileName(_currentPath);
                    _functions.DeleteMod(mod);
                    RefreshModList();
                    Log(modName + " has been deleted");
                    break;
                case DialogResult.No:
                    //do something else
                    break;
            }
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            openModFileDialog.Filter = Resources.modFilters;
            if (openModFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = openModFileDialog.FileName;
                var status = _functions.ExtractZipFile(consoleBox, path, _functions.GetGtaPath());
                if (status == true)
                {
                    RefreshModList();
                    Log(Path.GetFileName(path) + " has been installed");
                }
            }
            else
            {
                Log("Install cancelled by user.");
            }
        }
    }
}
