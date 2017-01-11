namespace Picachu_BG_PRO {
    partial class MultiForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btNewGame = new System.Windows.Forms.Button();
            this.btJoinGame = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sz1 = new System.Windows.Forms.RadioButton();
            this.sz2 = new System.Windows.Forms.RadioButton();
            this.sz3 = new System.Windows.Forms.RadioButton();
            this.sz4 = new System.Windows.Forms.RadioButton();
            this.listGame = new System.Windows.Forms.ListBox();
            this.sz0 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your name: ";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(83, 19);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(401, 20);
            this.tbName.TabIndex = 1;
            // 
            // btNewGame
            // 
            this.btNewGame.Location = new System.Drawing.Point(15, 208);
            this.btNewGame.Name = "btNewGame";
            this.btNewGame.Size = new System.Drawing.Size(127, 23);
            this.btNewGame.TabIndex = 2;
            this.btNewGame.Text = "Create a new game";
            this.btNewGame.UseVisualStyleBackColor = true;
            this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click);
            // 
            // btJoinGame
            // 
            this.btJoinGame.Location = new System.Drawing.Point(233, 208);
            this.btJoinGame.Name = "btJoinGame";
            this.btJoinGame.Size = new System.Drawing.Size(178, 23);
            this.btJoinGame.TabIndex = 3;
            this.btJoinGame.Text = "Join a game";
            this.btJoinGame.UseVisualStyleBackColor = true;
            this.btJoinGame.Click += new System.EventHandler(this.btJoinGame_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Game size:";
            // 
            // sz1
            // 
            this.sz1.AutoSize = true;
            this.sz1.Location = new System.Drawing.Point(83, 88);
            this.sz1.Name = "sz1";
            this.sz1.Size = new System.Drawing.Size(60, 17);
            this.sz1.TabIndex = 5;
            this.sz1.Text = "14 x 11";
            this.sz1.UseVisualStyleBackColor = true;
            // 
            // sz2
            // 
            this.sz2.AutoSize = true;
            this.sz2.Location = new System.Drawing.Point(83, 120);
            this.sz2.Name = "sz2";
            this.sz2.Size = new System.Drawing.Size(60, 17);
            this.sz2.TabIndex = 6;
            this.sz2.Text = "11 x 10";
            this.sz2.UseVisualStyleBackColor = true;
            // 
            // sz3
            // 
            this.sz3.AutoSize = true;
            this.sz3.Location = new System.Drawing.Point(83, 150);
            this.sz3.Name = "sz3";
            this.sz3.Size = new System.Drawing.Size(54, 17);
            this.sz3.TabIndex = 7;
            this.sz3.Text = "10 x 7";
            this.sz3.UseVisualStyleBackColor = true;
            // 
            // sz4
            // 
            this.sz4.AutoSize = true;
            this.sz4.Location = new System.Drawing.Point(83, 185);
            this.sz4.Name = "sz4";
            this.sz4.Size = new System.Drawing.Size(48, 17);
            this.sz4.TabIndex = 8;
            this.sz4.Text = "6 x 5";
            this.sz4.UseVisualStyleBackColor = true;
            // 
            // listGame
            // 
            this.listGame.FormattingEnabled = true;
            this.listGame.Location = new System.Drawing.Point(175, 56);
            this.listGame.Name = "listGame";
            this.listGame.ScrollAlwaysVisible = true;
            this.listGame.Size = new System.Drawing.Size(309, 134);
            this.listGame.TabIndex = 9;
            // 
            // sz0
            // 
            this.sz0.AutoSize = true;
            this.sz0.Checked = true;
            this.sz0.Location = new System.Drawing.Point(83, 56);
            this.sz0.Name = "sz0";
            this.sz0.Size = new System.Drawing.Size(60, 17);
            this.sz0.TabIndex = 10;
            this.sz0.TabStop = true;
            this.sz0.Text = "16 x 12";
            this.sz0.UseVisualStyleBackColor = true;
            // 
            // MultiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 263);
            this.Controls.Add(this.sz0);
            this.Controls.Add(this.listGame);
            this.Controls.Add(this.sz4);
            this.Controls.Add(this.sz3);
            this.Controls.Add(this.sz2);
            this.Controls.Add(this.sz1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btJoinGame);
            this.Controls.Add(this.btNewGame);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiForm";
            this.Text = "Local Area Network";
            this.Load += new System.EventHandler(this.MultiForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MultiForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btNewGame;
        private System.Windows.Forms.Button btJoinGame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton sz1;
        private System.Windows.Forms.RadioButton sz2;
        private System.Windows.Forms.RadioButton sz3;
        private System.Windows.Forms.RadioButton sz4;
        private System.Windows.Forms.ListBox listGame;
        private System.Windows.Forms.RadioButton sz0;
    }
}