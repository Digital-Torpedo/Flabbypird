namespace Flabbypird
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChangeUserButton = new System.Windows.Forms.Button();
            this.HighScoreButton = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            this.ImpressumButtom = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.GameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChangeUserButton
            // 
            this.ChangeUserButton.Location = new System.Drawing.Point(266, 242);
            this.ChangeUserButton.Name = "ChangeUserButton";
            this.ChangeUserButton.Size = new System.Drawing.Size(144, 23);
            this.ChangeUserButton.TabIndex = 2;
            this.ChangeUserButton.Text = "button1";
            this.ChangeUserButton.UseVisualStyleBackColor = true;
            // 
            // HighScoreButton
            // 
            this.HighScoreButton.Location = new System.Drawing.Point(266, 213);
            this.HighScoreButton.Name = "HighScoreButton";
            this.HighScoreButton.Size = new System.Drawing.Size(144, 23);
            this.HighScoreButton.TabIndex = 1;
            this.HighScoreButton.Text = "button2";
            this.HighScoreButton.UseVisualStyleBackColor = true;
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(266, 329);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(144, 23);
            this.HelpButton.TabIndex = 5;
            this.HelpButton.Text = "button4";
            this.HelpButton.UseVisualStyleBackColor = true;
            // 
            // ImpressumButtom
            // 
            this.ImpressumButtom.Location = new System.Drawing.Point(266, 271);
            this.ImpressumButtom.Name = "ImpressumButtom";
            this.ImpressumButtom.Size = new System.Drawing.Size(144, 23);
            this.ImpressumButtom.TabIndex = 3;
            this.ImpressumButtom.Text = "button1";
            this.ImpressumButtom.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(266, 300);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(144, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "button1";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // GameButton
            // 
            this.GameButton.Location = new System.Drawing.Point(266, 184);
            this.GameButton.Name = "GameButton";
            this.GameButton.Size = new System.Drawing.Size(144, 23);
            this.GameButton.TabIndex = 0;
            this.GameButton.Text = "button1";
            this.GameButton.UseVisualStyleBackColor = true;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 567);
            this.Controls.Add(this.GameButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ImpressumButtom);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.HighScoreButton);
            this.Controls.Add(this.ChangeUserButton);
            this.Name = "MainMenuForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ChangeUserButton;
        private System.Windows.Forms.Button HighScoreButton;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.Button ImpressumButtom;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button GameButton;

    }
}

