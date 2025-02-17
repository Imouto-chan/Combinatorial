namespace Combinatorial
{
    partial class CombinatorialForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            logBox = new TextBox();
            logLabel = new Label();
            SuspendLayout();
            // 
            // logBox
            // 
            logBox.Location = new Point(25, 59);
            logBox.Margin = new Padding(3, 4, 3, 4);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.Size = new Size(460, 344);
            logBox.TabIndex = 0;
            // 
            // logLabel
            // 
            logLabel.AutoSize = true;
            logLabel.Location = new Point(25, 35);
            logLabel.Name = "logLabel";
            logLabel.Size = new Size(37, 20);
            logLabel.TabIndex = 1;
            logLabel.Text = "Log:";
            // 
            // CombinatorialForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 451);
            Controls.Add(logLabel);
            Controls.Add(logBox);
            Name = "CombinatorialForm";
            Text = "Combinatorial";
            Shown += CombinatorialForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox logBox;
        private Label logLabel;
    }
}
