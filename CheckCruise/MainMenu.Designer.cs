namespace CheckCruise
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.about_button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFile = new System.Windows.Forms.Button();
            this.tolerances_button = new System.Windows.Forms.Button();
            this.analysis_button = new System.Windows.Forms.Button();
            this.reports_button = new System.Windows.Forms.Button();
            this._cruisePathTextBox = new System.Windows.Forms.TextBox();
            this._browseCruiseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "NATIONAL CHECK CRUISE PROGRAM";
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(209, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "CREATE NEW";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.createFile);
            // 
            // exit_button
            // 
            this.exit_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_button.Location = new System.Drawing.Point(449, 318);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(75, 23);
            this.exit_button.TabIndex = 6;
            this.exit_button.Text = "EXIT";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.onExit);
            // 
            // about_button
            // 
            this.about_button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("about_button.BackgroundImage")));
            this.about_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.about_button.Location = new System.Drawing.Point(420, 12);
            this.about_button.Name = "about_button";
            this.about_button.Size = new System.Drawing.Size(104, 43);
            this.about_button.TabIndex = 7;
            this.about_button.UseVisualStyleBackColor = true;
            this.about_button.Click += new System.EventHandler(this.onAbout);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::CheckCruise.Properties.Resources.FVS_12;
            this.pictureBox1.Location = new System.Drawing.Point(13, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(173, 258);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(409, 68);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(115, 23);
            this.openFile.TabIndex = 9;
            this.openFile.Text = "OPEN EXISITNG";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.onOpenExisting);
            // 
            // tolerances_button
            // 
            this.tolerances_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tolerances_button.Location = new System.Drawing.Point(209, 97);
            this.tolerances_button.Name = "tolerances_button";
            this.tolerances_button.Size = new System.Drawing.Size(315, 23);
            this.tolerances_button.TabIndex = 3;
            this.tolerances_button.Text = "REGIONAL TOLERANCES";
            this.tolerances_button.UseVisualStyleBackColor = true;
            this.tolerances_button.Click += new System.EventHandler(this.onTolerances);
            // 
            // analysis_button
            // 
            this.analysis_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analysis_button.Location = new System.Drawing.Point(209, 226);
            this.analysis_button.Name = "analysis_button";
            this.analysis_button.Size = new System.Drawing.Size(315, 23);
            this.analysis_button.TabIndex = 4;
            this.analysis_button.Text = "CHECK CRUISE ANALYSIS";
            this.analysis_button.UseVisualStyleBackColor = true;
            this.analysis_button.Click += new System.EventHandler(this.onAnalysis);
            // 
            // reports_button
            // 
            this.reports_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reports_button.Location = new System.Drawing.Point(209, 270);
            this.reports_button.Name = "reports_button";
            this.reports_button.Size = new System.Drawing.Size(315, 23);
            this.reports_button.TabIndex = 5;
            this.reports_button.Text = "REPORTS";
            this.reports_button.UseVisualStyleBackColor = true;
            this.reports_button.Click += new System.EventHandler(this.onReports);
            // 
            // _cruisePathTextBox
            // 
            this._cruisePathTextBox.Location = new System.Drawing.Point(209, 191);
            this._cruisePathTextBox.Name = "_cruisePathTextBox";
            this._cruisePathTextBox.ReadOnly = true;
            this._cruisePathTextBox.Size = new System.Drawing.Size(234, 20);
            this._cruisePathTextBox.TabIndex = 10;
            this._cruisePathTextBox.Text = "aaasl;kjf;lskjf;lsjefl;ksjefoilsjelfjslkjef;lskjfl;skjefl;sjefl;ksjef;lskejflksej" +
    "flksejflksjeioswujeoifjsefl.cruise";
            // 
            // _browseCruiseButton
            // 
            this._browseCruiseButton.Location = new System.Drawing.Point(449, 188);
            this._browseCruiseButton.Name = "_browseCruiseButton";
            this._browseCruiseButton.Size = new System.Drawing.Size(75, 23);
            this._browseCruiseButton.TabIndex = 11;
            this._browseCruiseButton.Text = "Browse...";
            this._browseCruiseButton.UseVisualStyleBackColor = true;
            this._browseCruiseButton.Click += new System.EventHandler(this._browseCruiseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Original Cruise File Location:";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 361);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._browseCruiseButton);
            this.Controls.Add(this._cruisePathTextBox);
            this.Controls.Add(this.reports_button);
            this.Controls.Add(this.analysis_button);
            this.Controls.Add(this.tolerances_button);
            this.Controls.Add(this.openFile);
            this.Controls.Add(this.about_button);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Cruise Program";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Button about_button;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.Button tolerances_button;
        private System.Windows.Forms.Button analysis_button;
        private System.Windows.Forms.Button reports_button;
        private System.Windows.Forms.TextBox _cruisePathTextBox;
        private System.Windows.Forms.Button _browseCruiseButton;
        private System.Windows.Forms.Label label2;
    }
}

