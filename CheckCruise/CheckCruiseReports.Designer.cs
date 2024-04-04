namespace CheckCruise
{
    partial class CheckCruiseReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckCruiseReports));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reportByCruiser = new System.Windows.Forms.RadioButton();
            this.reportBySale = new System.Windows.Forms.RadioButton();
            this.createEvaluationReport = new System.Windows.Forms.Button();
            this.cruiserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.createSummaryByCruiser = new System.Windows.Forms.RadioButton();
            this.createSummaryBySale = new System.Windows.Forms.RadioButton();
            this.createSummaryReports = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cruiserInitials = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.reportByCruiser);
            this.groupBox1.Controls.Add(this.reportBySale);
            this.groupBox1.Controls.Add(this.createEvaluationReport);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(27, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 118);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CHECK CRUISE EVALUATION REPORT";
            // 
            // reportByCruiser
            // 
            this.reportByCruiser.AutoSize = true;
            this.reportByCruiser.Location = new System.Drawing.Point(121, 31);
            this.reportByCruiser.Name = "reportByCruiser";
            this.reportByCruiser.Size = new System.Drawing.Size(90, 20);
            this.reportByCruiser.TabIndex = 5;
            this.reportByCruiser.TabStop = true;
            this.reportByCruiser.Text = "By Cruiser";
            this.reportByCruiser.UseVisualStyleBackColor = true;
            this.reportByCruiser.Click += new System.EventHandler(this.onReportByCruiser);
            // 
            // reportBySale
            // 
            this.reportBySale.AutoSize = true;
            this.reportBySale.Location = new System.Drawing.Point(26, 31);
            this.reportBySale.Name = "reportBySale";
            this.reportBySale.Size = new System.Drawing.Size(74, 20);
            this.reportBySale.TabIndex = 4;
            this.reportBySale.TabStop = true;
            this.reportBySale.Text = "By Sale";
            this.reportBySale.UseVisualStyleBackColor = true;
            this.reportBySale.Click += new System.EventHandler(this.onReportBySale);
            // 
            // createEvaluationReport
            // 
            this.createEvaluationReport.AutoSize = true;
            this.createEvaluationReport.Font = new System.Drawing.Font("Arial", 9.75F);
            this.createEvaluationReport.Location = new System.Drawing.Point(133, 86);
            this.createEvaluationReport.Name = "createEvaluationReport";
            this.createEvaluationReport.Size = new System.Drawing.Size(319, 26);
            this.createEvaluationReport.TabIndex = 3;
            this.createEvaluationReport.Text = "CREATE CHECK CRUISER EVALUATION REPORT";
            this.createEvaluationReport.UseVisualStyleBackColor = true;
            this.createEvaluationReport.Click += new System.EventHandler(this.onEvaluationReport);
            // 
            // cruiserName
            // 
            this.cruiserName.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cruiserName.Location = new System.Drawing.Point(394, 34);
            this.cruiserName.Name = "cruiserName";
            this.cruiserName.Size = new System.Drawing.Size(112, 22);
            this.cruiserName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(301, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cruiser Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label3.Location = new System.Drawing.Point(145, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cruiser Initials";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(537, 607);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.onExit);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.createSummaryByCruiser);
            this.groupBox2.Controls.Add(this.createSummaryBySale);
            this.groupBox2.Controls.Add(this.createSummaryReports);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(20, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(594, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SUMMARY REPORTS";
            // 
            // createSummaryByCruiser
            // 
            this.createSummaryByCruiser.AutoSize = true;
            this.createSummaryByCruiser.Location = new System.Drawing.Point(128, 25);
            this.createSummaryByCruiser.Name = "createSummaryByCruiser";
            this.createSummaryByCruiser.Size = new System.Drawing.Size(90, 20);
            this.createSummaryByCruiser.TabIndex = 9;
            this.createSummaryByCruiser.TabStop = true;
            this.createSummaryByCruiser.Text = "By Cruiser";
            this.createSummaryByCruiser.UseVisualStyleBackColor = true;
            this.createSummaryByCruiser.Click += new System.EventHandler(this.onSummaryByCruiser);
            // 
            // createSummaryBySale
            // 
            this.createSummaryBySale.AutoSize = true;
            this.createSummaryBySale.Location = new System.Drawing.Point(33, 25);
            this.createSummaryBySale.Name = "createSummaryBySale";
            this.createSummaryBySale.Size = new System.Drawing.Size(74, 20);
            this.createSummaryBySale.TabIndex = 8;
            this.createSummaryBySale.TabStop = true;
            this.createSummaryBySale.Text = "By Sale";
            this.createSummaryBySale.UseVisualStyleBackColor = true;
            this.createSummaryBySale.Click += new System.EventHandler(this.onSummaryBySale);
            // 
            // createSummaryReports
            // 
            this.createSummaryReports.AutoSize = true;
            this.createSummaryReports.Font = new System.Drawing.Font("Arial", 9.75F);
            this.createSummaryReports.Location = new System.Drawing.Point(161, 68);
            this.createSummaryReports.Name = "createSummaryReports";
            this.createSummaryReports.Size = new System.Drawing.Size(273, 26);
            this.createSummaryReports.TabIndex = 7;
            this.createSummaryReports.Text = "CREATE SUMMARY REPORTS";
            this.createSummaryReports.UseVisualStyleBackColor = true;
            this.createSummaryReports.Click += new System.EventHandler(this.onCreateSummaryReports);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(586, 22);
            this.label7.TabIndex = 8;
            this.label7.Text = "If reports are to be by cruiser, enter the cruiser initials and name below.  Crui" +
    "ser name is optional.";
            // 
            // cruiserInitials
            // 
            this.cruiserInitials.FormattingEnabled = true;
            this.cruiserInitials.Location = new System.Drawing.Point(241, 32);
            this.cruiserInitials.Name = "cruiserInitials";
            this.cruiserInitials.Size = new System.Drawing.Size(46, 24);
            this.cruiserInitials.TabIndex = 9;
            // 
            // CheckCruiseReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 344);
            this.Controls.Add(this.cruiserInitials);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cruiserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CheckCruiseReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check Cruise Reports";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createEvaluationReport;
        private System.Windows.Forms.TextBox cruiserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button createSummaryReports;
        private System.Windows.Forms.RadioButton reportByCruiser;
        private System.Windows.Forms.RadioButton reportBySale;
        private System.Windows.Forms.RadioButton createSummaryByCruiser;
        private System.Windows.Forms.RadioButton createSummaryBySale;
        private System.Windows.Forms.ComboBox cruiserInitials;
    }
}