namespace uBear.UI
{
    partial class LocationOverrideDialog
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
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.latDegEdit = new System.Windows.Forms.NumericUpDown();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lonDegEdit = new System.Windows.Forms.NumericUpDown();
            this.lbl3 = new System.Windows.Forms.Label();
            this.headingEdit = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.latDegEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lonDegEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(116, 319);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(89, 30);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(239, 319);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(89, 30);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "CANCEL";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // latDegEdit
            // 
            this.latDegEdit.DecimalPlaces = 7;
            this.latDegEdit.Location = new System.Drawing.Point(12, 46);
            this.latDegEdit.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.latDegEdit.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.latDegEdit.Name = "latDegEdit";
            this.latDegEdit.Size = new System.Drawing.Size(163, 30);
            this.latDegEdit.TabIndex = 5;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(12, 20);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(87, 23);
            this.lbl1.TabIndex = 6;
            this.lbl1.Text = "Latitude, °";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(12, 94);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(102, 23);
            this.lbl2.TabIndex = 8;
            this.lbl2.Text = "Longitude, °";
            // 
            // lonDegEdit
            // 
            this.lonDegEdit.DecimalPlaces = 7;
            this.lonDegEdit.Location = new System.Drawing.Point(12, 120);
            this.lonDegEdit.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.lonDegEdit.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.lonDegEdit.Name = "lonDegEdit";
            this.lonDegEdit.Size = new System.Drawing.Size(163, 30);
            this.lonDegEdit.TabIndex = 7;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(12, 175);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(89, 23);
            this.lbl3.TabIndex = 10;
            this.lbl3.Text = "Heading, °";
            // 
            // headingEdit
            // 
            this.headingEdit.DecimalPlaces = 2;
            this.headingEdit.Location = new System.Drawing.Point(12, 201);
            this.headingEdit.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.headingEdit.Name = "headingEdit";
            this.headingEdit.Size = new System.Drawing.Size(163, 30);
            this.headingEdit.TabIndex = 9;
            // 
            // LocationOverrideDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 361);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.headingEdit);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lonDegEdit);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.latDegEdit);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LocationOverrideDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Override antenna\'s location and heading";
            ((System.ComponentModel.ISupportInitialize)(this.latDegEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lonDegEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.NumericUpDown latDegEdit;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.NumericUpDown lonDegEdit;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.NumericUpDown headingEdit;
    }
}