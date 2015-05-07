using System.Windows.Forms;
namespace GTA5ModManager
{
    partial class ManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerForm));
            this.modListBox = new System.Windows.Forms.ListBox();
            this.consoleBox = new System.Windows.Forms.RichTextBox();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.enableButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modListBox
            // 
            this.modListBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.modListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.modListBox.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modListBox.ForeColor = System.Drawing.Color.DimGray;
            this.modListBox.FormattingEnabled = true;
            this.modListBox.ItemHeight = 20;
            this.modListBox.Location = new System.Drawing.Point(3, 1);
            this.modListBox.Name = "modListBox";
            this.modListBox.Size = new System.Drawing.Size(244, 304);
            this.modListBox.TabIndex = 0;
            this.modListBox.SelectedIndexChanged += new System.EventHandler(this.modListBox_SelectedIndexChanged);
            // 
            // consoleBox
            // 
            this.consoleBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.consoleBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.consoleBox.Font = new System.Drawing.Font("Open Sans", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleBox.ForeColor = System.Drawing.Color.DimGray;
            this.consoleBox.Location = new System.Drawing.Point(276, 237);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ReadOnly = true;
            this.consoleBox.Size = new System.Drawing.Size(461, 132);
            this.consoleBox.TabIndex = 1;
            this.consoleBox.Text = "";
            // 
            // uninstallButton
            // 
            this.uninstallButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.uninstallButton.FlatAppearance.BorderSize = 0;
            this.uninstallButton.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uninstallButton.ForeColor = System.Drawing.SystemColors.Control;
            this.uninstallButton.Location = new System.Drawing.Point(139, 311);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(108, 51);
            this.uninstallButton.TabIndex = 2;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = false;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // enableButton
            // 
            this.enableButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.enableButton.FlatAppearance.BorderSize = 0;
            this.enableButton.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enableButton.ForeColor = System.Drawing.SystemColors.Control;
            this.enableButton.Location = new System.Drawing.Point(3, 311);
            this.enableButton.Name = "enableButton";
            this.enableButton.Size = new System.Drawing.Size(108, 51);
            this.enableButton.TabIndex = 3;
            this.enableButton.Text = "Enable";
            this.enableButton.UseVisualStyleBackColor = false;
            this.enableButton.Click += new System.EventHandler(this.enableButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.refreshButton.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.ForeColor = System.Drawing.SystemColors.Control;
            this.refreshButton.Location = new System.Drawing.Point(276, 208);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(111, 23);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.Text = "Refresh Mods";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(736, 368);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.enableButton);
            this.Controls.Add(this.uninstallButton);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.modListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagerForm";
            this.Text = "Grand Theft Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox modListBox;
        private System.Windows.Forms.RichTextBox consoleBox;
        private System.Windows.Forms.Button uninstallButton;
        private Button enableButton;
        private Button refreshButton;

    }
}

