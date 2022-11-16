namespace Animlist_Transition_Tool
{
    partial class AnimlistTransitionTool
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimlistTransitionTool));
            this.FileViewBox = new System.Windows.Forms.ListBox();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fNISToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LaunchButton = new System.Windows.Forms.Button();
            this.ModPrefixInput = new System.Windows.Forms.RichTextBox();
            this.ModNameInput = new System.Windows.Forms.RichTextBox();
            this.ModAuthorInput = new System.Windows.Forms.RichTextBox();
            this.ModLinkInput = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FileProgressBar = new System.Windows.Forms.ProgressBar();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileViewBox
            // 
            this.FileViewBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FileViewBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.FileViewBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileViewBox.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileViewBox.ForeColor = System.Drawing.SystemColors.Control;
            this.FileViewBox.FormattingEnabled = true;
            this.FileViewBox.ItemHeight = 19;
            this.FileViewBox.Location = new System.Drawing.Point(12, 38);
            this.FileViewBox.Name = "FileViewBox";
            this.FileViewBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FileViewBox.Size = new System.Drawing.Size(1018, 988);
            this.FileViewBox.TabIndex = 1;
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.MenuStrip.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MenuStrip.Size = new System.Drawing.Size(1904, 27);
            this.MenuStrip.TabIndex = 2;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.outputToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animlistToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // animlistToolStripMenuItem
            // 
            this.animlistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fNISToolStripMenuItem});
            this.animlistToolStripMenuItem.Name = "animlistToolStripMenuItem";
            this.animlistToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.animlistToolStripMenuItem.Text = "Animlist";
            // 
            // fNISToolStripMenuItem
            // 
            this.fNISToolStripMenuItem.Name = "fNISToolStripMenuItem";
            this.fNISToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.fNISToolStripMenuItem.Text = "FNIS";
            this.fNISToolStripMenuItem.Click += new System.EventHandler(this.fNISToolStripMenuItem_Click);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pathToolStripMenuItem});
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.outputToolStripMenuItem.Text = "Output";
            // 
            // pathToolStripMenuItem
            // 
            this.pathToolStripMenuItem.Name = "pathToolStripMenuItem";
            this.pathToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.pathToolStripMenuItem.Text = "Path";
            this.pathToolStripMenuItem.Click += new System.EventHandler(this.pathToolStripMenuItem_Click);
            // 
            // LaunchButton
            // 
            this.LaunchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LaunchButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.LaunchButton.BackgroundImage = global::Animlist_Transition_Tool.Properties.Resources.av_werewolf;
            this.LaunchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LaunchButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LaunchButton.Location = new System.Drawing.Point(1831, 940);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(61, 63);
            this.LaunchButton.TabIndex = 3;
            this.LaunchButton.UseVisualStyleBackColor = false;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // ModPrefixInput
            // 
            this.ModPrefixInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ModPrefixInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModPrefixInput.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ModPrefixInput.ForeColor = System.Drawing.SystemColors.Control;
            this.ModPrefixInput.Location = new System.Drawing.Point(1128, 38);
            this.ModPrefixInput.MaxLength = 6;
            this.ModPrefixInput.Multiline = false;
            this.ModPrefixInput.Name = "ModPrefixInput";
            this.ModPrefixInput.Size = new System.Drawing.Size(86, 22);
            this.ModPrefixInput.TabIndex = 6;
            this.ModPrefixInput.Text = "";
            this.ModPrefixInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPress);
            // 
            // ModNameInput
            // 
            this.ModNameInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ModNameInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModNameInput.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ModNameInput.ForeColor = System.Drawing.SystemColors.Control;
            this.ModNameInput.Location = new System.Drawing.Point(1128, 66);
            this.ModNameInput.Multiline = false;
            this.ModNameInput.Name = "ModNameInput";
            this.ModNameInput.Size = new System.Drawing.Size(251, 22);
            this.ModNameInput.TabIndex = 8;
            this.ModNameInput.Text = "";
            this.ModNameInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPress);
            // 
            // ModAuthorInput
            // 
            this.ModAuthorInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ModAuthorInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModAuthorInput.DetectUrls = false;
            this.ModAuthorInput.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ModAuthorInput.ForeColor = System.Drawing.SystemColors.Control;
            this.ModAuthorInput.Location = new System.Drawing.Point(1128, 94);
            this.ModAuthorInput.Multiline = false;
            this.ModAuthorInput.Name = "ModAuthorInput";
            this.ModAuthorInput.Size = new System.Drawing.Size(251, 22);
            this.ModAuthorInput.TabIndex = 22;
            this.ModAuthorInput.Text = "";
            this.ModAuthorInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateKeyPress);
            // 
            // ModLinkInput
            // 
            this.ModLinkInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ModLinkInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModLinkInput.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ModLinkInput.ForeColor = System.Drawing.SystemColors.Control;
            this.ModLinkInput.Location = new System.Drawing.Point(1128, 122);
            this.ModLinkInput.Multiline = false;
            this.ModLinkInput.Name = "ModLinkInput";
            this.ModLinkInput.Size = new System.Drawing.Size(251, 22);
            this.ModLinkInput.TabIndex = 23;
            this.ModLinkInput.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(1042, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "Mod Prefix";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(1042, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 19);
            this.label7.TabIndex = 25;
            this.label7.Text = "Mod Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(1036, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 19);
            this.label9.TabIndex = 26;
            this.label9.Text = "Mod Author";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(1054, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 19);
            this.label8.TabIndex = 27;
            this.label8.Text = "Mod Link";
            // 
            // FileProgressBar
            // 
            this.FileProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.FileProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FileProgressBar.Location = new System.Drawing.Point(0, 1018);
            this.FileProgressBar.Name = "FileProgressBar";
            this.FileProgressBar.Size = new System.Drawing.Size(1904, 23);
            this.FileProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.FileProgressBar.TabIndex = 28;
            // 
            // AnimlistTransitionTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.FileProgressBar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModLinkInput);
            this.Controls.Add(this.ModAuthorInput);
            this.Controls.Add(this.ModNameInput);
            this.Controls.Add(this.ModPrefixInput);
            this.Controls.Add(this.LaunchButton);
            this.Controls.Add(this.FileViewBox);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "AnimlistTransitionTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Animlist Transition Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox FileViewBox;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem animlistToolStripMenuItem;
        private ToolStripMenuItem fNISToolStripMenuItem;
        private Button LaunchButton;
        private ToolStripMenuItem outputToolStripMenuItem;
        private ToolStripMenuItem pathToolStripMenuItem;
        private RichTextBox ModPrefixInput;
        private RichTextBox ModNameInput;
        private RichTextBox ModAuthorInput;
        private RichTextBox ModLinkInput;
        private Label label1;
        private Label label7;
        private Label label9;
        private Label label8;
        private ProgressBar FileProgressBar;
    }
}