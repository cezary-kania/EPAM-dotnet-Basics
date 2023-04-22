namespace Task1_WinForms
{
    partial class Mainform
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
            this.LabelOutput = new System.Windows.Forms.Label();
            this.InputName = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelOutput
            // 
            this.LabelOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelOutput.AutoSize = true;
            this.LabelOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabelOutput.Location = new System.Drawing.Point(11, 56);
            this.LabelOutput.Name = "LabelOutput";
            this.LabelOutput.Size = new System.Drawing.Size(0, 24);
            this.LabelOutput.TabIndex = 0;
            // 
            // InputName
            // 
            this.InputName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputName.Location = new System.Drawing.Point(76, 16);
            this.InputName.Name = "InputName";
            this.InputName.Size = new System.Drawing.Size(218, 20);
            this.InputName.TabIndex = 1;
            // 
            // LabelName
            // 
            this.LabelName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(12, 19);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(58, 13);
            this.LabelName.TabIndex = 2;
            this.LabelName.Text = "Username:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(300, 14);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 96);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.InputName);
            this.Controls.Add(this.LabelOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Mainform";
            this.Text = "Hello World";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelOutput;
        private System.Windows.Forms.TextBox InputName;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Button btnSubmit;
    }
}

