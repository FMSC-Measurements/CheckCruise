namespace CheckCruise
{
    partial class RegionalTolerances
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegionalTolerances));
            this.toleranceData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.exitBurron = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PrimarySecondaryNetVol = new System.Windows.Forms.CheckBox();
            this.PrimarySecondaryGrossVol = new System.Windows.Forms.CheckBox();
            this.SecondaryNetVol = new System.Windows.Forms.CheckBox();
            this.SecondaryGrossVol = new System.Windows.Forms.CheckBox();
            this.PrimaryNetVol = new System.Windows.Forms.CheckBox();
            this.PrimaryGrossVol = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BoardVolume = new System.Windows.Forms.CheckBox();
            this.CubicVolume = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PrimarySecondaryNetPC = new System.Windows.Forms.TextBox();
            this.PrimarySecondaryGrossPC = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SecondaryNetPC = new System.Windows.Forms.TextBox();
            this.SecondaryGrossPC = new System.Windows.Forms.TextBox();
            this.PrimaryNetPC = new System.Windows.Forms.TextBox();
            this.PrimaryGrossPC = new System.Windows.Forms.TextBox();
            this.volumeByProduct = new System.Windows.Forms.CheckBox();
            this.volumeBySpecies = new System.Windows.Forms.CheckBox();
            this.includeVolume = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.overallAccuracy = new System.Windows.Forms.TextBox();
            this.individualAccuracy = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.currentRegion = new System.Windows.Forms.TextBox();
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.tElementDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tToleranceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tUnitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tAddParamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tWeightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tBySpeciesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tByProductDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tElementAccuracyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tOverallAccuracyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tDateStampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tolerancesListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.toleranceData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tolerancesListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toleranceData
            // 
            this.toleranceData.AutoGenerateColumns = false;
            this.toleranceData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.toleranceData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tElementDataGridViewTextBoxColumn,
            this.tToleranceDataGridViewTextBoxColumn,
            this.tUnitsDataGridViewTextBoxColumn,
            this.tAddParamDataGridViewTextBoxColumn,
            this.tWeightDataGridViewTextBoxColumn,
            this.tBySpeciesDataGridViewTextBoxColumn,
            this.tByProductDataGridViewTextBoxColumn,
            this.tElementAccuracyDataGridViewTextBoxColumn,
            this.tOverallAccuracyDataGridViewTextBoxColumn,
            this.tDateStampDataGridViewTextBoxColumn});
            this.toleranceData.DataSource = this.tolerancesListBindingSource;
            this.toleranceData.Location = new System.Drawing.Point(30, 38);
            this.toleranceData.Name = "toleranceData";
            this.toleranceData.ReadOnly = true;
            this.toleranceData.Size = new System.Drawing.Size(529, 178);
            this.toleranceData.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Region for this cruise is:";
            // 
            // deleteButton
            // 
            this.deleteButton.AutoSize = true;
            this.deleteButton.Font = new System.Drawing.Font("Arial", 9.75F);
            this.deleteButton.Location = new System.Drawing.Point(444, 231);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(114, 26);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "DELETE ROW";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.onDeleteRow);
            // 
            // exitBurron
            // 
            this.exitBurron.Font = new System.Drawing.Font("Arial", 9.75F);
            this.exitBurron.Location = new System.Drawing.Point(483, 586);
            this.exitBurron.Name = "exitBurron";
            this.exitBurron.Size = new System.Drawing.Size(75, 23);
            this.exitBurron.TabIndex = 4;
            this.exitBurron.Text = "EXIT";
            this.exitBurron.UseVisualStyleBackColor = true;
            this.exitBurron.Click += new System.EventHandler(this.onExit);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.PrimarySecondaryNetVol);
            this.groupBox1.Controls.Add(this.PrimarySecondaryGrossVol);
            this.groupBox1.Controls.Add(this.SecondaryNetVol);
            this.groupBox1.Controls.Add(this.SecondaryGrossVol);
            this.groupBox1.Controls.Add(this.PrimaryNetVol);
            this.groupBox1.Controls.Add(this.PrimaryGrossVol);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.BoardVolume);
            this.groupBox1.Controls.Add(this.CubicVolume);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.PrimarySecondaryNetPC);
            this.groupBox1.Controls.Add(this.PrimarySecondaryGrossPC);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.SecondaryNetPC);
            this.groupBox1.Controls.Add(this.SecondaryGrossPC);
            this.groupBox1.Controls.Add(this.PrimaryNetPC);
            this.groupBox1.Controls.Add(this.PrimaryGrossPC);
            this.groupBox1.Controls.Add(this.volumeByProduct);
            this.groupBox1.Controls.Add(this.volumeBySpecies);
            this.groupBox1.Controls.Add(this.includeVolume);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.groupBox1.Location = new System.Drawing.Point(12, 273);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 222);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volume included ";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(10, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 33);
            this.label9.TabIndex = 41;
            this.label9.Text = "Primary + Secondary Product";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(356, 16);
            this.label7.TabIndex = 40;
            this.label7.Text = "---------------------------------------------------------------------------------" +
                "------";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 16);
            this.label6.TabIndex = 39;
            this.label6.Text = "Secondary Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(356, 16);
            this.label5.TabIndex = 38;
            this.label5.Text = "---------------------------------------------------------------------------------" +
                "------";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 37;
            this.label4.Text = "Primary Product";
            // 
            // PrimarySecondaryNetVol
            // 
            this.PrimarySecondaryNetVol.AutoSize = true;
            this.PrimarySecondaryNetVol.Location = new System.Drawing.Point(342, 182);
            this.PrimarySecondaryNetVol.Name = "PrimarySecondaryNetVol";
            this.PrimarySecondaryNetVol.Size = new System.Drawing.Size(47, 20);
            this.PrimarySecondaryNetVol.TabIndex = 36;
            this.PrimarySecondaryNetVol.Text = "Net";
            this.PrimarySecondaryNetVol.UseVisualStyleBackColor = true;
            this.PrimarySecondaryNetVol.CheckedChanged += new System.EventHandler(this.onPrimarySecondaryNetVol);
            // 
            // PrimarySecondaryGrossVol
            // 
            this.PrimarySecondaryGrossVol.AutoSize = true;
            this.PrimarySecondaryGrossVol.Location = new System.Drawing.Point(142, 179);
            this.PrimarySecondaryGrossVol.Name = "PrimarySecondaryGrossVol";
            this.PrimarySecondaryGrossVol.Size = new System.Drawing.Size(62, 20);
            this.PrimarySecondaryGrossVol.TabIndex = 35;
            this.PrimarySecondaryGrossVol.Text = "Gross";
            this.PrimarySecondaryGrossVol.UseVisualStyleBackColor = true;
            this.PrimarySecondaryGrossVol.CheckedChanged += new System.EventHandler(this.onPrimarySecondaryGrossVol);
            // 
            // SecondaryNetVol
            // 
            this.SecondaryNetVol.AutoSize = true;
            this.SecondaryNetVol.Location = new System.Drawing.Point(342, 141);
            this.SecondaryNetVol.Name = "SecondaryNetVol";
            this.SecondaryNetVol.Size = new System.Drawing.Size(47, 20);
            this.SecondaryNetVol.TabIndex = 34;
            this.SecondaryNetVol.Text = "Net";
            this.SecondaryNetVol.UseVisualStyleBackColor = true;
            this.SecondaryNetVol.CheckedChanged += new System.EventHandler(this.onSecondaryNetVol);
            // 
            // SecondaryGrossVol
            // 
            this.SecondaryGrossVol.AutoSize = true;
            this.SecondaryGrossVol.Location = new System.Drawing.Point(142, 141);
            this.SecondaryGrossVol.Name = "SecondaryGrossVol";
            this.SecondaryGrossVol.Size = new System.Drawing.Size(62, 20);
            this.SecondaryGrossVol.TabIndex = 33;
            this.SecondaryGrossVol.Text = "Gross";
            this.SecondaryGrossVol.UseVisualStyleBackColor = true;
            this.SecondaryGrossVol.CheckedChanged += new System.EventHandler(this.onSecondaryGrossVol);
            // 
            // PrimaryNetVol
            // 
            this.PrimaryNetVol.AutoSize = true;
            this.PrimaryNetVol.Location = new System.Drawing.Point(344, 99);
            this.PrimaryNetVol.Name = "PrimaryNetVol";
            this.PrimaryNetVol.Size = new System.Drawing.Size(47, 20);
            this.PrimaryNetVol.TabIndex = 32;
            this.PrimaryNetVol.Text = "Net";
            this.PrimaryNetVol.UseVisualStyleBackColor = true;
            this.PrimaryNetVol.CheckedChanged += new System.EventHandler(this.onPrimaryNetVol);
            // 
            // PrimaryGrossVol
            // 
            this.PrimaryGrossVol.AutoSize = true;
            this.PrimaryGrossVol.Location = new System.Drawing.Point(143, 100);
            this.PrimaryGrossVol.Name = "PrimaryGrossVol";
            this.PrimaryGrossVol.Size = new System.Drawing.Size(62, 20);
            this.PrimaryGrossVol.TabIndex = 31;
            this.PrimaryGrossVol.Text = "Gross";
            this.PrimaryGrossVol.UseVisualStyleBackColor = true;
            this.PrimaryGrossVol.CheckedChanged += new System.EventHandler(this.onPrimaryGrossVol);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(201, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 16);
            this.label8.TabIndex = 30;
            this.label8.Text = "Select type of volume";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BoardVolume
            // 
            this.BoardVolume.AutoSize = true;
            this.BoardVolume.Location = new System.Drawing.Point(280, 61);
            this.BoardVolume.Name = "BoardVolume";
            this.BoardVolume.Size = new System.Drawing.Size(60, 20);
            this.BoardVolume.TabIndex = 29;
            this.BoardVolume.Text = "BDFT";
            this.BoardVolume.UseVisualStyleBackColor = true;
            // 
            // CubicVolume
            // 
            this.CubicVolume.AutoSize = true;
            this.CubicVolume.Location = new System.Drawing.Point(188, 61);
            this.CubicVolume.Name = "CubicVolume";
            this.CubicVolume.Size = new System.Drawing.Size(60, 20);
            this.CubicVolume.TabIndex = 28;
            this.CubicVolume.Text = "CUFT";
            this.CubicVolume.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "percent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "percent";
            // 
            // PrimarySecondaryNetPC
            // 
            this.PrimarySecondaryNetPC.Location = new System.Drawing.Point(395, 177);
            this.PrimarySecondaryNetPC.Name = "PrimarySecondaryNetPC";
            this.PrimarySecondaryNetPC.Size = new System.Drawing.Size(37, 22);
            this.PrimarySecondaryNetPC.TabIndex = 25;
            // 
            // PrimarySecondaryGrossPC
            // 
            this.PrimarySecondaryGrossPC.Location = new System.Drawing.Point(211, 177);
            this.PrimarySecondaryGrossPC.Name = "PrimarySecondaryGrossPC";
            this.PrimarySecondaryGrossPC.Size = new System.Drawing.Size(37, 22);
            this.PrimarySecondaryGrossPC.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(438, 103);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 16);
            this.label14.TabIndex = 19;
            this.label14.Text = "percent";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(254, 145);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 16);
            this.label13.TabIndex = 18;
            this.label13.Text = "percent";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(438, 145);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 16);
            this.label12.TabIndex = 17;
            this.label12.Text = "percent";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(254, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 16);
            this.label11.TabIndex = 16;
            this.label11.Text = "percent";
            // 
            // SecondaryNetPC
            // 
            this.SecondaryNetPC.Location = new System.Drawing.Point(395, 139);
            this.SecondaryNetPC.Name = "SecondaryNetPC";
            this.SecondaryNetPC.Size = new System.Drawing.Size(37, 22);
            this.SecondaryNetPC.TabIndex = 15;
            // 
            // SecondaryGrossPC
            // 
            this.SecondaryGrossPC.Location = new System.Drawing.Point(211, 139);
            this.SecondaryGrossPC.Name = "SecondaryGrossPC";
            this.SecondaryGrossPC.Size = new System.Drawing.Size(37, 22);
            this.SecondaryGrossPC.TabIndex = 14;
            // 
            // PrimaryNetPC
            // 
            this.PrimaryNetPC.Location = new System.Drawing.Point(395, 97);
            this.PrimaryNetPC.Name = "PrimaryNetPC";
            this.PrimaryNetPC.Size = new System.Drawing.Size(37, 22);
            this.PrimaryNetPC.TabIndex = 13;
            // 
            // PrimaryGrossPC
            // 
            this.PrimaryGrossPC.Location = new System.Drawing.Point(211, 97);
            this.PrimaryGrossPC.Name = "PrimaryGrossPC";
            this.PrimaryGrossPC.Size = new System.Drawing.Size(37, 22);
            this.PrimaryGrossPC.TabIndex = 12;
            // 
            // volumeByProduct
            // 
            this.volumeByProduct.AutoSize = true;
            this.volumeByProduct.Location = new System.Drawing.Point(432, 19);
            this.volumeByProduct.Name = "volumeByProduct";
            this.volumeByProduct.Size = new System.Drawing.Size(90, 20);
            this.volumeByProduct.TabIndex = 2;
            this.volumeByProduct.Text = "By product";
            this.volumeByProduct.UseVisualStyleBackColor = true;
            // 
            // volumeBySpecies
            // 
            this.volumeBySpecies.AutoSize = true;
            this.volumeBySpecies.Location = new System.Drawing.Point(337, 19);
            this.volumeBySpecies.Name = "volumeBySpecies";
            this.volumeBySpecies.Size = new System.Drawing.Size(92, 20);
            this.volumeBySpecies.TabIndex = 1;
            this.volumeBySpecies.Text = "By species";
            this.volumeBySpecies.UseVisualStyleBackColor = true;
            // 
            // includeVolume
            // 
            this.includeVolume.AutoSize = true;
            this.includeVolume.Location = new System.Drawing.Point(12, 19);
            this.includeVolume.Name = "includeVolume";
            this.includeVolume.Size = new System.Drawing.Size(281, 20);
            this.includeVolume.TabIndex = 0;
            this.includeVolume.Text = "Include volume in determination of Pass/Fail";
            this.includeVolume.UseVisualStyleBackColor = true;
            this.includeVolume.CheckedChanged += new System.EventHandler(this.onIncludeVolume);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(362, 418);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(164, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "Primary _ Secondary Product Net";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.overallAccuracy);
            this.groupBox2.Controls.Add(this.individualAccuracy);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.groupBox2.Location = new System.Drawing.Point(11, 508);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 101);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Regional Requirements for Pass/Fail";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(366, 69);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(51, 16);
            this.label26.TabIndex = 23;
            this.label26.Text = "percent";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(366, 27);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(51, 16);
            this.label25.TabIndex = 22;
            this.label25.Text = "percent";
            // 
            // overallAccuracy
            // 
            this.overallAccuracy.Location = new System.Drawing.Point(323, 66);
            this.overallAccuracy.Name = "overallAccuracy";
            this.overallAccuracy.Size = new System.Drawing.Size(37, 22);
            this.overallAccuracy.TabIndex = 3;
            // 
            // individualAccuracy
            // 
            this.individualAccuracy.Location = new System.Drawing.Point(323, 24);
            this.individualAccuracy.Name = "individualAccuracy";
            this.individualAccuracy.Size = new System.Drawing.Size(37, 22);
            this.individualAccuracy.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(20, 69);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(195, 16);
            this.label24.TabIndex = 1;
            this.label24.Text = "Overall accuracy score (at least)";
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(20, 24);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(297, 37);
            this.label23.TabIndex = 0;
            this.label23.Text = "Overall accuracy score for each individual element (at least)";
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cancelButton.Location = new System.Drawing.Point(482, 532);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.onCancel);
            // 
            // currentRegion
            // 
            this.currentRegion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.currentRegion.Location = new System.Drawing.Point(167, 15);
            this.currentRegion.Name = "currentRegion";
            this.currentRegion.ReadOnly = true;
            this.currentRegion.Size = new System.Drawing.Size(37, 13);
            this.currentRegion.TabIndex = 8;
            // 
            // editButton
            // 
            this.editButton.AutoSize = true;
            this.editButton.Font = new System.Drawing.Font("Arial", 9.75F);
            this.editButton.Location = new System.Drawing.Point(231, 231);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(114, 26);
            this.editButton.TabIndex = 9;
            this.editButton.Text = "EDIT ROW";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.onEdit);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(30, 231);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(114, 26);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "ADD ROW";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.onAdd);
            // 
            // tElementDataGridViewTextBoxColumn
            // 
            this.tElementDataGridViewTextBoxColumn.DataPropertyName = "T_Element";
            this.tElementDataGridViewTextBoxColumn.HeaderText = "ELEMENT";
            this.tElementDataGridViewTextBoxColumn.Name = "tElementDataGridViewTextBoxColumn";
            this.tElementDataGridViewTextBoxColumn.ReadOnly = true;
            this.tElementDataGridViewTextBoxColumn.Width = 150;
            // 
            // tToleranceDataGridViewTextBoxColumn
            // 
            this.tToleranceDataGridViewTextBoxColumn.DataPropertyName = "T_Tolerance";
            this.tToleranceDataGridViewTextBoxColumn.HeaderText = "TOLERANCE";
            this.tToleranceDataGridViewTextBoxColumn.Name = "tToleranceDataGridViewTextBoxColumn";
            this.tToleranceDataGridViewTextBoxColumn.ReadOnly = true;
            this.tToleranceDataGridViewTextBoxColumn.Width = 75;
            // 
            // tUnitsDataGridViewTextBoxColumn
            // 
            this.tUnitsDataGridViewTextBoxColumn.DataPropertyName = "T_Units";
            this.tUnitsDataGridViewTextBoxColumn.HeaderText = "UNITS";
            this.tUnitsDataGridViewTextBoxColumn.Name = "tUnitsDataGridViewTextBoxColumn";
            this.tUnitsDataGridViewTextBoxColumn.ReadOnly = true;
            this.tUnitsDataGridViewTextBoxColumn.Width = 55;
            // 
            // tAddParamDataGridViewTextBoxColumn
            // 
            this.tAddParamDataGridViewTextBoxColumn.DataPropertyName = "T_AddParam";
            this.tAddParamDataGridViewTextBoxColumn.HeaderText = "ADDITIONAL PARAMETER";
            this.tAddParamDataGridViewTextBoxColumn.Name = "tAddParamDataGridViewTextBoxColumn";
            this.tAddParamDataGridViewTextBoxColumn.ReadOnly = true;
            this.tAddParamDataGridViewTextBoxColumn.Width = 135;
            // 
            // tWeightDataGridViewTextBoxColumn
            // 
            this.tWeightDataGridViewTextBoxColumn.DataPropertyName = "T_Weight";
            this.tWeightDataGridViewTextBoxColumn.HeaderText = "WEIGHT";
            this.tWeightDataGridViewTextBoxColumn.Name = "tWeightDataGridViewTextBoxColumn";
            this.tWeightDataGridViewTextBoxColumn.ReadOnly = true;
            this.tWeightDataGridViewTextBoxColumn.Width = 65;
            // 
            // tBySpeciesDataGridViewTextBoxColumn
            // 
            this.tBySpeciesDataGridViewTextBoxColumn.DataPropertyName = "T_BySpecies";
            this.tBySpeciesDataGridViewTextBoxColumn.HeaderText = "T_BySpecies";
            this.tBySpeciesDataGridViewTextBoxColumn.Name = "tBySpeciesDataGridViewTextBoxColumn";
            this.tBySpeciesDataGridViewTextBoxColumn.ReadOnly = true;
            this.tBySpeciesDataGridViewTextBoxColumn.Visible = false;
            // 
            // tByProductDataGridViewTextBoxColumn
            // 
            this.tByProductDataGridViewTextBoxColumn.DataPropertyName = "T_ByProduct";
            this.tByProductDataGridViewTextBoxColumn.HeaderText = "T_ByProduct";
            this.tByProductDataGridViewTextBoxColumn.Name = "tByProductDataGridViewTextBoxColumn";
            this.tByProductDataGridViewTextBoxColumn.ReadOnly = true;
            this.tByProductDataGridViewTextBoxColumn.Visible = false;
            // 
            // tElementAccuracyDataGridViewTextBoxColumn
            // 
            this.tElementAccuracyDataGridViewTextBoxColumn.DataPropertyName = "T_ElementAccuracy";
            this.tElementAccuracyDataGridViewTextBoxColumn.HeaderText = "T_ElementAccuracy";
            this.tElementAccuracyDataGridViewTextBoxColumn.Name = "tElementAccuracyDataGridViewTextBoxColumn";
            this.tElementAccuracyDataGridViewTextBoxColumn.ReadOnly = true;
            this.tElementAccuracyDataGridViewTextBoxColumn.Visible = false;
            // 
            // tOverallAccuracyDataGridViewTextBoxColumn
            // 
            this.tOverallAccuracyDataGridViewTextBoxColumn.DataPropertyName = "T_OverallAccuracy";
            this.tOverallAccuracyDataGridViewTextBoxColumn.HeaderText = "T_OverallAccuracy";
            this.tOverallAccuracyDataGridViewTextBoxColumn.Name = "tOverallAccuracyDataGridViewTextBoxColumn";
            this.tOverallAccuracyDataGridViewTextBoxColumn.ReadOnly = true;
            this.tOverallAccuracyDataGridViewTextBoxColumn.Visible = false;
            // 
            // tDateStampDataGridViewTextBoxColumn
            // 
            this.tDateStampDataGridViewTextBoxColumn.DataPropertyName = "T_DateStamp";
            this.tDateStampDataGridViewTextBoxColumn.HeaderText = "T_DateStamp";
            this.tDateStampDataGridViewTextBoxColumn.Name = "tDateStampDataGridViewTextBoxColumn";
            this.tDateStampDataGridViewTextBoxColumn.ReadOnly = true;
            this.tDateStampDataGridViewTextBoxColumn.Visible = false;
            // 
            // tolerancesListBindingSource
            // 
            this.tolerancesListBindingSource.DataSource = typeof(CheckCruise.TolerancesList);
            // 
            // RegionalTolerances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 623);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.currentRegion);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.exitBurron);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toleranceData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label17);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegionalTolerances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter or Edit Regional Tolerances";
            ((System.ComponentModel.ISupportInitialize)(this.toleranceData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tolerancesListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView toleranceData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button exitBurron;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox includeVolume;
        private System.Windows.Forms.CheckBox volumeByProduct;
        private System.Windows.Forms.CheckBox volumeBySpecies;
        private System.Windows.Forms.TextBox PrimarySecondaryNetPC;
        private System.Windows.Forms.TextBox PrimarySecondaryGrossPC;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox SecondaryNetPC;
        private System.Windows.Forms.TextBox SecondaryGrossPC;
        private System.Windows.Forms.TextBox PrimaryNetPC;
        private System.Windows.Forms.TextBox PrimaryGrossPC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox overallAccuracy;
        private System.Windows.Forms.TextBox individualAccuracy;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox currentRegion;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.BindingSource tolerancesListBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn tElementDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tToleranceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tUnitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tAddParamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tWeightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tBySpeciesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tByProductDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tElementAccuracyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tOverallAccuracyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tDateStampDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox BoardVolume;
        private System.Windows.Forms.CheckBox CubicVolume;
        private System.Windows.Forms.CheckBox PrimaryGrossVol;
        private System.Windows.Forms.CheckBox PrimarySecondaryNetVol;
        private System.Windows.Forms.CheckBox PrimarySecondaryGrossVol;
        private System.Windows.Forms.CheckBox SecondaryNetVol;
        private System.Windows.Forms.CheckBox SecondaryGrossVol;
        private System.Windows.Forms.CheckBox PrimaryNetVol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
    }
}