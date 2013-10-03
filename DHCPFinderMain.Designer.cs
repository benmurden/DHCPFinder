namespace DHCPFinder
{
    partial class DHCPFinderForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInterface = new System.Windows.Forms.Label();
            this.cmbInterfaces = new System.Windows.Forms.ComboBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.grdOutput = new System.Windows.Forms.DataGridView();
            this.toolTipTimer = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipRealMac = new System.Windows.Forms.ToolTip(this.components);
            this.txtRealMac = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.numTimerInterval = new System.Windows.Forms.NumericUpDown();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSrcMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCIADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYIADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSIADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGIADDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 370);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Real DHCP MAC";
            // 
            // lblInterface
            // 
            this.lblInterface.AutoSize = true;
            this.lblInterface.Location = new System.Drawing.Point(9, 64);
            this.lblInterface.Name = "lblInterface";
            this.lblInterface.Size = new System.Drawing.Size(49, 13);
            this.lblInterface.TabIndex = 4;
            this.lblInterface.Text = "Interface";
            // 
            // cmbInterfaces
            // 
            this.cmbInterfaces.FormattingEnabled = true;
            this.cmbInterfaces.Location = new System.Drawing.Point(103, 61);
            this.cmbInterfaces.Name = "cmbInterfaces";
            this.cmbInterfaces.Size = new System.Drawing.Size(604, 21);
            this.cmbInterfaces.TabIndex = 2;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(9, 38);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(55, 13);
            this.lblTimer.TabIndex = 7;
            this.lblTimer.Text = "Timer (ms)";
            // 
            // grdOutput
            // 
            this.grdOutput.AllowUserToAddRows = false;
            this.grdOutput.AllowUserToDeleteRows = false;
            this.grdOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colSrcMac,
            this.colCIADDR,
            this.colYIADDR,
            this.colSIADDR,
            this.colGIADDR,
            this.colHost});
            this.grdOutput.Location = new System.Drawing.Point(12, 88);
            this.grdOutput.Name = "grdOutput";
            this.grdOutput.ReadOnly = true;
            this.grdOutput.RowHeadersVisible = false;
            this.grdOutput.Size = new System.Drawing.Size(695, 276);
            this.grdOutput.TabIndex = 9;
            // 
            // toolTipTimer
            // 
            this.toolTipTimer.ToolTipTitle = "DHCPDISCOVER Timer";
            // 
            // toolTipRealMac
            // 
            this.toolTipRealMac.ToolTipTitle = "Real DHCP MAC Address";
            // 
            // txtRealMac
            // 
            this.txtRealMac.Location = new System.Drawing.Point(103, 9);
            this.txtRealMac.MaxLength = 12;
            this.txtRealMac.Name = "txtRealMac";
            this.txtRealMac.Size = new System.Drawing.Size(100, 20);
            this.txtRealMac.TabIndex = 0;
            this.txtRealMac.Text = "000000000000";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(174, 375);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 10;
            this.lblVersion.Text = "Version";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(93, 370);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 4;
            this.btnClearLog.Text = "Clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // numTimerInterval
            // 
            this.numTimerInterval.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTimerInterval.Location = new System.Drawing.Point(103, 35);
            this.numTimerInterval.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numTimerInterval.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numTimerInterval.Name = "numTimerInterval";
            this.numTimerInterval.Size = new System.Drawing.Size(120, 20);
            this.numTimerInterval.TabIndex = 1;
            this.numTimerInterval.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Time";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colSrcMac
            // 
            this.colSrcMac.HeaderText = "Source MAC Address";
            this.colSrcMac.Name = "colSrcMac";
            this.colSrcMac.ReadOnly = true;
            this.colSrcMac.Width = 150;
            // 
            // colCIADDR
            // 
            this.colCIADDR.HeaderText = "Client Address";
            this.colCIADDR.Name = "colCIADDR";
            this.colCIADDR.ReadOnly = true;
            // 
            // colYIADDR
            // 
            this.colYIADDR.HeaderText = "Your Address";
            this.colYIADDR.Name = "colYIADDR";
            this.colYIADDR.ReadOnly = true;
            // 
            // colSIADDR
            // 
            this.colSIADDR.HeaderText = "Server Address";
            this.colSIADDR.Name = "colSIADDR";
            this.colSIADDR.ReadOnly = true;
            // 
            // colGIADDR
            // 
            this.colGIADDR.HeaderText = "Gateway Address";
            this.colGIADDR.Name = "colGIADDR";
            this.colGIADDR.ReadOnly = true;
            // 
            // colHost
            // 
            this.colHost.HeaderText = "Host Name";
            this.colHost.Name = "colHost";
            this.colHost.ReadOnly = true;
            // 
            // DHCPFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 405);
            this.Controls.Add(this.numTimerInterval);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtRealMac);
            this.Controls.Add(this.grdOutput);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.cmbInterfaces);
            this.Controls.Add(this.lblInterface);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Name = "DHCPFinderForm";
            this.Text = "DHCP Finder";
            this.Load += new System.EventHandler(this.DHCPFinder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.ComboBox cmbInterfaces;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.DataGridView grdOutput;
        private System.Windows.Forms.ToolTip toolTipTimer;
        private System.Windows.Forms.ToolTip toolTipRealMac;
        private System.Windows.Forms.TextBox txtRealMac;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.NumericUpDown numTimerInterval;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSrcMac;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCIADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYIADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSIADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGIADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHost;
    }
}

