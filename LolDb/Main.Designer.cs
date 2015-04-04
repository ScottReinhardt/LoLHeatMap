namespace LolDb
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.BtnToggleDownload = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.JobIdTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // BtnToggleDownload
            // 
            this.BtnToggleDownload.Location = new System.Drawing.Point(12, 12);
            this.BtnToggleDownload.Name = "BtnToggleDownload";
            this.BtnToggleDownload.Size = new System.Drawing.Size(130, 23);
            this.BtnToggleDownload.TabIndex = 2;
            this.BtnToggleDownload.Text = "Start Downloading";
            this.BtnToggleDownload.Click += new System.EventHandler(this.BtnToggleDownload_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Write Data To File";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // JobIdTimer
            // 
            this.JobIdTimer.Interval = 300000;
            this.JobIdTimer.Tick += new System.EventHandler(this.JobIdTimer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 57);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnToggleDownload);
            this.Name = "Main";
            this.Text = "Lol Api Challenge";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnToggleDownload;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer JobIdTimer;
    }
}

