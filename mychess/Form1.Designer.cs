namespace mychess
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param Name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewLanGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDefeat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.gbChessField = new System.Windows.Forms.GroupBox();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsGame,
            this.tsHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(602, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsGame
            // 
            this.tsGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNewGame,
            this.tsNewLanGame,
            this.tsLoad,
            this.toolStripSeparator1,
            this.tsUndo,
            this.tsSave,
            this.tsDefeat,
            this.toolStripSeparator2,
            this.tsStats,
            this.toolStripSeparator3,
            this.tsExit});
            this.tsGame.Image = ((System.Drawing.Image)(resources.GetObject("tsGame.Image")));
            this.tsGame.Name = "tsGame";
            this.tsGame.Size = new System.Drawing.Size(62, 20);
            this.tsGame.Text = "Игра";
            // 
            // tsNewGame
            // 
            this.tsNewGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tsNewGame.Image = ((System.Drawing.Image)(resources.GetObject("tsNewGame.Image")));
            this.tsNewGame.Name = "tsNewGame";
            this.tsNewGame.Size = new System.Drawing.Size(180, 22);
            this.tsNewGame.Text = "Новая игра";
            this.tsNewGame.Click += new System.EventHandler(this.tsNewGame_Click);
            // 
            // tsNewLanGame
            // 
            this.tsNewLanGame.Image = ((System.Drawing.Image)(resources.GetObject("tsNewLanGame.Image")));
            this.tsNewLanGame.Name = "tsNewLanGame";
            this.tsNewLanGame.Size = new System.Drawing.Size(180, 22);
            this.tsNewLanGame.Text = "Новая игра по сети";
            // 
            // tsLoad
            // 
            this.tsLoad.Name = "tsLoad";
            this.tsLoad.Size = new System.Drawing.Size(180, 22);
            this.tsLoad.Text = "Загрузить игру";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsUndo
            // 
            this.tsUndo.Enabled = false;
            this.tsUndo.Name = "tsUndo";
            this.tsUndo.Size = new System.Drawing.Size(180, 22);
            this.tsUndo.Text = "Отменить ход";
            // 
            // tsSave
            // 
            this.tsSave.Enabled = false;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(180, 22);
            this.tsSave.Text = "Сохранить игру";
            // 
            // tsDefeat
            // 
            this.tsDefeat.Enabled = false;
            this.tsDefeat.Image = ((System.Drawing.Image)(resources.GetObject("tsDefeat.Image")));
            this.tsDefeat.Name = "tsDefeat";
            this.tsDefeat.Size = new System.Drawing.Size(180, 22);
            this.tsDefeat.Text = "Сдаться";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // tsStats
            // 
            this.tsStats.Image = ((System.Drawing.Image)(resources.GetObject("tsStats.Image")));
            this.tsStats.Name = "tsStats";
            this.tsStats.Size = new System.Drawing.Size(180, 22);
            this.tsStats.Text = "Статистика";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // tsExit
            // 
            this.tsExit.Name = "tsExit";
            this.tsExit.Size = new System.Drawing.Size(180, 22);
            this.tsExit.Text = "Выход";
            // 
            // tsHelp
            // 
            this.tsHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem4});
            this.tsHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsHelp.Image")));
            this.tsHelp.Name = "tsHelp";
            this.tsHelp.Size = new System.Drawing.Size(81, 20);
            this.tsHelp.Text = "Справка";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem2.Text = "Правила";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem4.Text = "О программе";
            // 
            // gbChessField
            // 
            this.gbChessField.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gbChessField.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbChessField.Location = new System.Drawing.Point(101, 45);
            this.gbChessField.Margin = new System.Windows.Forms.Padding(0);
            this.gbChessField.Name = "gbChessField";
            this.gbChessField.Padding = new System.Windows.Forms.Padding(0);
            this.gbChessField.Size = new System.Drawing.Size(400, 400);
            this.gbChessField.TabIndex = 17;
            this.gbChessField.TabStop = false;
            this.gbChessField.Visible = false;
            this.gbChessField.Enter += new System.EventHandler(this.gbChessField_Enter);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(101, 467);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(400, 86);
            this.rtbLog.TabIndex = 18;
            this.rtbLog.Text = "";
            this.rtbLog.Visible = false;
            this.rtbLog.TextChanged += new System.EventHandler(this.rtbLog_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(508, 467);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(82, 86);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(85, 86);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(452, 375);
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(602, 580);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.gbChessField);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Power Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsGame;
        private System.Windows.Forms.ToolStripMenuItem tsHelp;
        private System.Windows.Forms.ToolStripMenuItem tsNewGame;
        private System.Windows.Forms.ToolStripMenuItem tsNewLanGame;
        private System.Windows.Forms.ToolStripMenuItem tsStats;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsSave;
        private System.Windows.Forms.ToolStripMenuItem tsDefeat;
        private System.Windows.Forms.ToolStripMenuItem tsLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsExit;
        private System.Windows.Forms.GroupBox gbChessField;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}

