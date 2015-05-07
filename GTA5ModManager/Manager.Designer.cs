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
            this.button1 = new System.Windows.Forms.Button();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modListBox
            // 
            this.modListBox.FormattingEnabled = true;
            this.modListBox.Location = new System.Drawing.Point(473, 1);
            this.modListBox.Name = "modListBox";
            this.modListBox.Size = new System.Drawing.Size(258, 303);
            this.modListBox.TabIndex = 0;
            this.modListBox.SelectedIndexChanged += new System.EventHandler(this.modListBox_SelectedIndexChanged);
            // 
            // consoleBox
            // 
            this.consoleBox.Location = new System.Drawing.Point(473, 301);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ReadOnly = true;
            this.consoleBox.Size = new System.Drawing.Size(258, 238);
            this.consoleBox.TabIndex = 1;
            this.consoleBox.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 51);
            this.button1.TabIndex = 2;
            this.button1.Text = "Enable";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // uninstallButton
            // 
            this.uninstallButton.Location = new System.Drawing.Point(278, 85);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(169, 51);
            this.uninstallButton.TabIndex = 3;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(729, 540);
            this.Controls.Add(this.uninstallButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.modListBox);
            this.Name = "ManagerForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox modListBox;
        private System.Windows.Forms.RichTextBox consoleBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button uninstallButton;

    }
}

