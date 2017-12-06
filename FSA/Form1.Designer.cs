namespace FSA
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trans = new System.Windows.Forms.CheckBox();
            this.init = new System.Windows.Forms.CheckBox();
            this.final = new System.Windows.Forms.CheckBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textFSA = new System.Windows.Forms.TextBox();
            this.textGen = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.start = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addMidp = new System.Windows.Forms.Button();
            this.delMidp = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.step = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.クリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.変換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ノード名変換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nfadfaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.簡易化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphGen = new System.Windows.Forms.Button();
            this.cbREmode = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.clearSize = new System.Windows.Forms.Button();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Location = new System.Drawing.Point(30, 52);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1283, 764);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // trans
            // 
            this.trans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trans.AutoSize = true;
            this.trans.Location = new System.Drawing.Point(20, 845);
            this.trans.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.trans.Name = "trans";
            this.trans.Size = new System.Drawing.Size(90, 28);
            this.trans.TabIndex = 1;
            this.trans.Text = "遷移";
            this.trans.UseVisualStyleBackColor = true;
            this.trans.CheckedChanged += new System.EventHandler(this.trans_CheckedChanged);
            // 
            // init
            // 
            this.init.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.init.AutoSize = true;
            this.init.Location = new System.Drawing.Point(137, 845);
            this.init.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.init.Name = "init";
            this.init.Size = new System.Drawing.Size(138, 28);
            this.init.TabIndex = 2;
            this.init.Text = "初期状態";
            this.init.UseVisualStyleBackColor = true;
            this.init.CheckedChanged += new System.EventHandler(this.init_CheckedChanged);
            // 
            // final
            // 
            this.final.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.final.AutoSize = true;
            this.final.Location = new System.Drawing.Point(297, 845);
            this.final.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.final.Name = "final";
            this.final.Size = new System.Drawing.Size(138, 28);
            this.final.TabIndex = 3;
            this.final.Text = "受理状態";
            this.final.UseVisualStyleBackColor = true;
            this.final.CheckedChanged += new System.EventHandler(this.final_CheckedChanged);
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.Location = new System.Drawing.Point(830, 841);
            this.nameBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(483, 31);
            this.nameBox.TabIndex = 4;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // input
            // 
            this.input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.input.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.input.Location = new System.Drawing.Point(101, 898);
            this.input.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(1679, 45);
            this.input.TabIndex = 5;
            this.input.TextChanged += new System.EventHandler(this.input_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 908);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "入力";
            // 
            // textFSA
            // 
            this.textFSA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textFSA.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textFSA.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textFSA.Location = new System.Drawing.Point(1348, 52);
            this.textFSA.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.textFSA.Multiline = true;
            this.textFSA.Name = "textFSA";
            this.textFSA.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textFSA.Size = new System.Drawing.Size(620, 764);
            this.textFSA.TabIndex = 7;
            // 
            // textGen
            // 
            this.textGen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textGen.Location = new System.Drawing.Point(1348, 841);
            this.textGen.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.textGen.Name = "textGen";
            this.textGen.Size = new System.Drawing.Size(163, 46);
            this.textGen.TabIndex = 8;
            this.textGen.Text = "テキスト変換";
            this.textGen.UseVisualStyleBackColor = true;
            this.textGen.Click += new System.EventHandler(this.textGen_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox2.Location = new System.Drawing.Point(16, 955);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1764, 250);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.start.Location = new System.Drawing.Point(1805, 901);
            this.start.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(163, 42);
            this.start.TabIndex = 11;
            this.start.Text = "開始・停止";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Location = new System.Drawing.Point(471, 833);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(93, 46);
            this.deleteButton.TabIndex = 12;
            this.deleteButton.Text = "削除";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addMidp
            // 
            this.addMidp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addMidp.Location = new System.Drawing.Point(586, 833);
            this.addMidp.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.addMidp.Name = "addMidp";
            this.addMidp.Size = new System.Drawing.Size(111, 46);
            this.addMidp.TabIndex = 13;
            this.addMidp.Text = "点追加";
            this.addMidp.UseVisualStyleBackColor = true;
            this.addMidp.Click += new System.EventHandler(this.addMidp_Click);
            // 
            // delMidp
            // 
            this.delMidp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delMidp.Location = new System.Drawing.Point(707, 833);
            this.delMidp.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.delMidp.Name = "delMidp";
            this.delMidp.Size = new System.Drawing.Size(111, 46);
            this.delMidp.TabIndex = 14;
            this.delMidp.Text = "点削除";
            this.delMidp.UseVisualStyleBackColor = true;
            this.delMidp.Click += new System.EventHandler(this.delMidp_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // step
            // 
            this.step.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.step.Location = new System.Drawing.Point(1805, 955);
            this.step.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(163, 46);
            this.step.TabIndex = 17;
            this.step.Text = "ステップ";
            this.step.UseVisualStyleBackColor = true;
            this.step.Click += new System.EventHandler(this.step_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 66);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(13, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1998, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.変換ToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(13, 4, 0, 4);
            this.menuStrip2.Size = new System.Drawing.Size(1998, 42);
            this.menuStrip2.TabIndex = 20;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.クリアToolStripMenuItem,
            this.開くToolStripMenuItem,
            this.保存するToolStripMenuItem,
            this.閉じるToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(92, 34);
            this.toolStripMenuItem1.Text = "ファイル";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // クリアToolStripMenuItem
            // 
            this.クリアToolStripMenuItem.Name = "クリアToolStripMenuItem";
            this.クリアToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.クリアToolStripMenuItem.Text = "クリア";
            this.クリアToolStripMenuItem.Click += new System.EventHandler(this.クリアToolStripMenuItem_Click);
            // 
            // 開くToolStripMenuItem
            // 
            this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
            this.開くToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.開くToolStripMenuItem.Text = "開く";
            this.開くToolStripMenuItem.Click += new System.EventHandler(this.開くToolStripMenuItem_Click);
            // 
            // 保存するToolStripMenuItem
            // 
            this.保存するToolStripMenuItem.Name = "保存するToolStripMenuItem";
            this.保存するToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.保存するToolStripMenuItem.Text = "保存する";
            this.保存するToolStripMenuItem.Click += new System.EventHandler(this.保存するToolStripMenuItem_Click);
            // 
            // 閉じるToolStripMenuItem
            // 
            this.閉じるToolStripMenuItem.Name = "閉じるToolStripMenuItem";
            this.閉じるToolStripMenuItem.Size = new System.Drawing.Size(172, 34);
            this.閉じるToolStripMenuItem.Text = "終了";
            this.閉じるToolStripMenuItem.Click += new System.EventHandler(this.閉じるToolStripMenuItem_Click);
            // 
            // 変換ToolStripMenuItem
            // 
            this.変換ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ノード名変換ToolStripMenuItem,
            this.nfadfaToolStripMenuItem,
            this.簡易化ToolStripMenuItem});
            this.変換ToolStripMenuItem.Name = "変換ToolStripMenuItem";
            this.変換ToolStripMenuItem.Size = new System.Drawing.Size(73, 34);
            this.変換ToolStripMenuItem.Text = "変換";
            this.変換ToolStripMenuItem.Click += new System.EventHandler(this.変換ToolStripMenuItem_Click);
            // 
            // ノード名変換ToolStripMenuItem
            // 
            this.ノード名変換ToolStripMenuItem.Name = "ノード名変換ToolStripMenuItem";
            this.ノード名変換ToolStripMenuItem.Size = new System.Drawing.Size(282, 34);
            this.ノード名変換ToolStripMenuItem.Text = "状態名変換";
            this.ノード名変換ToolStripMenuItem.Click += new System.EventHandler(this.ノード名変換ToolStripMenuItem_Click);
            // 
            // nfadfaToolStripMenuItem
            // 
            this.nfadfaToolStripMenuItem.Name = "nfadfaToolStripMenuItem";
            this.nfadfaToolStripMenuItem.Size = new System.Drawing.Size(282, 34);
            this.nfadfaToolStripMenuItem.Text = "nfa→dfa変換";
            this.nfadfaToolStripMenuItem.Click += new System.EventHandler(this.nfadfaToolStripMenuItem_Click);
            // 
            // 簡易化ToolStripMenuItem
            // 
            this.簡易化ToolStripMenuItem.Name = "簡易化ToolStripMenuItem";
            this.簡易化ToolStripMenuItem.Size = new System.Drawing.Size(282, 34);
            this.簡易化ToolStripMenuItem.Text = "簡易化(dfaのみ可)";
            this.簡易化ToolStripMenuItem.Click += new System.EventHandler(this.簡易化ToolStripMenuItem_Click);
            // 
            // graphGen
            // 
            this.graphGen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.graphGen.Location = new System.Drawing.Point(1524, 841);
            this.graphGen.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.graphGen.Name = "graphGen";
            this.graphGen.Size = new System.Drawing.Size(145, 46);
            this.graphGen.TabIndex = 21;
            this.graphGen.Text = "グラフ変換";
            this.graphGen.UseVisualStyleBackColor = true;
            this.graphGen.Click += new System.EventHandler(this.graphGen_Click);
            // 
            // cbREmode
            // 
            this.cbREmode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbREmode.AutoSize = true;
            this.cbREmode.Location = new System.Drawing.Point(1725, 853);
            this.cbREmode.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.cbREmode.Name = "cbREmode";
            this.cbREmode.Size = new System.Drawing.Size(241, 28);
            this.cbREmode.TabIndex = 23;
            this.cbREmode.Text = "正規表現変換モード";
            this.cbREmode.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(1805, 1047);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(167, 90);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Value = 5;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // clearSize
            // 
            this.clearSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSize.Location = new System.Drawing.Point(1805, 1143);
            this.clearSize.Name = "clearSize";
            this.clearSize.Size = new System.Drawing.Size(161, 46);
            this.clearSize.TabIndex = 25;
            this.clearSize.Text = "サイズ戻し";
            this.clearSize.UseVisualStyleBackColor = true;
            this.clearSize.Click += new System.EventHandler(this.clearSize_Click);
            // 
            // menuStrip3
            // 
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(1998, 24);
            this.menuStrip3.TabIndex = 26;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1998, 1220);
            this.Controls.Add(this.clearSize);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.cbREmode);
            this.Controls.Add(this.graphGen);
            this.Controls.Add(this.step);
            this.Controls.Add(this.delMidp);
            this.Controls.Add(this.addMidp);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.start);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textGen);
            this.Controls.Add(this.textFSA);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.input);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.final);
            this.Controls.Add(this.init);
            this.Controls.Add(this.trans);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip3);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "Form1";
            this.Text = "計算機数学2017前期：有限オートマトン";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox trans;
        private System.Windows.Forms.CheckBox init;
        private System.Windows.Forms.CheckBox final;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFSA;
        private System.Windows.Forms.Button textGen;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addMidp;
        private System.Windows.Forms.Button delMidp;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button step;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 開くToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存するToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 閉じるToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 変換ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nfadfaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 簡易化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem クリアToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ノード名変換ToolStripMenuItem;
        private System.Windows.Forms.Button graphGen;
        private System.Windows.Forms.CheckBox cbREmode;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button clearSize;
        private System.Windows.Forms.MenuStrip menuStrip3;
    }
}

