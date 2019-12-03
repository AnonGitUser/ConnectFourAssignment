namespace ConnectFour
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnRedPlayer = new System.Windows.Forms.Button();
            this.btnYellowPlayer = new System.Windows.Forms.Button();
            this.tbPlayerName = new System.Windows.Forms.TextBox();
            this.lblRScore = new System.Windows.Forms.Label();
            this.lblYScore = new System.Windows.Forms.Label();
            this.lblRedPlayer = new System.Windows.Forms.Label();
            this.lblYellowPlayer = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1009, 28);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.saveToolStripMenuItem.Text = "Customise";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(630, 42);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(138, 49);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset Board";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.BackColor = System.Drawing.Color.Black;
            this.lblPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer.ForeColor = System.Drawing.Color.Red;
            this.lblPlayer.Location = new System.Drawing.Point(582, -9);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(250, 32);
            this.lblPlayer.TabIndex = 15;
            this.lblPlayer.Text = "Red Players Turn";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(750, 456);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(138, 49);
            this.btnExit.TabIndex = 27;
            this.btnExit.Text = "Exit Game";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(606, 456);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(138, 49);
            this.btnRestart.TabIndex = 26;
            this.btnRestart.Text = "Restart Game";
            this.btnRestart.UseVisualStyleBackColor = true;
            // 
            // btnRedPlayer
            // 
            this.btnRedPlayer.Location = new System.Drawing.Point(606, 331);
            this.btnRedPlayer.Name = "btnRedPlayer";
            this.btnRedPlayer.Size = new System.Drawing.Size(88, 49);
            this.btnRedPlayer.TabIndex = 25;
            this.btnRedPlayer.Text = "Confirm Red Player";
            this.btnRedPlayer.UseVisualStyleBackColor = true;
            this.btnRedPlayer.Visible = false;
            // 
            // btnYellowPlayer
            // 
            this.btnYellowPlayer.Location = new System.Drawing.Point(709, 331);
            this.btnYellowPlayer.Name = "btnYellowPlayer";
            this.btnYellowPlayer.Size = new System.Drawing.Size(138, 49);
            this.btnYellowPlayer.TabIndex = 24;
            this.btnYellowPlayer.Text = "Confirm Yellow Player";
            this.btnYellowPlayer.UseVisualStyleBackColor = true;
            this.btnYellowPlayer.Visible = false;
            // 
            // tbPlayerName
            // 
            this.tbPlayerName.Location = new System.Drawing.Point(606, 303);
            this.tbPlayerName.Name = "tbPlayerName";
            this.tbPlayerName.Size = new System.Drawing.Size(241, 22);
            this.tbPlayerName.TabIndex = 23;
            this.tbPlayerName.Text = "Enter red players name";
            this.tbPlayerName.Visible = false;
            // 
            // lblRScore
            // 
            this.lblRScore.AutoSize = true;
            this.lblRScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRScore.Location = new System.Drawing.Point(602, 217);
            this.lblRScore.Name = "lblRScore";
            this.lblRScore.Size = new System.Drawing.Size(18, 20);
            this.lblRScore.TabIndex = 22;
            this.lblRScore.Text = "0";
            // 
            // lblYScore
            // 
            this.lblYScore.AutoSize = true;
            this.lblYScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYScore.Location = new System.Drawing.Point(769, 217);
            this.lblYScore.Name = "lblYScore";
            this.lblYScore.Size = new System.Drawing.Size(18, 20);
            this.lblYScore.TabIndex = 21;
            this.lblYScore.Text = "0";
            // 
            // lblRedPlayer
            // 
            this.lblRedPlayer.AutoSize = true;
            this.lblRedPlayer.BackColor = System.Drawing.Color.Black;
            this.lblRedPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPlayer.ForeColor = System.Drawing.Color.Red;
            this.lblRedPlayer.Location = new System.Drawing.Point(602, 186);
            this.lblRedPlayer.Name = "lblRedPlayer";
            this.lblRedPlayer.Size = new System.Drawing.Size(42, 20);
            this.lblRedPlayer.TabIndex = 20;
            this.lblRedPlayer.Text = "Red";
            // 
            // lblYellowPlayer
            // 
            this.lblYellowPlayer.AutoSize = true;
            this.lblYellowPlayer.BackColor = System.Drawing.Color.Black;
            this.lblYellowPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYellowPlayer.ForeColor = System.Drawing.Color.Yellow;
            this.lblYellowPlayer.Location = new System.Drawing.Point(769, 186);
            this.lblYellowPlayer.Name = "lblYellowPlayer";
            this.lblYellowPlayer.Size = new System.Drawing.Size(63, 20);
            this.lblYellowPlayer.TabIndex = 19;
            this.lblYellowPlayer.Text = "Yellow";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(668, 132);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(53, 20);
            this.lblScore.TabIndex = 18;
            this.lblScore.Text = "Score";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 565);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnRedPlayer);
            this.Controls.Add(this.btnYellowPlayer);
            this.Controls.Add(this.tbPlayerName);
            this.Controls.Add(this.lblRScore);
            this.Controls.Add(this.lblYScore);
            this.Controls.Add(this.lblRedPlayer);
            this.Controls.Add(this.lblYellowPlayer);
            this.Controls.Add(this.lblScore);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnRedPlayer;
        private System.Windows.Forms.Button btnYellowPlayer;
        private System.Windows.Forms.TextBox tbPlayerName;
        private System.Windows.Forms.Label lblRScore;
        private System.Windows.Forms.Label lblYScore;
        private System.Windows.Forms.Label lblRedPlayer;
        private System.Windows.Forms.Label lblYellowPlayer;
        private System.Windows.Forms.Label lblScore;
    }
}

