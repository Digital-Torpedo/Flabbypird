namespace Flabbypird
{
    partial class ChangeLevelForm
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
            this.LevelList = new System.Windows.Forms.ListBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.BackToMainMenuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LevelList
            // 
            this.LevelList.FormattingEnabled = true;
            this.LevelList.Location = new System.Drawing.Point(12, 90);
            this.LevelList.Name = "LevelList";
            this.LevelList.Size = new System.Drawing.Size(120, 459);
            this.LevelList.TabIndex = 0;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 9);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(35, 13);
            this.DescriptionLabel.TabIndex = 1;
            this.DescriptionLabel.Text = "label1";
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(697, 526);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(75, 23);
            this.StartGameButton.TabIndex = 2;
            this.StartGameButton.Text = "button1";
            this.StartGameButton.UseVisualStyleBackColor = true;
            // 
            // BackToMainMenuButton
            // 
            this.BackToMainMenuButton.Location = new System.Drawing.Point(147, 526);
            this.BackToMainMenuButton.Name = "BackToMainMenuButton";
            this.BackToMainMenuButton.Size = new System.Drawing.Size(75, 23);
            this.BackToMainMenuButton.TabIndex = 3;
            this.BackToMainMenuButton.Text = "button1";
            this.BackToMainMenuButton.UseVisualStyleBackColor = true;
            // 
            // ChangeLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.BackToMainMenuButton);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.LevelList);
            this.Name = "ChangeLevelForm";
            this.Text = "ChangeLevel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LevelList;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.Button BackToMainMenuButton;
    }
}