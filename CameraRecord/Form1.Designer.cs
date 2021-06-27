
namespace CameraRecord
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.palyInfoTrackBar = new System.Windows.Forms.TrackBar();
            this.palyInfoLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.palySpeedScaleComboBox = new System.Windows.Forms.ComboBox();
            this.stopPlaybackButton = new System.Windows.Forms.Button();
            this.playbackButton = new System.Windows.Forms.Button();
            this.stopVisionButton = new System.Windows.Forms.Button();
            this.startVisionButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.palyInfoTrackBar)).BeginInit();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1392, 1169);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.hWindowControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1392, 1091);
            this.panel3.TabIndex = 0;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(1392, 1091);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(1392, 1091);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.palyInfoTrackBar);
            this.panel4.Controls.Add(this.palyInfoLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 1091);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1392, 78);
            this.panel4.TabIndex = 1;
            // 
            // palyInfoTrackBar
            // 
            this.palyInfoTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.palyInfoTrackBar.Location = new System.Drawing.Point(3, 7);
            this.palyInfoTrackBar.Name = "palyInfoTrackBar";
            this.palyInfoTrackBar.Size = new System.Drawing.Size(1244, 69);
            this.palyInfoTrackBar.TabIndex = 0;
            this.palyInfoTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // palyInfoLabel
            // 
            this.palyInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.palyInfoLabel.AutoSize = true;
            this.palyInfoLabel.Location = new System.Drawing.Point(1253, 28);
            this.palyInfoLabel.Name = "palyInfoLabel";
            this.palyInfoLabel.Size = new System.Drawing.Size(35, 18);
            this.palyInfoLabel.TabIndex = 1;
            this.palyInfoLabel.Text = "0/0";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.palySpeedScaleComboBox);
            this.panel2.Controls.Add(this.stopPlaybackButton);
            this.panel2.Controls.Add(this.playbackButton);
            this.panel2.Controls.Add(this.stopVisionButton);
            this.panel2.Controls.Add(this.startVisionButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1392, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 1169);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 627);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "回放速度";
            // 
            // palySpeedScaleComboBox
            // 
            this.palySpeedScaleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.palySpeedScaleComboBox.FormattingEnabled = true;
            this.palySpeedScaleComboBox.Location = new System.Drawing.Point(142, 623);
            this.palySpeedScaleComboBox.Name = "palySpeedScaleComboBox";
            this.palySpeedScaleComboBox.Size = new System.Drawing.Size(159, 26);
            this.palySpeedScaleComboBox.TabIndex = 1;
            // 
            // stopPlaybackButton
            // 
            this.stopPlaybackButton.Location = new System.Drawing.Point(38, 390);
            this.stopPlaybackButton.Name = "stopPlaybackButton";
            this.stopPlaybackButton.Size = new System.Drawing.Size(263, 102);
            this.stopPlaybackButton.TabIndex = 0;
            this.stopPlaybackButton.Text = "停止回放";
            this.stopPlaybackButton.UseVisualStyleBackColor = true;
            this.stopPlaybackButton.Click += new System.EventHandler(this.stopPlaybackButton_Click);
            // 
            // playbackButton
            // 
            this.playbackButton.Location = new System.Drawing.Point(38, 270);
            this.playbackButton.Name = "playbackButton";
            this.playbackButton.Size = new System.Drawing.Size(263, 102);
            this.playbackButton.TabIndex = 0;
            this.playbackButton.Text = "回放";
            this.playbackButton.UseVisualStyleBackColor = true;
            this.playbackButton.Click += new System.EventHandler(this.playbackButton_Click);
            // 
            // stopVisionButton
            // 
            this.stopVisionButton.Location = new System.Drawing.Point(38, 153);
            this.stopVisionButton.Name = "stopVisionButton";
            this.stopVisionButton.Size = new System.Drawing.Size(263, 102);
            this.stopVisionButton.TabIndex = 0;
            this.stopVisionButton.Text = "暂停";
            this.stopVisionButton.UseVisualStyleBackColor = true;
            this.stopVisionButton.Click += new System.EventHandler(this.stopVisionButton_Click);
            // 
            // startVisionButton
            // 
            this.startVisionButton.Location = new System.Drawing.Point(38, 36);
            this.startVisionButton.Name = "startVisionButton";
            this.startVisionButton.Size = new System.Drawing.Size(263, 102);
            this.startVisionButton.TabIndex = 0;
            this.startVisionButton.Text = "开始采图";
            this.startVisionButton.UseVisualStyleBackColor = true;
            this.startVisionButton.Click += new System.EventHandler(this.startVisionButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1169);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1722, 31);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(195, 24);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1722, 1200);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.palyInfoTrackBar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TrackBar palyInfoTrackBar;
        private System.Windows.Forms.Label palyInfoLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button playbackButton;
        private System.Windows.Forms.Button stopVisionButton;
        private System.Windows.Forms.Button startVisionButton;
        private System.Windows.Forms.Button stopPlaybackButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox palySpeedScaleComboBox;
    }
}

