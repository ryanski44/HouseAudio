namespace HouseAudioReceiver
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
            this.label1 = new System.Windows.Forms.Label();
            this.textHost = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.textNTPHost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownDelay = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownJitter = new System.Windows.Forms.NumericUpDown();
            this.buttonAutoJitter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJitter)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Multicast IP";
            // 
            // textHost
            // 
            this.textHost.Location = new System.Drawing.Point(162, 14);
            this.textHost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textHost.Name = "textHost";
            this.textHost.Size = new System.Drawing.Size(370, 26);
            this.textHost.TabIndex = 1;
            this.textHost.Text = "239.0.0.222";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(543, 11);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(112, 35);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(18, 117);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(705, 303);
            this.dataGridView.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "NTP Server";
            // 
            // textNTPHost
            // 
            this.textNTPHost.Location = new System.Drawing.Point(162, 54);
            this.textNTPHost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNTPHost.Name = "textNTPHost";
            this.textNTPHost.Size = new System.Drawing.Size(370, 26);
            this.textNTPHost.TabIndex = 1;
            this.textNTPHost.Text = "192.168.2.9";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Delay";
            // 
            // numericUpDownDelay
            // 
            this.numericUpDownDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownDelay.Location = new System.Drawing.Point(162, 88);
            this.numericUpDownDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownDelay.Name = "numericUpDownDelay";
            this.numericUpDownDelay.Size = new System.Drawing.Size(120, 26);
            this.numericUpDownDelay.TabIndex = 7;
            this.numericUpDownDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDelay.ValueChanged += new System.EventHandler(this.numericUpDownDelay_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Jitter";
            // 
            // numericUpDownJitter
            // 
            this.numericUpDownJitter.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownJitter.Location = new System.Drawing.Point(349, 86);
            this.numericUpDownJitter.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownJitter.Name = "numericUpDownJitter";
            this.numericUpDownJitter.Size = new System.Drawing.Size(120, 26);
            this.numericUpDownJitter.TabIndex = 7;
            this.numericUpDownJitter.ValueChanged += new System.EventHandler(this.numericUpDownJitter_ValueChanged);
            // 
            // buttonAutoJitter
            // 
            this.buttonAutoJitter.Location = new System.Drawing.Point(543, 54);
            this.buttonAutoJitter.Name = "buttonAutoJitter";
            this.buttonAutoJitter.Size = new System.Drawing.Size(112, 33);
            this.buttonAutoJitter.TabIndex = 8;
            this.buttonAutoJitter.Text = "Auto Jitter";
            this.buttonAutoJitter.UseVisualStyleBackColor = true;
            this.buttonAutoJitter.Click += new System.EventHandler(this.buttonAutoJitter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 438);
            this.Controls.Add(this.buttonAutoJitter);
            this.Controls.Add(this.numericUpDownJitter);
            this.Controls.Add(this.numericUpDownDelay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textNTPHost);
            this.Controls.Add(this.textHost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJitter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textHost;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textNTPHost;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownJitter;
        private System.Windows.Forms.Button buttonAutoJitter;
    }
}

