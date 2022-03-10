namespace CheckCruise
{
    partial class elementSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(elementSelection));
            this.selectedElement = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.selectedTolerance = new System.Windows.Forms.ComboBox();
            this.selectedUnit = new System.Windows.Forms.ComboBox();
            this.selectedParameter = new System.Windows.Forms.ComboBox();
            this.selectedWeight = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // selectedElement
            // 
            this.selectedElement.Items.AddRange(new object[] {
            "In/Out Trees",
            "Species",
            "Live/Dead",
            "Product",
            "DBH",
            "Total Height",
            "Total Height <= 100 feet",
            "Total Height > 100 feet",
            "Merch Height Primary",
            "Merch Height Secondary",
            "Height to First Live Limb",
            "Top DIB Primary",
            "Seen Defect % Primary",
            "Seen Defect % Secondary",
            "Recoverable %",
            "Form class",
            "Clear Face",
            "Tree Grade",
            "Log Grade",
            "Log Defect %"});
            this.selectedElement.Location = new System.Drawing.Point(109, 16);
            this.selectedElement.Name = "selectedElement";
            this.selectedElement.Size = new System.Drawing.Size(194, 24);
            this.selectedElement.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "ELEMENT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "TOLERANCE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "UNITS";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 32);
            this.label4.TabIndex = 4;
            this.label4.Text = "ADDITIONAL PARAMETER";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "WEIGHT";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(247, 215);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.onExit);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(136, 215);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.onCancel);
            // 
            // selectedTolerance
            // 
            this.selectedTolerance.FormattingEnabled = true;
            this.selectedTolerance.Items.AddRange(new object[] {
            "None",
            "0.1",
            "0.2",
            "0.3",
            "0.4",
            "0.5",
            "0.6",
            "0.7",
            "0.8",
            "0.9",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.selectedTolerance.Location = new System.Drawing.Point(109, 49);
            this.selectedTolerance.Name = "selectedTolerance";
            this.selectedTolerance.Size = new System.Drawing.Size(62, 24);
            this.selectedTolerance.TabIndex = 8;
            // 
            // selectedUnit
            // 
            this.selectedUnit.FormattingEnabled = true;
            this.selectedUnit.Items.AddRange(new object[] {
            "None",
            "feet",
            "inches",
            "percent",
            "abs",
            "grade"});
            this.selectedUnit.Location = new System.Drawing.Point(109, 87);
            this.selectedUnit.Name = "selectedUnit";
            this.selectedUnit.Size = new System.Drawing.Size(81, 24);
            this.selectedUnit.TabIndex = 9;
            // 
            // selectedParameter
            // 
            this.selectedParameter.FormattingEnabled = true;
            this.selectedParameter.Items.AddRange(new object[] {
            "None",
            "7% whichever >",
            "+1 per 25 feet > 100 feet",
            "3% whichever >"});
            this.selectedParameter.Location = new System.Drawing.Point(109, 130);
            this.selectedParameter.Name = "selectedParameter";
            this.selectedParameter.Size = new System.Drawing.Size(160, 24);
            this.selectedParameter.TabIndex = 10;
            // 
            // selectedWeight
            // 
            this.selectedWeight.Location = new System.Drawing.Point(109, 164);
            this.selectedWeight.Name = "selectedWeight";
            this.selectedWeight.Size = new System.Drawing.Size(43, 22);
            this.selectedWeight.TabIndex = 11;
            // 
            // elementSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 246);
            this.Controls.Add(this.selectedWeight);
            this.Controls.Add(this.selectedParameter);
            this.Controls.Add(this.selectedUnit);
            this.Controls.Add(this.selectedTolerance);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectedElement);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "elementSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Element Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectedElement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox selectedTolerance;
        private System.Windows.Forms.ComboBox selectedUnit;
        private System.Windows.Forms.ComboBox selectedParameter;
        private System.Windows.Forms.TextBox selectedWeight;
    }
}