namespace CheckCruise
{
    partial class CheckCruiseAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckCruiseAnalysis));
            this.label1 = new System.Windows.Forms.Label();
            this.reviewTolerances = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.compareButton = new System.Windows.Forms.Button();
            this.browseCheck = new System.Windows.Forms.Button();
            this.browseOriginal = new System.Windows.Forms.Button();
            this.checkFileName = new System.Windows.Forms.TextBox();
            this.originalFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.cruiserInitials = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "If desired, click the button to review regional tolerances before starting the an" +
    "alysis.";
            // 
            // reviewTolerances
            // 
            this.reviewTolerances.AutoSize = true;
            this.reviewTolerances.Location = new System.Drawing.Point(307, 12);
            this.reviewTolerances.Name = "reviewTolerances";
            this.reviewTolerances.Size = new System.Drawing.Size(231, 26);
            this.reviewTolerances.TabIndex = 1;
            this.reviewTolerances.Text = "REVIEW REGIONAL TOLERANCES";
            this.reviewTolerances.UseVisualStyleBackColor = true;
            this.reviewTolerances.Click += new System.EventHandler(this.onReviewTolerances);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cruiserInitials);
            this.groupBox1.Controls.Add(this.compareButton);
            this.groupBox1.Controls.Add(this.browseCheck);
            this.groupBox1.Controls.Add(this.browseOriginal);
            this.groupBox1.Controls.Add(this.checkFileName);
            this.groupBox1.Controls.Add(this.originalFileName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(23, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 239);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please select or enter filenames for analysis.";
            // 
            // compareButton
            // 
            this.compareButton.AutoSize = true;
            this.compareButton.Location = new System.Drawing.Point(229, 183);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(83, 26);
            this.compareButton.TabIndex = 6;
            this.compareButton.Text = "COMPARE";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.onCompare);
            // 
            // browseCheck
            // 
            this.browseCheck.Location = new System.Drawing.Point(433, 69);
            this.browseCheck.Name = "browseCheck";
            this.browseCheck.Size = new System.Drawing.Size(75, 23);
            this.browseCheck.TabIndex = 5;
            this.browseCheck.Text = "Browse...";
            this.browseCheck.UseVisualStyleBackColor = true;
            this.browseCheck.Click += new System.EventHandler(this.onBrowseCheck);
            // 
            // browseOriginal
            // 
            this.browseOriginal.Location = new System.Drawing.Point(433, 24);
            this.browseOriginal.Name = "browseOriginal";
            this.browseOriginal.Size = new System.Drawing.Size(75, 23);
            this.browseOriginal.TabIndex = 4;
            this.browseOriginal.Text = "Browse...";
            this.browseOriginal.UseVisualStyleBackColor = true;
            this.browseOriginal.Click += new System.EventHandler(this.onBrowseOriginal);
            // 
            // checkFileName
            // 
            this.checkFileName.Location = new System.Drawing.Point(173, 69);
            this.checkFileName.Name = "checkFileName";
            this.checkFileName.Size = new System.Drawing.Size(244, 22);
            this.checkFileName.TabIndex = 3;
            // 
            // originalFileName
            // 
            this.originalFileName.Location = new System.Drawing.Point(173, 25);
            this.originalFileName.Name = "originalFileName";
            this.originalFileName.Size = new System.Drawing.Size(244, 22);
            this.originalFileName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "CHECK CRUISE FILE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "ORIGINAL CRUISE FILE";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(456, 307);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.onExit);
            // 
            // cruiserInitials
            // 
            this.cruiserInitials.Location = new System.Drawing.Point(284, 129);
            this.cruiserInitials.MaxLength = 3;
            this.cruiserInitials.Name = "cruiserInitials";
            this.cruiserInitials.Size = new System.Drawing.Size(68, 22);
            this.cruiserInitials.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Enter Check Cruiser Initials";
            // 
            // CheckCruiseAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 372);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.reviewTolerances);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CheckCruiseAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check Cruise Analysis";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reviewTolerances;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseCheck;
        private System.Windows.Forms.Button browseOriginal;
        private System.Windows.Forms.TextBox checkFileName;
        private System.Windows.Forms.TextBox originalFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cruiserInitials;
    }
}