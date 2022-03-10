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
            this.label1 = new System.Windows.Forms.Label();
            this.checkCruiseFilename = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
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
            this.label2 = new System.Windows.Forms.Label();
            this.multipleFileBrowse = new System.Windows.Forms.Button();
            this.multipleSalesFileName = new System.Windows.Forms.RichTextBox();
            this.createByCruiserMultipleSales = new System.Windows.Forms.CheckBox();
            this.createByCruiserSingleSale = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cruiserInitials = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(52, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(534, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a file for Check Cruise Evaluation Report and/or Summary Reports for sing" +
                "le sale.";
            // 
            // checkCruiseFilename
            // 
            this.checkCruiseFilename.Location = new System.Drawing.Point(130, 54);
            this.checkCruiseFilename.Name = "checkCruiseFilename";
            this.checkCruiseFilename.Size = new System.Drawing.Size(267, 22);
            this.checkCruiseFilename.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(433, 52);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.onBrowse);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.reportByCruiser);
            this.groupBox1.Controls.Add(this.reportBySale);
            this.groupBox1.Controls.Add(this.createEvaluationReport);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(124, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 118);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CHECK CRUISE EVALUATION REPORT";
            // 
            // reportByCruiser
            // 
            this.reportByCruiser.AutoSize = true;
            this.reportByCruiser.Location = new System.Drawing.Point(239, 31);
            this.reportByCruiser.Name = "reportByCruiser";
            this.reportByCruiser.Size = new System.Drawing.Size(91, 20);
            this.reportByCruiser.TabIndex = 5;
            this.reportByCruiser.TabStop = true;
            this.reportByCruiser.Text = "By Cruiser";
            this.reportByCruiser.UseVisualStyleBackColor = true;
            this.reportByCruiser.Click += new System.EventHandler(this.onReportByCruiser);
            // 
            // reportBySale
            // 
            this.reportBySale.AutoSize = true;
            this.reportBySale.Location = new System.Drawing.Point(65, 31);
            this.reportBySale.Name = "reportBySale";
            this.reportBySale.Size = new System.Drawing.Size(75, 20);
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
            this.createEvaluationReport.Location = new System.Drawing.Point(37, 74);
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
            this.cruiserName.Location = new System.Drawing.Point(396, 121);
            this.cruiserName.Name = "cruiserName";
            this.cruiserName.Size = new System.Drawing.Size(112, 22);
            this.cruiserName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(303, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cruiser Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label3.Location = new System.Drawing.Point(147, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
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
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.multipleFileBrowse);
            this.groupBox2.Controls.Add(this.multipleSalesFileName);
            this.groupBox2.Controls.Add(this.createByCruiserMultipleSales);
            this.groupBox2.Controls.Add(this.createByCruiserSingleSale);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(22, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(594, 273);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SUMMARY REPORTS";
            // 
            // createSummaryByCruiser
            // 
            this.createSummaryByCruiser.AutoSize = true;
            this.createSummaryByCruiser.Location = new System.Drawing.Point(295, 25);
            this.createSummaryByCruiser.Name = "createSummaryByCruiser";
            this.createSummaryByCruiser.Size = new System.Drawing.Size(91, 20);
            this.createSummaryByCruiser.TabIndex = 9;
            this.createSummaryByCruiser.TabStop = true;
            this.createSummaryByCruiser.Text = "By Cruiser";
            this.createSummaryByCruiser.UseVisualStyleBackColor = true;
            this.createSummaryByCruiser.Click += new System.EventHandler(this.onSummaryByCruiser);
            // 
            // createSummaryBySale
            // 
            this.createSummaryBySale.AutoSize = true;
            this.createSummaryBySale.Location = new System.Drawing.Point(102, 85);
            this.createSummaryBySale.Name = "createSummaryBySale";
            this.createSummaryBySale.Size = new System.Drawing.Size(75, 20);
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
            this.createSummaryReports.Location = new System.Drawing.Point(142, 229);
            this.createSummaryReports.Name = "createSummaryReports";
            this.createSummaryReports.Size = new System.Drawing.Size(273, 26);
            this.createSummaryReports.TabIndex = 7;
            this.createSummaryReports.Text = "CREATE SUMMARY REPORTS";
            this.createSummaryReports.UseVisualStyleBackColor = true;
            this.createSummaryReports.Click += new System.EventHandler(this.onCreateSummaryReports);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(292, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Add files for multiple sales report";
            // 
            // multipleFileBrowse
            // 
            this.multipleFileBrowse.Font = new System.Drawing.Font("Arial", 9.75F);
            this.multipleFileBrowse.Location = new System.Drawing.Point(495, 119);
            this.multipleFileBrowse.Name = "multipleFileBrowse";
            this.multipleFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.multipleFileBrowse.TabIndex = 5;
            this.multipleFileBrowse.Text = "Browse...";
            this.multipleFileBrowse.UseVisualStyleBackColor = true;
            this.multipleFileBrowse.Click += new System.EventHandler(this.onMultipleBrowse);
            // 
            // multipleSalesFileName
            // 
            this.multipleSalesFileName.Location = new System.Drawing.Point(196, 148);
            this.multipleSalesFileName.Name = "multipleSalesFileName";
            this.multipleSalesFileName.Size = new System.Drawing.Size(374, 63);
            this.multipleSalesFileName.TabIndex = 6;
            this.multipleSalesFileName.Text = "";
            // 
            // createByCruiserMultipleSales
            // 
            this.createByCruiserMultipleSales.AutoSize = true;
            this.createByCruiserMultipleSales.Font = new System.Drawing.Font("Arial", 9.75F);
            this.createByCruiserMultipleSales.Location = new System.Drawing.Point(341, 86);
            this.createByCruiserMultipleSales.Name = "createByCruiserMultipleSales";
            this.createByCruiserMultipleSales.Size = new System.Drawing.Size(109, 20);
            this.createByCruiserMultipleSales.TabIndex = 4;
            this.createByCruiserMultipleSales.Text = "Multiple Sales";
            this.createByCruiserMultipleSales.UseVisualStyleBackColor = true;
            this.createByCruiserMultipleSales.Click += new System.EventHandler(this.oCreateMultipleSale);
            // 
            // createByCruiserSingleSale
            // 
            this.createByCruiserSingleSale.AutoSize = true;
            this.createByCruiserSingleSale.Font = new System.Drawing.Font("Arial", 9.75F);
            this.createByCruiserSingleSale.Location = new System.Drawing.Point(341, 51);
            this.createByCruiserSingleSale.Name = "createByCruiserSingleSale";
            this.createByCruiserSingleSale.Size = new System.Drawing.Size(93, 20);
            this.createByCruiserSingleSale.TabIndex = 3;
            this.createByCruiserSingleSale.Text = "Single Sale";
            this.createByCruiserSingleSale.UseVisualStyleBackColor = true;
            this.createByCruiserSingleSale.Click += new System.EventHandler(this.onCreateSingleSale);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(26, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(586, 22);
            this.label7.TabIndex = 8;
            this.label7.Text = "If reports are to be by cruiser, enter the cruiser initials and name below.  Crui" +
                "ser name is optional.";
            // 
            // cruiserInitials
            // 
            this.cruiserInitials.FormattingEnabled = true;
            this.cruiserInitials.Location = new System.Drawing.Point(243, 119);
            this.cruiserInitials.Name = "cruiserInitials";
            this.cruiserInitials.Size = new System.Drawing.Size(46, 24);
            this.cruiserInitials.TabIndex = 9;
            // 
            // CheckCruiseReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 648);
            this.Controls.Add(this.cruiserInitials);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cruiserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.checkCruiseFilename);
            this.Controls.Add(this.label1);
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox checkCruiseFilename;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createEvaluationReport;
        private System.Windows.Forms.TextBox cruiserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button multipleFileBrowse;
        private System.Windows.Forms.RichTextBox multipleSalesFileName;
        private System.Windows.Forms.CheckBox createByCruiserMultipleSales;
        private System.Windows.Forms.CheckBox createByCruiserSingleSale;
        private System.Windows.Forms.Button createSummaryReports;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton reportByCruiser;
        private System.Windows.Forms.RadioButton reportBySale;
        private System.Windows.Forms.RadioButton createSummaryByCruiser;
        private System.Windows.Forms.RadioButton createSummaryBySale;
        private System.Windows.Forms.ComboBox cruiserInitials;
    }
}