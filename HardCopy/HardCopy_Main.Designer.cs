namespace HardCopy
{
    partial class HardCopy_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.list_log = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bt_Set = new System.Windows.Forms.Button();
            this.bt_Close = new System.Windows.Forms.Button();
            this.bt_Capture = new System.Windows.Forms.Button();
            this.bt_Crop = new System.Windows.Forms.Button();
            this.bt_Create_Image = new System.Windows.Forms.Button();
            this.bt_Print = new System.Windows.Forms.Button();
            this.pb_printImg = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.usbstatus_img = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.process_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.process_gague = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.u_ClockCount1 = new HardCopy.U_ClockCount();
            this.u_HV1 = new HardCopy.U_HV();
            this.u_Margin1 = new HardCopy.U_Margin();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_printImg)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_log
            // 
            this.list_log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_log.FormattingEnabled = true;
            this.list_log.ItemHeight = 12;
            this.list_log.Location = new System.Drawing.Point(0, 429);
            this.list_log.Name = "list_log";
            this.list_log.Size = new System.Drawing.Size(684, 148);
            this.list_log.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bt_Set);
            this.panel1.Controls.Add(this.bt_Close);
            this.panel1.Controls.Add(this.bt_Capture);
            this.panel1.Controls.Add(this.bt_Crop);
            this.panel1.Controls.Add(this.bt_Create_Image);
            this.panel1.Controls.Add(this.bt_Print);
            this.panel1.Controls.Add(this.u_ClockCount1);
            this.panel1.Controls.Add(this.u_HV1);
            this.panel1.Controls.Add(this.u_Margin1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 137);
            this.panel1.TabIndex = 1;
            // 
            // bt_Set
            // 
            this.bt_Set.Location = new System.Drawing.Point(529, 71);
            this.bt_Set.Name = "bt_Set";
            this.bt_Set.Size = new System.Drawing.Size(75, 29);
            this.bt_Set.TabIndex = 15;
            this.bt_Set.Text = "설 정";
            this.bt_Set.UseVisualStyleBackColor = true;
            this.bt_Set.Click += new System.EventHandler(this.bt_Set_Click);
            // 
            // bt_Close
            // 
            this.bt_Close.Location = new System.Drawing.Point(453, 103);
            this.bt_Close.Name = "bt_Close";
            this.bt_Close.Size = new System.Drawing.Size(75, 29);
            this.bt_Close.TabIndex = 14;
            this.bt_Close.Text = "USB닫기";
            this.bt_Close.UseVisualStyleBackColor = true;
            this.bt_Close.Click += new System.EventHandler(this.bt_Close_Click);
            // 
            // bt_Capture
            // 
            this.bt_Capture.Location = new System.Drawing.Point(453, 71);
            this.bt_Capture.Name = "bt_Capture";
            this.bt_Capture.Size = new System.Drawing.Size(75, 29);
            this.bt_Capture.TabIndex = 13;
            this.bt_Capture.Text = "요 청";
            this.bt_Capture.UseVisualStyleBackColor = true;
            this.bt_Capture.Click += new System.EventHandler(this.bt_Capture_Click);
            // 
            // bt_Crop
            // 
            this.bt_Crop.Location = new System.Drawing.Point(453, 39);
            this.bt_Crop.Name = "bt_Crop";
            this.bt_Crop.Size = new System.Drawing.Size(75, 29);
            this.bt_Crop.TabIndex = 11;
            this.bt_Crop.Text = "CROP";
            this.bt_Crop.UseVisualStyleBackColor = true;
            this.bt_Crop.Click += new System.EventHandler(this.bt_Crop_Click);
            // 
            // bt_Create_Image
            // 
            this.bt_Create_Image.Location = new System.Drawing.Point(529, 39);
            this.bt_Create_Image.Name = "bt_Create_Image";
            this.bt_Create_Image.Size = new System.Drawing.Size(75, 29);
            this.bt_Create_Image.TabIndex = 12;
            this.bt_Create_Image.Text = "이미지";
            this.bt_Create_Image.UseVisualStyleBackColor = true;
            this.bt_Create_Image.Click += new System.EventHandler(this.bt_Create_Image_Click);
            // 
            // bt_Print
            // 
            this.bt_Print.Location = new System.Drawing.Point(605, 38);
            this.bt_Print.Name = "bt_Print";
            this.bt_Print.Size = new System.Drawing.Size(75, 29);
            this.bt_Print.TabIndex = 10;
            this.bt_Print.Text = "인쇄";
            this.bt_Print.UseVisualStyleBackColor = true;
            this.bt_Print.Click += new System.EventHandler(this.bt_Print_Click);
            // 
            // pb_printImg
            // 
            this.pb_printImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_printImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_printImg.Location = new System.Drawing.Point(0, 0);
            this.pb_printImg.Name = "pb_printImg";
            this.pb_printImg.Size = new System.Drawing.Size(684, 292);
            this.pb_printImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_printImg.TabIndex = 9;
            this.pb_printImg.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usbstatus_img,
            this.toolStripStatusLabel1,
            this.process_status,
            this.process_gague,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 577);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 24);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // usbstatus_img
            // 
            this.usbstatus_img.AutoSize = false;
            this.usbstatus_img.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.usbstatus_img.Name = "usbstatus_img";
            this.usbstatus_img.Size = new System.Drawing.Size(55, 19);
            this.usbstatus_img.Text = "USB:";
            this.usbstatus_img.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.usbstatus_img.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 19);
            this.toolStripStatusLabel1.Text = "STATUS";
            // 
            // process_status
            // 
            this.process_status.AutoSize = false;
            this.process_status.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.process_status.Name = "process_status";
            this.process_status.Size = new System.Drawing.Size(150, 19);
            this.process_status.Text = "데이터 요청 (100/100)";
            // 
            // process_gague
            // 
            this.process_gague.Maximum = 300;
            this.process_gague.Name = "process_gague";
            this.process_gague.Size = new System.Drawing.Size(200, 18);
            this.process_gague.Step = 1;
            this.process_gague.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.process_gague.Value = 1;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(84, 19);
            this.toolStripStatusLabel3.Spring = true;
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(125, 19);
            this.toolStripStatusLabel4.Text = "0000-00-00 00:00:00";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pb_printImg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(684, 292);
            this.panel2.TabIndex = 14;
            // 
            // u_ClockCount1
            // 
            this.u_ClockCount1.BackColor = System.Drawing.Color.White;
            this.u_ClockCount1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.u_ClockCount1.CLOCK = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.u_ClockCount1.Location = new System.Drawing.Point(453, 11);
            this.u_ClockCount1.Name = "u_ClockCount1";
            this.u_ClockCount1.Size = new System.Drawing.Size(200, 27);
            this.u_ClockCount1.TabIndex = 2;
            // 
            // u_HV1
            // 
            this.u_HV1.BackColor = System.Drawing.Color.White;
            this.u_HV1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.u_HV1.HORIZONTAL = 1188;
            this.u_HV1.Location = new System.Drawing.Point(220, 11);
            this.u_HV1.Margin_BOTTOM = 0;
            this.u_HV1.Margin_Left = 0;
            this.u_HV1.Margin_Right = 0;
            this.u_HV1.Margin_TOP = 0;
            this.u_HV1.Name = "u_HV1";
            this.u_HV1.Size = new System.Drawing.Size(227, 122);
            this.u_HV1.TabIndex = 1;
            this.u_HV1.VERTICAL = 299;
            // 
            // u_Margin1
            // 
            this.u_Margin1.BackColor = System.Drawing.Color.White;
            this.u_Margin1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.u_Margin1.Location = new System.Drawing.Point(10, 11);
            this.u_Margin1.Margin_BOTTOM = 0;
            this.u_Margin1.Margin_LEFT = 0;
            this.u_Margin1.Margin_RIGHT = 0;
            this.u_Margin1.Margin_TOP = 0;
            this.u_Margin1.Name = "u_Margin1";
            this.u_Margin1.Size = new System.Drawing.Size(204, 121);
            this.u_Margin1.TabIndex = 0;
            // 
            // HardCopy_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 601);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.list_log);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "HardCopy_Main";
            this.Text = "HardCopy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HardCopy_Main_FormClosing);
            this.Load += new System.EventHandler(this.HardCopy_Main_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_printImg)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_log;
        private System.Windows.Forms.Panel panel1;
        private U_Margin u_Margin1;
        private U_ClockCount u_ClockCount1;
        private U_HV u_HV1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel usbstatus_img;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel process_status;
        private System.Windows.Forms.ToolStripProgressBar process_gague;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.PictureBox pb_printImg;
        private System.Windows.Forms.Button bt_Capture;
        private System.Windows.Forms.Button bt_Crop;
        private System.Windows.Forms.Button bt_Create_Image;
        private System.Windows.Forms.Button bt_Print;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bt_Close;
        private System.Windows.Forms.Button bt_Set;
    }
}

