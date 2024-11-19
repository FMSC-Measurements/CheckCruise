namespace CheckCruise
{
    partial class createCheckFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(createCheckFile));
            this.label1 = new System.Windows.Forms.Label();
            this.cruiseFilename = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addAllRecords = new System.Windows.Forms.Button();
            this.addOneRecord = new System.Windows.Forms.Button();
            this.removeOneRecord = new System.Windows.Forms.Button();
            this.removeAllRecords = new System.Windows.Forms.Button();
            this.originalCruise = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originalCruiseUnitsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkCruise = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkCruiseUnitsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.includeLogs = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.checkCruiserInitials = new System.Windows.Forms.TextBox();
            this.excludeMeasurements = new System.Windows.Forms.CheckBox();
            this.createFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.originalCruise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalCruiseUnitsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCruise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCruiseUnitsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter cruise filename:";
            // 
            // cruiseFilename
            // 
            this.cruiseFilename.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cruiseFilename.Location = new System.Drawing.Point(147, 11);
            this.cruiseFilename.Name = "cruiseFilename";
            this.cruiseFilename.Size = new System.Drawing.Size(244, 22);
            this.cruiseFilename.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.Location = new System.Drawing.Point(413, 8);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.onBrowse);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(105, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "From the Original Cruise list, select the cutting units to include in the check c" +
    "ruise file.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Original Cruise";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(409, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Check Cruise";
            // 
            // addAllRecords
            // 
            this.addAllRecords.Font = new System.Drawing.Font("Arial", 9.75F);
            this.addAllRecords.Location = new System.Drawing.Point(270, 120);
            this.addAllRecords.Name = "addAllRecords";
            this.addAllRecords.Size = new System.Drawing.Size(75, 23);
            this.addAllRecords.TabIndex = 8;
            this.addAllRecords.Text = "ALL -->";
            this.addAllRecords.UseVisualStyleBackColor = true;
            this.addAllRecords.Click += new System.EventHandler(this.onAddAll);
            // 
            // addOneRecord
            // 
            this.addOneRecord.Font = new System.Drawing.Font("Arial", 9.75F);
            this.addOneRecord.Location = new System.Drawing.Point(270, 151);
            this.addOneRecord.Name = "addOneRecord";
            this.addOneRecord.Size = new System.Drawing.Size(75, 23);
            this.addOneRecord.TabIndex = 9;
            this.addOneRecord.Text = "-->";
            this.addOneRecord.UseVisualStyleBackColor = true;
            this.addOneRecord.Click += new System.EventHandler(this.onAddOne);
            // 
            // removeOneRecord
            // 
            this.removeOneRecord.Font = new System.Drawing.Font("Arial", 9.75F);
            this.removeOneRecord.Location = new System.Drawing.Point(270, 217);
            this.removeOneRecord.Name = "removeOneRecord";
            this.removeOneRecord.Size = new System.Drawing.Size(75, 23);
            this.removeOneRecord.TabIndex = 10;
            this.removeOneRecord.Text = "<--";
            this.removeOneRecord.UseVisualStyleBackColor = true;
            this.removeOneRecord.Click += new System.EventHandler(this.onRemoveOne);
            // 
            // removeAllRecords
            // 
            this.removeAllRecords.Font = new System.Drawing.Font("Arial", 9.75F);
            this.removeAllRecords.Location = new System.Drawing.Point(270, 248);
            this.removeAllRecords.Name = "removeAllRecords";
            this.removeAllRecords.Size = new System.Drawing.Size(75, 23);
            this.removeAllRecords.TabIndex = 11;
            this.removeAllRecords.Text = "<-- ALL";
            this.removeAllRecords.UseVisualStyleBackColor = true;
            this.removeAllRecords.Click += new System.EventHandler(this.onRemoveAll);
            // 
            // originalCruise
            // 
            this.originalCruise.AllowUserToAddRows = false;
            this.originalCruise.AllowUserToDeleteRows = false;
            this.originalCruise.AutoGenerateColumns = false;
            this.originalCruise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.originalCruise.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.originalCruise.DataSource = this.originalCruiseUnitsBindingSource;
            this.originalCruise.Location = new System.Drawing.Point(15, 121);
            this.originalCruise.Name = "originalCruise";
            this.originalCruise.ReadOnly = true;
            this.originalCruise.Size = new System.Drawing.Size(235, 150);
            this.originalCruise.TabIndex = 12;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "OC_Method";
            this.dataGridViewTextBoxColumn1.HeaderText = "METHOD";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "OC_Stratum";
            this.dataGridViewTextBoxColumn2.HeaderText = "STRATUM";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 65;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "OC_Unit";
            this.dataGridViewTextBoxColumn3.HeaderText = "UNIT";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // originalCruiseUnitsBindingSource
            // 
            this.originalCruiseUnitsBindingSource.DataSource = typeof(CheckCruise.createCheckFile.OriginalCruiseUnits);
            // 
            // checkCruise
            // 
            this.checkCruise.AllowUserToAddRows = false;
            this.checkCruise.AutoGenerateColumns = false;
            this.checkCruise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.checkCruise.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.checkCruise.DataSource = this.checkCruiseUnitsBindingSource;
            this.checkCruise.Location = new System.Drawing.Point(363, 121);
            this.checkCruise.Name = "checkCruise";
            this.checkCruise.Size = new System.Drawing.Size(240, 150);
            this.checkCruise.TabIndex = 13;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CC_Method";
            this.dataGridViewTextBoxColumn4.HeaderText = "METHOD";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CC_Stratum";
            this.dataGridViewTextBoxColumn5.HeaderText = "STRATUM";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 65;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "CC_Unit";
            this.dataGridViewTextBoxColumn6.HeaderText = "UNIT";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // checkCruiseUnitsBindingSource
            // 
            this.checkCruiseUnitsBindingSource.DataSource = typeof(CheckCruise.createCheckFile.CheckCruiseUnits);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label5.Location = new System.Drawing.Point(15, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 37);
            this.label5.TabIndex = 14;
            this.label5.Text = "Click the box below to include all log records in the check file.";
            // 
            // includeLogs
            // 
            this.includeLogs.AutoSize = true;
            this.includeLogs.Font = new System.Drawing.Font("Arial", 9.75F);
            this.includeLogs.Location = new System.Drawing.Point(18, 326);
            this.includeLogs.Name = "includeLogs";
            this.includeLogs.Size = new System.Drawing.Size(139, 20);
            this.includeLogs.TabIndex = 15;
            this.includeLogs.Text = "Include Log records";
            this.includeLogs.UseVisualStyleBackColor = true;
            this.includeLogs.CheckedChanged += new System.EventHandler(this.onIncludeLogs);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label6.Location = new System.Drawing.Point(387, 285);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(216, 85);
            this.label6.TabIndex = 16;
            this.label6.Text = "Please enter the check cruiser initials.  These are used to complete the check cr" +
    "uise later.  Be sure to record them when collecting check cruise data.";
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Arial", 9.75F);
            this.exitButton.Location = new System.Drawing.Point(490, 414);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 17;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.onExit);
            // 
            // checkCruiserInitials
            // 
            this.checkCruiserInitials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkCruiserInitials.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkCruiserInitials.Location = new System.Drawing.Point(454, 373);
            this.checkCruiserInitials.Name = "checkCruiserInitials";
            this.checkCruiserInitials.Size = new System.Drawing.Size(54, 22);
            this.checkCruiserInitials.TabIndex = 18;
            // 
            // excludeMeasurements
            // 
            this.excludeMeasurements.AutoSize = true;
            this.excludeMeasurements.Font = new System.Drawing.Font("Arial", 9.75F);
            this.excludeMeasurements.Location = new System.Drawing.Point(18, 375);
            this.excludeMeasurements.Name = "excludeMeasurements";
            this.excludeMeasurements.Size = new System.Drawing.Size(239, 20);
            this.excludeMeasurements.TabIndex = 19;
            this.excludeMeasurements.Text = "Exclude cruiser tree measurements?";
            this.excludeMeasurements.UseVisualStyleBackColor = true;
            this.excludeMeasurements.CheckedChanged += new System.EventHandler(this.onExcludeMeasurements);
            // 
            // createFileButton
            // 
            this.createFileButton.AutoSize = true;
            this.createFileButton.Font = new System.Drawing.Font("Arial", 9.75F);
            this.createFileButton.Location = new System.Drawing.Point(190, 411);
            this.createFileButton.Name = "createFileButton";
            this.createFileButton.Size = new System.Drawing.Size(201, 26);
            this.createFileButton.TabIndex = 20;
            this.createFileButton.Text = "CREATE CHECK CRUISE FILE";
            this.createFileButton.UseVisualStyleBackColor = true;
            this.createFileButton.Click += new System.EventHandler(this.onCreateFile);
            // 
            // createCheckFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 449);
            this.Controls.Add(this.createFileButton);
            this.Controls.Add(this.excludeMeasurements);
            this.Controls.Add(this.checkCruiserInitials);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.includeLogs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkCruise);
            this.Controls.Add(this.originalCruise);
            this.Controls.Add(this.removeAllRecords);
            this.Controls.Add(this.removeOneRecord);
            this.Controls.Add(this.addOneRecord);
            this.Controls.Add(this.addAllRecords);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.cruiseFilename);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "createCheckFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create a Check Cruise File";
            ((System.ComponentModel.ISupportInitialize)(this.originalCruise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalCruiseUnitsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCruise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCruiseUnitsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cruiseFilename;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addAllRecords;
        private System.Windows.Forms.Button addOneRecord;
        private System.Windows.Forms.Button removeOneRecord;
        private System.Windows.Forms.Button removeAllRecords;
        private System.Windows.Forms.DataGridView originalCruise;
        private System.Windows.Forms.DataGridView checkCruise;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox includeLogs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox checkCruiserInitials;
        private System.Windows.Forms.CheckBox excludeMeasurements;
        private System.Windows.Forms.Button createFileButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.BindingSource originalCruiseUnitsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.BindingSource checkCruiseUnitsBindingSource;
    }
}